#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Authentication;
using LibrameCore.Authentication.Managers;
using LibrameCore.Authentication.Senders;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace LibrameStandard
{
    /// <summary>
    /// 认证 Librame 构建器静态扩展。
    /// </summary>
    public static class AuthenticationLibrameBuilderExtensions
    {

        /// <summary>
        /// 注册认证模块。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器。</param>
        /// <param name="optionsAction">给定的认证选项动作方法（可选）。</param>
        /// <returns>返回 Librame 构建器。</returns>
        public static ILibrameBuilder AddAuthentication(this ILibrameBuilder builder,
            Action<AuthenticationOptions> optionsAction = null)
        {
            // AddOptions
            builder.AddOptions(AuthenticationOptions.Key, optionsAction);

            // HttpContext 访问器
            builder.Services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            // 管理器
            builder.Services.TryAddTransient<IPasswordManager, PasswordManager>();
            builder.Services.TryAddTransient<IRoleManager, RoleManager>();
            builder.Services.TryAddTransient<ITokenManager, TokenManager>();
            builder.Services.TryAddTransient(typeof(IUserManager<>), typeof(UserManager<>));

            // 发送器
            builder.Services.TryAddTransient<IEmailSender, EmailSender>();
            builder.Services.TryAddTransient<ISmsSender, SmsSender>();

            return builder;
        }

    }
}
