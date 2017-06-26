﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace LibrameStandard.Authentication
{
    using Adaptation;
    using Handlers;
    using Managers;
    using Models;
    using Senders;

    /// <summary>
    /// 认证适配器。
    /// </summary>
    public class AuthenticationAdapter : AbstractAdapter, IAuthenticationAdapter
    {
        /// <summary>
        /// 构造一个算法适配器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public AuthenticationAdapter(ILibrameBuilder builder)
            : base(nameof(Authentication), builder)
        {
            TryAddAuthentication();
        }
        

        /// <summary>
        /// 令牌处理程序。
        /// </summary>
        public ITokenHandler TokenHandler => Builder.GetService<ITokenHandler>();

        /// <summary>
        /// 密码管理器。
        /// </summary>
        public IPasswordManager PasswordManager => UserManager.PasswordManager;

        /// <summary>
        /// 角色管理器。
        /// </summary>
        public IRoleManager RoleManager => Builder.GetService<IRoleManager>();

        /// <summary>
        /// 令牌管理器。
        /// </summary>
        public ITokenManager TokenManager => TokenHandler.TokenManager;

        /// <summary>
        /// 用户管理器。
        /// </summary>
        public IUserManager UserManager => TokenHandler.UserManager;


        /// <summary>
        /// 邮箱发送器。
        /// </summary>
        public IEmailSender EmailSender => Builder.GetService<IEmailSender>();

        /// <summary>
        /// 短信发送器。
        /// </summary>
        public ISmsSender SmsSender => Builder.GetService<ISmsSender>();


        /// <summary>
        /// 尝试添加认证模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        public virtual ILibrameBuilder TryAddAuthentication()
        {
            var options = (Builder.Options as LibrameCoreOptions).Authentication;

            // 处理程序
            var tokenHandlerType = Type.GetType(options.Handlers.TokenHandlerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(ITokenHandler), tokenHandlerType);

            // 管理器
            var passwordManagerType = Type.GetType(options.Managers.PasswordManagerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(IPasswordManager), passwordManagerType);

            var roleManagerType = Type.GetType(options.Managers.RoleManagerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(IRoleManager), roleManagerType);

            var tokenManagerType = Type.GetType(options.Managers.TokenManagerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(ITokenManager), tokenManagerType);

            var userManagerType = Type.GetType(options.Managers.UserManagerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(IUserManager), userManagerType);
            
            // 模型
            var roleModelType = Type.GetType(options.Models.RoleModelTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(IRoleModel), roleModelType);

            var tokenModelType = Type.GetType(options.Models.TokenModelTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(ITokenModel), tokenModelType);

            var userModelType = Type.GetType(options.Models.UserModelTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(IUserModel), userModelType);

            // 发送器
            var emailSenderType = Type.GetType(options.Senders.EmailSenderTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(IEmailSender), emailSenderType);

            var smsSenderType = Type.GetType(options.Senders.SmsSenderTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(ISmsSender), smsSenderType);
            
            // Add Identity
            //Builder.Services.AddIdentity<IUserModel, IRoleModel>()
            //    .AddEntityFrameworkStores<Entity.DbContexts.SqlServerDbContextReader>()
            //    .AddDefaultTokenProviders();

            return Builder;
        }

    }
}