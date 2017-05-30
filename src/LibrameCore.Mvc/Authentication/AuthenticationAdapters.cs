#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Options;
using System;

namespace LibrameCore.Authentication
{
    using Adaptation;
    using Managers;
    using Utilities;

    /// <summary>
    /// 认证适配器接口。
    /// </summary>
    public interface IAuthenticationAdapter : IAdapter
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }


        /// <summary>
        /// 令牌生成器。
        /// </summary>
        ITokenGenerator TokenGenerator { get; }

        /// <summary>
        /// 用户管理器。
        /// </summary>
        IUserManager UserManager { get; }

        /// <summary>
        /// 令牌处理程序。
        /// </summary>
        ITokenHandler TokenHandler { get; }


        /// <summary>
        /// 尝试添加认证模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        ILibrameBuilder TryAddAuthentication();
    }


    /// <summary>
    /// 认证适配器。
    /// </summary>
    public class AuthenticationAdapter : AbstractAdapter, IAuthenticationAdapter
    {
        /// <summary>
        /// 构造一个算法适配器实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        /// <param name="options">给定的选择项。</param>
        public AuthenticationAdapter(ILibrameBuilder builder, IOptions<LibrameMvcOptions> options)
            : base(nameof(Authentication), options)
        {
            Builder = builder.NotNull(nameof(builder));

            TryAddAuthentication();
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }


        /// <summary>
        /// 令牌生成器。
        /// </summary>
        public ITokenGenerator TokenGenerator => Builder.GetService<ITokenGenerator>();

        /// <summary>
        /// 令牌处理程序。
        /// </summary>
        public ITokenHandler TokenHandler => Builder.GetService<ITokenHandler>();

        /// <summary>
        /// 用户管理器。
        /// </summary>
        public IUserManager UserManager => Builder.GetService<IUserManager>();


        /// <summary>
        /// 尝试添加认证模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        public virtual ILibrameBuilder TryAddAuthentication()
        {
            var options = (Options as LibrameMvcOptions).Authentication;

            // 用户管理器
            var userManagerType = Type.GetType(options.UserManagerTypeName, throwOnError: true);
            typeof(IUserManager).CanAssignableFromType(userManagerType);
            Builder.TryAddTransient(typeof(IUserManager), userManagerType);

            // 令牌生成器
            var tokenGeneratorType = Type.GetType(options.TokenGeneratorTypeName, throwOnError: true);
            typeof(ITokenGenerator).CanAssignableFromType(tokenGeneratorType);
            Builder.TryAddTransient(typeof(ITokenGenerator), tokenGeneratorType);
            
            // 令牌处理程序
            var tokenHandlerType = Type.GetType(options.TokenHandlerTypeName, throwOnError: true);
            typeof(ITokenHandler).CanAssignableFromType(tokenHandlerType);
            Builder.TryAddTransient(typeof(ITokenHandler), tokenHandlerType);

            return Builder;
        }

    }
}
