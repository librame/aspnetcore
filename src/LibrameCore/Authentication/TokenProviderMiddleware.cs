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
using System.IdentityModel.Tokens.Jwt;
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
                        return context.Response.WriteBadRequestAsync("Invalid request.");
                    }
            }
        }


        private async Task InvokeValidateToken(HttpContext context)
        {
            var userResult = await ValidateToken(context);
            if (userResult == null)
            {
                await context.Response.WriteBadRequestAsync("Invalid token.");
                return;
            }

            var result = new
            {
                username = userResult.User?.Name,
                succeeded = userResult.IdentityResult.Succeeded,
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
                await context.Response.WriteBadRequestAsync("Invalid request.");
                return;
            }

            var userResult = await ValidateUser(context);
            if (userResult == null)
            {
                await context.Response.WriteBadRequestAsync("Username or password is empty.");
                return;
            }

            if (!userResult.IdentityResult.Succeeded)
            {
                var message = userResult.IdentityResult.Errors.FirstOrDefault()?.Description;
                await context.Response.WriteBadRequestAsync(message.AsOrDefault("Invalid username or password."));
                return;
            }

            // Cookie
            var roles = await _roleManager.GetRoles(userResult.User);
            var identity = CreateIdentity(userResult.User, roles);
            
            await context.Authentication.SignInAsync(AuthenticationOptions.DEFAULT_SCHEME,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true
                });

            // Redirect ReturnUrl
            var returnUrl = context.Request.Form["returnUrl"].ToString();
            if (!string.IsNullOrEmpty(returnUrl))
            {
                context.Response.Redirect(returnUrl);
                return;
            }

            var token = _tokenManager.Encode(identity);

            // Redirect LoginSuccessful
            if (!string.IsNullOrEmpty(_options.TokenProvider.LoginSuccessful))
            {
                var location = AppendUrlToken(_options.TokenProvider.LoginSuccessful, token);

                context.Response.Redirect(location);
                return;
            }
            else
            {
                var result = new
                {
                    access_token = token,
                    expires_in = (int)_options.TokenProvider.Expiration.TotalSeconds,
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
        /// 解析用户模型。
        /// </summary>
        /// <param name="jwt">给定的 JSON Web 令牌。</param>
        /// <returns>返回用户模型与角色集合。</returns>
        private (IUserModel User, IEnumerable<string> Roles) ParseUserRoles(JwtSecurityToken jwt)
        {
            return LibrameIdentity.ParseUserRoles(jwt);
        }


        private string AppendUrlToken(string url, string token)
        {
            url += (url.IndexOf('?') > 0 ? "&" : "?") + "token=" + token;

            return url;
        }


        /// <summary>
        /// 根据 HTTP 上下文信息异步认证令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回用户身份结果。</returns>
        private Task<LibrameIdentityResult> ValidateToken(HttpContext context)
        {
            var token = context.Request.Query["token"].ToString();
            var requiredRolesString = context.Request.Query["required_roles"].ToString();

            if (string.IsNullOrEmpty(token))
                token = context.Request.Headers["Authentication"].ToString();

            if (string.IsNullOrEmpty(requiredRolesString))
                requiredRolesString = context.Request.Headers["RequiredRoles"].ToString();

            var requiredRoles = (!string.IsNullOrEmpty(requiredRolesString)
                ? requiredRolesString.ToString().Split(',') : null);

            return _tokenManager.ValidateAsync(token, requiredRoles, ParseUserRoles);
        }


        /// <summary>
        /// 根据 HTTP 上下文信息异步认证用户。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回用户身份结果。</returns>
        private Task<LibrameIdentityResult> ValidateUser(HttpContext context)
        {
            var username = context.Request.Form["username"].ToString();
            var password = context.Request.Form["password"].ToString();

            return _userManager.ValidateAsync(username, password);
        }

    }
}
