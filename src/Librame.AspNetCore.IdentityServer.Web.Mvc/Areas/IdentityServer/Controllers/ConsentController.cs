// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Web.Controllers
{
    using Builders;
    using Extensions;
    using Extensions.Core.Services;
    using Models;

    /// <summary>
    /// This controller processes the consent UI
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    [Area(IdentityServerRouteBuilderExtensions.AreaName)]
    [Route(IdentityServerRouteBuilderExtensions.Template)]
    public class ConsentController : Controller
    {
        [InjectionService]
        private IIdentityServerInteractionService _interaction = null;

        [InjectionService]
        private IClientStore _clientStore = null;

        [InjectionService]
        private IResourceStore _resourceStore = null;

        [InjectionService]
        private IEventService _events = null;

        [InjectionService]
        private ILogger<ConsentController> _logger = null;

        [InjectionService]
        private IOptions<IdentityServerBuilderOptions> _builderOptions = null;


        /// <summary>
        /// 构造一个 <see cref="ConsentController"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        public ConsentController(IInjectionService injectionService)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);
        }


        private ConsentOptions _options
            => _builderOptions.Value.Consents;


        /// <summary>
        /// Shows the consent screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await BuildViewModelAsync(returnUrl).ConfigureAndResultAsync();
            if (vm != null)
            {
                return View("Index", vm);
            }

            return View("Error");
        }

        /// <summary>
        /// Handles the consent screen postback
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "model")]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            model.NotNull(nameof(model));

            var result = await ProcessConsent(model).ConfigureAndResultAsync();

            if (result.IsRedirect)
            {
                if (await _clientStore.IsPkceClientAsync(result.ClientId).ConfigureAndResultAsync())
                {
                    // if the client is PKCE then we assume it's native, so this change in how to
                    // return the response is for better UX for the end user.
                    return View("Redirect", new RedirectViewModel { RedirectUrl = result.RedirectUri });
                }

                return Redirect(result.RedirectUri);
            }

            if (result.HasValidationError)
            {
                ModelState.AddModelError("", result.ValidationError);
            }

            if (result.ShowView)
            {
                return View("Index", result.ViewModel);
            }

            return View("Error");
        }

        /*****************************************/
        /* helper APIs for the ConsentController */
        /*****************************************/
        private async Task<ProcessConsentResult> ProcessConsent(ConsentInputModel model)
        {
            var result = new ProcessConsentResult();

            // validate return url is still valid
            var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl).ConfigureAndResultAsync();
            if (request == null) return result;

            ConsentResponse grantedConsent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model.Button == "no")
            {
                grantedConsent = ConsentResponse.Denied;

                // emit event
                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), result.ClientId, request.ScopesRequested)).ConfigureAndWaitAsync();
            }
            // user clicked 'yes' - validate the data
            else if (model.Button == "yes" && model != null)
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (_options.EnableOfflineAccess == false)
                    {
                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    grantedConsent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesConsented = scopes.ToArray()
                    };

                    // emit event
                    await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.ClientId, request.ScopesRequested, grantedConsent.ScopesConsented, grantedConsent.RememberConsent)).ConfigureAndWaitAsync();
                }
                else
                {
                    result.ValidationError = _options.MustChooseOneErrorMessage;
                }
            }
            else
            {
                result.ValidationError = _options.InvalidSelectionErrorMessage;
            }

            if (grantedConsent != null)
            {
                // communicate outcome of consent back to identityserver
                await _interaction.GrantConsentAsync(request, grantedConsent).ConfigureAndWaitAsync();

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.ClientId = request.ClientId;
            }
            else
            {
                // we need to redisplay the consent UI
                result.ViewModel = await BuildViewModelAsync(model.ReturnUrl, model).ConfigureAndResultAsync();
            }

            return result;
        }

        private async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl).ConfigureAndResultAsync();
            if (request != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId).ConfigureAndResultAsync();
                if (client != null)
                {
                    var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested).ConfigureAndResultAsync();
                    if (resources != null && (resources.IdentityResources.Any() || resources.ApiResources.Any()))
                    {
                        return CreateConsentViewModel(model, returnUrl, request, client, resources);
                    }
                    else
                    {
                        _logger.LogError("No scopes matching: {0}", request.ScopesRequested.Aggregate((x, y) => x + ", " + y));
                    }
                }
                else
                {
                    _logger.LogError("Invalid client id: {0}", request.ClientId);
                }
            }
            else
            {
                _logger.LogError("No consent request matching request: {0}", returnUrl);
            }

            return null;
        }

        private ConsentViewModel CreateConsentViewModel(
            ConsentInputModel model, string returnUrl,
            AuthorizationRequest request,
            Client client, IdentityServer4.Models.Resources resources)
        {
            var vm = new ConsentViewModel
            {
                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),

                ReturnUrl = returnUrl,

                ClientName = client.ClientName ?? client.ClientId,
                ClientUrl = client.ClientUri,
                ClientLogoUrl = client.LogoUri,
                AllowRememberConsent = client.AllowRememberConsent
            };

            vm.IdentityScopes = resources.IdentityResources.Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            vm.ResourceScopes = resources.ApiResources.SelectMany(x => x.Scopes).Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();
            if (_options.EnableOfflineAccess && resources.OfflineAccess)
            {
                vm.ResourceScopes = vm.ResourceScopes.Union(new ScopeViewModel[] {
                    GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null)
                });
            }

            return vm;
        }

        private static ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeViewModel
            {
                Name = identity.Name,
                DisplayName = identity.DisplayName,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        private static ScopeViewModel CreateScopeViewModel(Scope scope, bool check)
        {
            return new ScopeViewModel
            {
                Name = scope.Name,
                DisplayName = scope.DisplayName,
                Description = scope.Description,
                Emphasize = scope.Emphasize,
                Required = scope.Required,
                Checked = check || scope.Required
            };
        }

        private ScopeViewModel GetOfflineAccessScope(bool check)
        {
            return new ScopeViewModel
            {
                Name = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = _options.OfflineAccessDisplayName,
                Description = _options.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }
    }
}