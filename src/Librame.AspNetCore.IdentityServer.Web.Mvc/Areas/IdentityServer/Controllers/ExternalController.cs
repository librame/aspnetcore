// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Web.Controllers
{
    using AspNetCore.IdentityServer.Builders;
    using AspNetCore.IdentityServer.Options;
    using AspNetCore.Web.Applications;
    using Extensions;
    using Extensions.Core.Identifiers;
    using Extensions.Core.Services;

    /// <summary>
    /// 外部控制器。
    /// </summary>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    [SecurityHeaders]
    [AllowAnonymous]
    [GenericApplicationModel(typeof(IdentityServerGenericTypeDefinitionMapper))]
    [Area(IdentityServerRouteBuilderExtensions.AreaName)]
    [Route(IdentityServerRouteBuilderExtensions.Template)]
    public class ExternalController<TUser, TGenId> : Controller
        where TUser : class, IIdentifier<TGenId>
        where TGenId : IEquatable<TGenId>
    {
        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        [InjectionService]
        private IIdentityServerInteractionService _interaction = null;

        [InjectionService]
        private IEventService _events = null;

        [InjectionService]
        private IOptions<IdentityServerBuilderOptions> _builderOptions = null;

        [InjectionService]
        private ILogger<ExternalController<TUser, TGenId>> _logger = null;


        /// <summary>
        /// 构造一个 <see cref="ExternalController{TUser, TGenId}"/>。
        /// </summary>
        /// <param name="injectionService">给定的 <see cref="IInjectionService"/>。</param>
        public ExternalController(IInjectionService injectionService)
        {
            injectionService.NotNull(nameof(injectionService)).Inject(this);
        }


        private UserManager<TUser> _userManager
            => _signInManager.UserManager;

        private AccountOptions _options
            => _builderOptions.Value.Accounts;


        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        public IActionResult Challenge(string scheme, string returnUrl)
        {
            if (returnUrl.IsEmpty())
                returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && _interaction.IsValidReturnUrl(returnUrl) == false)
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            // start challenge and roundtrip the return URL and scheme 
            var props = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(Callback)),
                Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", scheme },
                }
            };

            return Challenge(props, scheme);
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme).ConfigureAwait();
            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                _logger.LogDebug("External claims: {@claims}", externalClaims);
            }

            // lookup our user and external provider info
            var (user, provider, providerUserId, claims) = await FindUserFromExternalProviderAsync(result).ConfigureAwait();
            if (user == null)
            {
                // this might be where you might initiate a custom workflow for user registration
                // in this sample we don't show how that would be done, as our sample implementation
                // simply auto-provisions new external user
                user = await AutoProvisionUserAsync(provider, providerUserId, claims).ConfigureAwait();
            }

            // this allows us to collect any additional claims or properties
            // for the specific protocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallback(result, additionalLocalClaims, localSignInProps);

            // issue authentication cookie for user
            var id = (await user.GetObjectIdAsync().ConfigureAwait())?.ToString();
            var username = await _userManager.GetUserNameAsync(user).ConfigureAwait();

            var isuser = new IdentityServer4.IdentityServerUser(id)
            {
                DisplayName = username,
                IdentityProvider = provider,
                AdditionalClaims = additionalLocalClaims
            };

            await HttpContext.SignInAsync(isuser, localSignInProps).ConfigureAwait();

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme)
                .ConfigureAwait();

            // retrieve return URL
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            // check if external login is in the context of an OIDC request
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl).ConfigureAwait();

            await _events.RaiseAsync(new UserLoginSuccessEvent(provider,
                providerUserId, id, username, true, context?.Client.ClientId)).ConfigureAwait();

            if (context != null)
            {
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", returnUrl);
                }
            }

            return Redirect(returnUrl);
        }


        //private async Task<IActionResult> ProcessWindowsLoginAsync(string returnUrl)
        //{
        //    // see if windows auth has already been requested and succeeded
        //    var result = await HttpContext.AuthenticateAsync(_options.WindowsAuthenticationSchemeName).ConfigureAwait();
        //    if (result?.Principal is WindowsPrincipal wp)
        //    {
        //        // we will issue the external cookie and then redirect the
        //        // user back to the external callback, in essence, treating windows
        //        // auth the same as any other external authentication mechanism
        //        var props = new AuthenticationProperties()
        //        {
        //            RedirectUri = Url.Action("Callback"),
        //            Items =
        //            {
        //                { "returnUrl", returnUrl },
        //                { "scheme", _options.WindowsAuthenticationSchemeName },
        //            }
        //        };

        //        var id = new ClaimsIdentity(_options.WindowsAuthenticationSchemeName);
        //        id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
        //        id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));

        //        // add the groups as claims -- be careful if the number of groups is too large
        //        if (_options.IncludeWindowsGroups)
        //        {
        //            var wi = wp.Identity as WindowsIdentity;
        //            var groups = wi.Groups.Translate(typeof(NTAccount));
        //            var roles = groups.Select(x => new Claim(JwtClaimTypes.Role, x.Value));
        //            id.AddClaims(roles);
        //        }

        //        await HttpContext.SignInAsync(
        //            IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme,
        //            new ClaimsPrincipal(id),
        //            props).ConfigureAwait();

        //        return Redirect(props.RedirectUri);
        //    }
        //    else
        //    {
        //        // trigger windows auth
        //        // since windows auth don't support the redirect uri,
        //        // this URL is re-triggered when we call challenge
        //        return Challenge(_options.WindowsAuthenticationSchemeName);
        //    }
        //}

        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        private async Task<(TUser user, string provider, string providerUserId, IEnumerable<Claim> claims)>
            FindUserFromExternalProviderAsync(AuthenticateResult result)
        {
            var externalUser = result.Principal;

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                              externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            var claims = externalUser.Claims.ToList();
            claims.Remove(userIdClaim);

            var provider = result.Properties.Items["scheme"];
            var providerUserId = userIdClaim.Value;

            // find external user
            var user = await _userManager.FindByLoginAsync(provider, providerUserId).ConfigureAwait();

            return (user, provider, providerUserId, claims);
        }

        private async Task<TUser> AutoProvisionUserAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            foreach (var claim in claims)
            {
                // if the external system sends a display name - translate that to the standard OIDC name claim
                if (claim.Type == ClaimTypes.Name)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, claim.Value));
                }
                // if the JWT handler has an outbound mapping to an OIDC claim use that
                else if (JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.ContainsKey(claim.Type))
                {
                    filtered.Add(new Claim(JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap[claim.Type], claim.Value));
                }
                // copy the claim as-is
                else
                {
                    filtered.Add(claim);
                }
            }

            // if no display name was provided, try to construct by first and/or last name
            if (!filtered.Any(x => x.Type == JwtClaimTypes.Name))
            {
                var first = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value;
                var last = filtered.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // email
            var email = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
               claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Email, email));
            }

            var user = typeof(TUser).EnsureCreate<TUser>();
            await _userManager.SetUserNameAsync(user, Guid.NewGuid().ToString()).ConfigureAwait();
            
            var identityResult = await _userManager.CreateAsync(user).ConfigureAwait();
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            if (filtered.Any())
            {
                identityResult = await _userManager.AddClaimsAsync(user, filtered).ConfigureAwait();
                if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);
            }

            identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider)).ConfigureAwait();
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            return user;
        }

        // if the external login is OIDC-based, there are certain things we need to preserve to make logout work
        // this will be different for WS-Fed, SAML2p or other protocols
        private static void ProcessLoginCallback(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            var idToken = externalResult.Properties.GetTokenValue("id_token");
            if (idToken != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
            }
        }

    }
}