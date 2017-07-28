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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrameCore.Authentication
{
    using Managers;
    using Models;

    /// <summary>
    /// 令牌提供程序中间件。
    /// </summary>
    /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
    public class TokenProviderMiddleware<TUserModel>
        where TUserModel : class, IUserModel
    {
        private readonly RequestDelegate _next;
        private readonly AuthenticationOptions _options;
        private readonly ITokenManager _tokenManager;
        private readonly IUserManager<TUserModel> _userManager;
        private readonly IRoleManager _roleManager;


        /// <summary>
        /// 构造一个令牌提供程序中间件实例。
        /// </summary>
        /// <param name="next">给定的下一步请求委托。</param>
        /// <param name="options">给定的认证选项。</param>
        /// <param name="tokenManager">给定的令牌管理器。</param>
        /// <param name="userManager">给定的用户管理器。</param>
        /// <param name="roleManager">给定的角色管理器。</param>
        public TokenProviderMiddleware(RequestDelegate next, IOptions<AuthenticationOptions> options,
            ITokenManager tokenManager, IUserManager<TUserModel> userManager, IRoleManager roleManager)
        {
            _next = next.NotNull(nameof(next));
            _options = options.NotNull(nameof(options)).Value;

            _tokenManager = tokenManager.NotNull(nameof(tokenManager));
            _userManager = userManager.NotNull(nameof(userManager));
            _roleManager = roleManager.NotNull(nameof(roleManager));
        }


        /// <summary>
        /// 异步执行。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回异步操作。</returns>
        public Task Invoke(HttpContext context)
        {
            // If the request path doesn't match, skip
            if (!context.Request.Path.Equals(_options.TokenProvider.Path, StringComparison.Ordinal))
            {
                return _next.Invoke(context);
            }

            switch (context.Request.Method.ToLower())
            {
                case "get":
                    {
                        // Validate
                        return InvokeValidateToken(context);
                    }

                case "post":
                    {
                        // Login
                        return InvokeGenerateToken(context);
                    }

                default:
                    {
                        return context.Response.WriteErrorRequestAsync(Resources.Core.InvalidRequest);
                    }
            }
        }


        private async Task InvokeValidateToken(HttpContext context)
        {
            var token = await ValidateToken(context);
            if (token.identity == null)
            {
                await context.Response.WriteErrorRequestAsync(Resources.Core.InvalidToken);
                return;
            }

            var result = new
            {
                authentication_type = token.identity.AuthenticationType,
                is_authenticated = token.identity.IsAuthenticated,
                name = token.identity.Name,
                issuer = token.identity.Issuer,
                audience = token.identity.Audience,
                issued_time = token.identity.IssuedTimeUtc.ToLocalTime().ToString(),
                expiration_time = token.identity.ExpirationTimeUtc.ToLocalTime().ToString(),
                id = token.identity.JwtId,
                subject = token.identity.Subject,
                uniqueName = token.identity.UniqueName,
                roles = token.identity.Roles.JoinString(","),
                // 支持角色验证
                succeeded = token.identityResult.Succeeded,
            };

            await context.Response.WriteJsonAsync(result);
            return;
        }


        /// <summary>
        /// 异步执行生成令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回异步操作。</returns>
        private async Task InvokeGenerateToken(HttpContext context)
        {
            if (!context.Request.HasFormContentType)
            {
                await context.Response.WriteErrorRequestAsync(Resources.Core.InvalidRequest);
                return;
            }

            var user = await ValidateUser(context);
            if (user.model == null)
            {
                var message = user.identity.Errors.FirstOrDefault()?.Description;
                message = message.AsOrDefault(Resources.Core.UsernameOrPasswordIsEmpty);
                
                await context.Response.WriteErrorRequestAsync(message);
                return;
            }

            if (!user.identity.Succeeded)
            {
                var message = user.identity.Errors.FirstOrDefault()?.Description;
                message = message.AsOrDefault(Resources.Core.InvalidUsernameOrPassword);

                await context.Response.WriteErrorRequestAsync(message);
                return;
            }
            
            // Identity
            var roles = await _roleManager.GetRoles(user.model);
            var identity = CreateIdentity(user.model, roles);

            // SignIn
            var utcNow = DateTimeOffset.UtcNow;
            await context.Authentication.SignInAsync(AuthenticationOptions.DEFAULT_SCHEME,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IssuedUtc = utcNow,
                    ExpiresUtc = utcNow.Add(_options.TokenProvider.Expiration)
                });

            // Redirect ReturnUrl
            var returnUrl = context.Request.Form["returnUrl"].ToString();
            returnUrl = returnUrl.AsOrDefault(context.Request.Query["returnUrl"].ToString());

            if (!string.IsNullOrEmpty(returnUrl))
            {
                context.Response.Redirect(returnUrl);
                return;
            }

            // Token
            var token = _tokenManager.Encode(identity);

            // Redirect LoginSuccessful
            if (!string.IsNullOrEmpty(_options.TokenProvider.LoginSuccessful))
            {
                var location = FormatUrlToken(_options.TokenProvider.LoginSuccessful, token);

                context.Response.Redirect(location);
                return;
            }
            else
            {
                var result = new
                {
                    access_token = token,
                    token_type = "bearer",
                    expires_in_seconds = (int)_options.TokenProvider.Expiration.TotalSeconds,
                };

                await context.Response.WriteJsonAsync(result);
                return;
            }
        }


        /// <summary>
        /// 创建 Librame 身份标识。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <param name="roles">给定的用户角色集合。</param>
        /// <returns>返回 Librame 身份标识。</returns>
        private LibrameIdentity CreateIdentity(IUserModel user, IEnumerable<string> roles)
        {
            return new LibrameIdentity(user, roles, _options.TokenProvider);
        }


        /// <summary>
        /// 格式 URL 令牌。
        /// </summary>
        /// <param name="url">给定的 URL。</param>
        /// <param name="token">给定的令牌。</param>
        /// <returns>返回字符串。</returns>
        private string FormatUrlToken(string url, string token)
        {
            if (url.Contains("{token}"))
                return url.Replace("{token}", token);

            return url;
        }


        /// <summary>
        /// 根据 HTTP 上下文信息异步认证令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回用户身份结果。</returns>
        private Task<(IdentityResult identityResult, LibrameIdentity identity)> ValidateToken(HttpContext context)
        {
            // 优先支持查询参数
            var token = context.Request.Query["token"].ToString();
            var requiredRolesString = context.Request.Query["required_roles"].ToString();

            // 其次支持请求头部信息
            if (string.IsNullOrEmpty(token))
                token = context.Request.Headers["Authentication"].ToString();

            if (string.IsNullOrEmpty(requiredRolesString))
                requiredRolesString = context.Request.Headers["RequiredRoles"].ToString();

            var requiredRoles = (!string.IsNullOrEmpty(requiredRolesString)
                ? requiredRolesString.ToString().Split(',') : null);

            return _tokenManager.ValidateAsync(token, requiredRoles);
        }


        /// <summary>
        /// 根据 HTTP 上下文信息异步认证用户。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回用户身份结果和用户模型。</returns>
        private Task<(IdentityResult identity, TUserModel model)> ValidateUser(HttpContext context)
        {
            var username = context.Request.Form["username"].ToString();
            var password = context.Request.Form["password"].ToString();

            return _userManager.ValidateAsync(username, password);
        }

    }
}
