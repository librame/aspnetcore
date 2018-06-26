#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LibrameCore.Extensions.Authentication
{
    /// <summary>
    /// 认证扩展处理程序。
    /// </summary>
    public class AuthenticationExtensionHandler : AuthenticationHandler<AuthenticationExtensionOptions>, IAuthenticationSignInHandler
    {
        private IAuthenticationPolicy _policy = null;


        /// <summary>
        /// 构造一个 <see cref="AuthenticationExtensionHandler"/> 实例。
        /// </summary>
        /// <param name="policy">给定的 <see cref="IAuthenticationPolicy"/>。</param>
        /// <param name="options">给定的 <see cref="IOptionsMonitor{AuthenticationExtensionOptions}"/>。</param>
        /// <param name="logger">给定的 <see cref="ILoggerFactory"/>。</param>
        /// <param name="encoder">给定的 <see cref="UrlEncoder"/>。</param>
        /// <param name="clock">给定的 <see cref="ISystemClock"/>。</param>
        public AuthenticationExtensionHandler(IAuthenticationPolicy policy,
            IOptionsMonitor<AuthenticationExtensionOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _policy = policy.NotNull(nameof(policy));
        }


        /// <summary>
        /// 异步登入。
        /// </summary>
        /// <param name="user">给定的声明身份。</param>
        /// <param name="properties">给定的认证属性。</param>
        /// <returns>返回一个任务。</returns>
        public Task SignInAsync(ClaimsPrincipal user, AuthenticationProperties properties)
        {
            if (user.Identity is LibrameClaimsIdentity)
            {
                var identity = (user.Identity as LibrameClaimsIdentity);
                var token = _policy.TokenManager.Encode(identity);

                _policy.AddCookieToken(Context, identity.ExpirationTimeUtc, token);
            }

            return Task.CompletedTask;
        }


        /// <summary>
        /// 异步登出。
        /// </summary>
        /// <param name="properties">给定的认证属性。</param>
        /// <returns>返回一个任务。</returns>
        public Task SignOutAsync(AuthenticationProperties properties)
        {
            _policy.DeleteCookieToken(Context);

            return Task.CompletedTask;
        }


        /// <summary>
        /// 异步处理认证。
        /// </summary>
        /// <returns>返回一个带认证结果的任务。</returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var auth = _policy.Authenticate(Context);

            if (auth.Identity == null || !auth.Identity.IsAuthenticated)
                return Task.FromResult(AuthenticateResult.NoResult());

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(auth.Identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    IssuedUtc = auth.Identity.IssuedTimeUtc,
                    ExpiresUtc = auth.Identity.ExpirationTimeUtc,
                    AllowRefresh = false
                },
                Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

    }
}