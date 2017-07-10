#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Handlers;
using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibrameCore.Authentication.Handlers
{
    using Managers;
    using Models;

    /// <summary>
    /// 令牌处理程序。
    /// </summary>
    /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
    public class TokenHandler<TUserModel> : AbstractHander, ITokenHandler<TUserModel>
        where TUserModel : class, IUserModel
    {
        /// <summary>
        /// 构建一个默认处理程序实例。
        /// </summary>
        /// <param name="tokenManager">给定的令牌管理器。</param>
        /// <param name="userManager">给定的用户管理器。</param>
        /// <param name="roleManager">给定的角色管理器。</param>
        public TokenHandler(ITokenManager tokenManager, IUserManager<TUserModel> userManager,
            IRoleManager roleManager)
        {
            TokenManager = tokenManager.NotNull(nameof(tokenManager));
            UserManager = userManager.NotNull(nameof(userManager));
            RoleManager = roleManager.NotNull(nameof(roleManager));
        }

        
        /// <summary>
        /// 令牌管理器。
        /// </summary>
        public ITokenManager TokenManager { get; }
        
        /// <summary>
        /// 用户管理器。
        /// </summary>
        public IUserManager<TUserModel> UserManager { get; }

        /// <summary>
        /// 角色管理器。
        /// </summary>
        public IRoleManager RoleManager { get; }


        /// <summary>
        /// 参数设置。
        /// </summary>
        public TokenHandlerSettings Settings => TokenManager.Options.TokenHandler;


        /// <summary>
        /// 配置令牌处理程序。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        public override void Configure(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                switch (context.Request.Method.ToLower())
                {
                    case "get":
                        {
                            // Validate
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

                    case "post":
                        {
                            // Login
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

                            var roles = await RoleManager.GetRoles(userResult.User);
                            var identity = CreateIdentity(userResult.User, roles);

                            var token = TokenManager.Encode(identity);

                            // Cookie
                            await context.Authentication.SignInAsync(AuthenticationOptions.DEFAULT_SCHEME,
                                new ClaimsPrincipal(identity),
                                new AuthenticationProperties
                                {
                                    IsPersistent = true
                                });

                            // Success
                            if (!string.IsNullOrEmpty(Settings.LoginSuccessful))
                            {
                                var location = Settings.LoginSuccessful;
                                location += (location.IndexOf('?') > 0 ? "&" : "?") + "token=" + token;

                                context.Response.Redirect(location);
                                return;
                            }
                            else
                            {
                                var result = new
                                {
                                    access_token = token,
                                    expires_in = (int)Settings.Expiration.TotalSeconds,
                                };

                                await context.Response.WriteJsonAsync(result);
                                return;
                            }
                        }

                    default:
                        {
                            await context.Response.WriteBadRequestAsync("Invalid request.");
                            return;
                        }
                }
            });
        }


        /// <summary>
        /// 创建 Librame 身份标识。
        /// </summary>
        /// <param name="user">给定的用户模型。</param>
        /// <param name="roles">给定的用户角色集合。</param>
        /// <returns>返回 Librame 身份标识。</returns>
        protected virtual LibrameIdentity CreateIdentity(IUserModel user, IEnumerable<string> roles)
        {
            return new LibrameIdentity(user, roles, Settings);
        }

        /// <summary>
        /// 解析用户模型。
        /// </summary>
        /// <param name="jwt">给定的 JSON Web 令牌。</param>
        /// <returns>返回用户模型与角色集合。</returns>
        protected virtual (IUserModel User, IEnumerable<string> Roles) ParseUserRoles(JwtSecurityToken jwt)
        {
            return LibrameIdentity.ParseUserRoles(jwt);
        }


        /// <summary>
        /// 根据 HTTP 上下文信息异步认证令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回用户身份结果。</returns>
        protected virtual Task<LibrameIdentityResult> ValidateToken(HttpContext context)
        {
            var token = context.Request.Query["token"].ToString();
            var requiredRolesString = context.Request.Query["required_roles"].ToString();

            if (string.IsNullOrEmpty(token))
                token = context.Request.Headers["Authentication"].ToString();

            if (string.IsNullOrEmpty(requiredRolesString))
                requiredRolesString = context.Request.Headers["RequiredRoles"].ToString();

            var requiredRoles = (!string.IsNullOrEmpty(requiredRolesString)
                ? requiredRolesString.ToString().Split(',') : null);

            return TokenManager.ValidateAsync(token, requiredRoles, ParseUserRoles);
        }

        /// <summary>
        /// 根据 HTTP 上下文信息异步认证用户。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回用户身份结果。</returns>
        protected virtual Task<LibrameIdentityResult> ValidateUser(HttpContext context)
        {
            var username = context.Request.Form["username"].ToString();
            var password = context.Request.Form["password"].ToString();

            return UserManager.ValidateAsync(username, password);
        }

    }
}
