// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Web.Controllers
{
    using AspNetCore.IdentityServer.Builders;
    using AspNetCore.IdentityServer.Options;
    using AspNetCore.IdentityServer.Web.Models;
    using Extensions;
    using Extensions.Core.Services;

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


        private ConsentOptions _consentOptions
            => _builderOptions.Value.Consents;


        /// <summary>
        /// Shows the consent screen
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpGet]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var vm = await BuildViewModelAsync(returnUrl).ConfigureAwait();
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
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public async Task<IActionResult> Index(ConsentInputModel model)
        {
            var result = await ProcessConsent(model).ConfigureAwait();

            if (result.IsRedirect)
            {
                var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl).ConfigureAwait();
                if (context?.IsNativeClient() == true)
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", result.RedirectUri);
                }

                return Redirect(result.RedirectUri);
            }

            if (result.HasValidationError)
            {
                ModelState.AddModelError(string.Empty, result.ValidationError);
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
            var request = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl).ConfigureAwait();
            if (request == null) return result;

            ConsentResponse grantedConsent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model?.Button == "no")
            {
                grantedConsent = new ConsentResponse { Error = AuthorizationError.AccessDenied };

                // emit event
                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(),
                    request.Client.ClientId,
                    request.ValidatedResources.RawScopeValues))
                    .ConfigureAwait();
            }
            // user clicked 'yes' - validate the data
            else if (model?.Button == "yes")
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (_consentOptions.EnableOfflineAccess == false)
                    {
                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    grantedConsent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesValuesConsented = scopes.ToArray(),
                        Description = model.Description
                    };

                    // emit event
                    await _events.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(),
                        request.Client.ClientId,
                        request.ValidatedResources.RawScopeValues,
                        grantedConsent.ScopesValuesConsented,
                        grantedConsent.RememberConsent))
                        .ConfigureAwait();
                }
                else
                {
                    result.ValidationError = _consentOptions.MustChooseOneErrorMessage;
                }
            }
            else
            {
                result.ValidationError = _consentOptions.InvalidSelectionErrorMessage;
            }

            if (grantedConsent != null)
            {
                // communicate outcome of consent back to identityserver
                await _interaction.GrantConsentAsync(request, grantedConsent).ConfigureAwait();

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.Client = request.Client;
            }
            else
            {
                // we need to redisplay the consent UI
                result.ViewModel = await BuildViewModelAsync(model.ReturnUrl, model).ConfigureAwait();
            }

            return result;
        }

        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        private async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
        {
            var request = await _interaction.GetAuthorizationContextAsync(returnUrl).ConfigureAwait();
            if (request != null)
            {
                return CreateConsentViewModel(model, returnUrl, request);
            }
            else
            {
                _logger.LogError("No consent request matching request: {0}", returnUrl);
            }

            return null;
        }

        private ConsentViewModel CreateConsentViewModel(ConsentInputModel model, string returnUrl,
            AuthorizationRequest request)
        {
            var vm = new ConsentViewModel
            {
                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),
                Description = model?.Description,

                ReturnUrl = returnUrl,

                ClientName = request.Client.ClientName ?? request.Client.ClientId,
                ClientUrl = request.Client.ClientUri,
                ClientLogoUrl = request.Client.LogoUri,
                AllowRememberConsent = request.Client.AllowRememberConsent
            };

            vm.IdentityScopes = request.ValidatedResources.Resources.IdentityResources
                .Select(x => CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null))
                .ToArray();

            var apiScopes = new List<ScopeViewModel>();
            foreach (var parsedScope in request.ValidatedResources.ParsedScopes)
            {
                var apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
                if (apiScope != null)
                {
                    var scopeVm = CreateScopeViewModel(parsedScope, apiScope,
                        vm.ScopesConsented.Contains(parsedScope.RawValue) || model == null);
                    apiScopes.Add(scopeVm);
                }
            }
            if (_consentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
            {
                apiScopes.Add(GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null));
            }
            vm.ApiScopes = apiScopes;

            return vm;
        }

        private static ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeViewModel
            {
                Value = identity.Name,
                DisplayName = identity.DisplayName,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        private static ScopeViewModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
        {
            var displayName = apiScope.DisplayName ?? apiScope.Name;
            if (!string.IsNullOrWhiteSpace(parsedScopeValue.ParsedParameter))
            {
                displayName += ":" + parsedScopeValue.ParsedParameter;
            }

            return new ScopeViewModel
            {
                Value = parsedScopeValue.RawValue,
                DisplayName = displayName,
                Description = apiScope.Description,
                Emphasize = apiScope.Emphasize,
                Required = apiScope.Required,
                Checked = check || apiScope.Required
            };
        }

        private ScopeViewModel GetOfflineAccessScope(bool check)
        {
            return new ScopeViewModel
            {
                Value = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = _consentOptions.OfflineAccessDisplayName,
                Description = _consentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }
    }
}