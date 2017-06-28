#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using LibrameStandard.Handlers;
using System.Linq;
using System.Threading.Tasks;

namespace LibrameStandard.Authentication.Handlers
{
    using Managers;
    using Models;
    using Utilities;

    /// <summary>
    /// 令牌处理程序。
    /// </summary>
    public class TokenHandler : AbstractHander<TokenHandlerSettings>, ITokenHandler
    {
        /// <summary>
        /// 构建一个默认处理程序实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public TokenHandler(ILibrameBuilder builder)
            : base(builder)
        {
        }


        /// <summary>
        /// 令牌管理器。
        /// </summary>
        public ITokenManager TokenManager => Builder.GetService<ITokenManager>();


        /// <summary>
        /// 开始处理令牌。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        public override void OnHandling(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                switch (context.Request.Method.ToLower())
                {
                    case "get":
                        {
                            var userResult = await ValidateToken(context);
                            if (userResult == null)
                            {
                                await context.Response.WriteBadRequestAsync("Invalid token.");
                                return;
                            }

                            await context.Response.WriteJsonAsync(userResult.User);
                            return;
                        }

                    //case "post":
                    //    {
                    //        if (!context.Request.HasFormContentType)
                    //        {
                    //            await context.Response.WriteBadRequestAsync("Invalid request.");
                    //            return;
                    //        }

                    //        var userResult = await ValidateUser(context);
                    //        if (userResult == null)
                    //        {
                    //            await context.Response.WriteBadRequestAsync("Username or password is empty.");
                    //            return;
                    //        }

                    //        if (!userResult.IdentityResult.Succeeded)
                    //        {
                    //            var message = userResult.IdentityResult.Errors.FirstOrDefault()?.Description;
                    //            await context.Response.WriteBadRequestAsync(message.AsOrDefault("Invalid username or password."));
                    //            return;
                    //        }

                    //        var token = TokenManager.Encode(userResult.User);
                    //        var result = new
                    //        {
                    //            access_token = token,
                    //            expires_in = (int)Settings.Expiration.TotalSeconds,
                    //        };

                    //        await context.Response.WriteJsonAsync(result);
                    //        return;
                    //    }

                    default:
                        {
                            await context.Response.WriteBadRequestAsync("Invalid request.");
                            return;
                        }
                }
            });
        }

        /// <summary>
        /// 根据 HTTP 上下文信息异步认证令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回用户身份结果。</returns>
        protected virtual Task<UserIdentityResult> ValidateToken(HttpContext context)
        {
            var name = context.Request.Query["name"];

            if (string.IsNullOrEmpty(name))
                name = context.Request.Headers["Authentication"];

            return TokenManager.ValidateAsync(name);
        }

        ///// <summary>
        ///// 根据 HTTP 上下文信息异步认证用户。
        ///// </summary>
        ///// <param name="context">给定的 HTTP 上下文。</param>
        ///// <returns>返回用户身份结果。</returns>
        //protected virtual Task<UserIdentityResult> ValidateUser(HttpContext context)
        //{
        //    var username = context.Request.Form["username"];
        //    var password = context.Request.Form["password"];

        //    return UserManager.ValidateAsync(username, password);
        //}

    }
}
