// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
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
    /// 设备控制器。
    /// </summary>
    [Authorize]
    [SecurityHeaders]
    [Area(IdentityServerRouteBuilderExtensions.AreaName)]
    [Route(IdentityServerRouteBuilderExtensions.Template)]
    public class DeviceController : Controller
    {
        [InjectionService]
        private IDeviceFlowInteractionService _interaction = null;

        [InjectionService]
        private IEventService _events = null;

        [InjectionService]
        private IOptions<IdentityServerOptions> _options = null;

        [InjectionService]
        private IOptions<IdentityServerBuilderOptions> _builderOptions = null;


        /// <summary>
        /// 构造一个 <see cref="DeviceController"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        public DeviceController(IInjectionService injectionService)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);
        }


        private ConsentOptions _consentOptions
            => _builderOptions.Value.Consents;


        /// <summary>
        /// GET: /Device
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userCodeParamName = _options.Value.UserInteraction.DeviceVerificationUserCodeParameter;
            string userCode = Request.Query[userCodeParamName];
            if (string.IsNullOrWhiteSpace(userCode)) return View("UserCodeCapture");

            var vm = await BuildViewModelAsync(userCode).ConfigureAwait();
            if (vm == null) return View("Error");

            vm.ConfirmUserCode = true;
            return View("UserCodeConfirmation", vm);
        }


        /// <summary>
        /// POST: /Device/UserCodeCapture
        /// </summary>
        /// <param name="userCode"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCodeCapture(string userCode)
        {
            var vm = await BuildViewModelAsync(userCode).ConfigureAwait();
            if (vm == null) return View("Error");

            return View("UserCodeConfirmation", vm);
        }


        /// <summary>
        /// POST: /Device/Callback
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Callback(DeviceAuthorizationInputModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var result = await ProcessConsent(model).ConfigureAwait();
            if (result.HasValidationError) return View("Error");

            return View("Success");
        }


        private async Task<ProcessConsentResult> ProcessConsent(DeviceAuthorizationInputModel model)
        {
            var result = new ProcessConsentResult();

            var request = await _interaction.GetAuthorizationContextAsync(model.UserCode).ConfigureAwait();
            if (request == null)
                return result;

            ConsentResponse grantedConsent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model.Button == "no")
            {
                grantedConsent = new ConsentResponse { Error = AuthorizationError.AccessDenied };

                // emit event
                await _events.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(),
                    request.Client.ClientId,
                    request.ValidatedResources.RawScopeValues))
                    .ConfigureAwait();
            }
            // user clicked 'yes' - validate the data
            else if (model.Button == "yes")
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
                await _interaction.HandleRequestAsync(model.UserCode, grantedConsent).ConfigureAwait();

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.Client = request.Client;
            }
            else
            {
                // we need to redisplay the consent UI
                result.ViewModel = await BuildViewModelAsync(model.UserCode, model).ConfigureAwait();
            }

            return result;
        }

        private async Task<DeviceAuthorizationViewModel> BuildViewModelAsync(string userCode, DeviceAuthorizationInputModel model = null)
        {
            var request = await _interaction.GetAuthorizationContextAsync(userCode).ConfigureAwait();
            if (request != null)
            {
                return CreateConsentViewModel(userCode, model, request);
            }

            return null;
        }

        private DeviceAuthorizationViewModel CreateConsentViewModel(string userCode,
            DeviceAuthorizationInputModel model, DeviceFlowAuthorizationRequest request)
        {
            var vm = new DeviceAuthorizationViewModel
            {
                UserCode = userCode,
                Description = model?.Description,

                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),
                
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
                    var scopeVm = CreateScopeViewModel(parsedScope, apiScope, vm.ScopesConsented.Contains(parsedScope.RawValue) || model == null);
                    apiScopes.Add(scopeVm);
                }
            }
            if (_consentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
            {
                apiScopes.Add(GetOfflineAccessScope(vm.ScopesConsented.Contains(IdentityServerConstants.StandardScopes.OfflineAccess) || model == null));
            }
            vm.ApiScopes = apiScopes;

            return vm;
        }

        private static ScopeViewModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeViewModel
            {
                Value = identity.Name,
                DisplayName = identity.DisplayName ?? identity.Name,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        private static ScopeViewModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
        {
            return new ScopeViewModel
            {
                Value = parsedScopeValue.RawValue,
                // todo: use the parsed scope value in the display?
                DisplayName = apiScope.DisplayName ?? apiScope.Name,
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
                Value = IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = _consentOptions.OfflineAccessDisplayName,
                Description = _consentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }

    }
}