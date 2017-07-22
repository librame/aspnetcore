#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibrameCore
{
    using Authentication;
    using Authentication.Handlers;
    using Authentication.Models;

    /// <summary>
    /// 认证应用构建器静态扩展。
    /// </summary>
    public static class AuthenticationApplicationBuilderExtensions
    {

        /// <summary>
        /// 使用 Librame 认证。
        /// </summary>
        /// <typeparam name="TUserModel">指定的用户模型类型。</typeparam>
        /// <param name="app">给定的应用构建器接口。</param>
        /// <param name="cookieOptionsAction">给定的 Cookie 认证选项方法。</param>
        public static void UseLibrameAuthentication<TUserModel>(this IApplicationBuilder app,
            Action<CookieAuthenticationOptions> cookieOptionsAction = null)
            where TUserModel : class, IUserModel
        {
            var options = app.ApplicationServices.GetOptions<AuthenticationOptions>();

            // Use Cookie
            app.UseLibrameCookieAuthentication(cookieOptionsAction);

            // Use TokenHandler
            app.UseLibrameAuthenticationTokenHandler<TUserModel>(options.TokenHandler);
        }


        private static void UseLibrameCookieAuthentication(this IApplicationBuilder app,
            Action<CookieAuthenticationOptions> cookieOptionsAction = null)
        {
            var options = new CookieAuthenticationOptions
            {
                AuthenticationScheme = AuthenticationOptions.DEFAULT_SCHEME,
                // 是否自动启用验证，如果不启用，则即便客服端传输了Cookie信息，服务端也不会主动解析。
                // 除了明确配置了 [Authorize(ActiveAuthenticationSchemes = "上面的方案名")] 属性的地方，才会解析，此功能一般用在需要在同一应用中启用多种验证方案的时候。比如分Area.
                AutomaticAuthenticate = true,
                LoginPath = "/Account/Login"
            };
            
            cookieOptionsAction?.Invoke(options);

            app.UseCookieAuthentication(options);
        }

        private static void UseLibrameAuthenticationTokenHandler<TUserModel>(this IApplicationBuilder app,
            TokenHandlerSettings settings)
            where TUserModel : class, IUserModel
        {
            // 调用令牌处理程序
            var tokenHandler = app.ApplicationServices.GetService<ITokenHandler<TUserModel>>();

            app.Map(settings.Path, tokenHandler.Configure);
        }

    }
}
