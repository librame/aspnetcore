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
        public static void UseLibrameAuthentication<TUserModel>(this IApplicationBuilder app)
            where TUserModel : class, IUserModel
        {
            var options = app.ApplicationServices.GetOptions<AuthenticationOptions>();

            // Use TokenHandler
            app.UseLibrameAuthenticationTokenHandler<TUserModel>(options.TokenHandler);
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
