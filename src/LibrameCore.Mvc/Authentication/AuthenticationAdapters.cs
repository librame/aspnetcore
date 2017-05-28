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
    using Utility;

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
        /// 用户认证。
        /// </summary>
        IUserAuthentication UserAuthentication { get; }


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
        /// 用户认证。
        /// </summary>
        public IUserAuthentication UserAuthentication => Builder.GetService<IUserAuthentication>();


        /// <summary>
        /// 尝试添加认证模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        public virtual ILibrameBuilder TryAddAuthentication()
        {
            var options = (Options as LibrameMvcOptions).Authentication;

            // 令牌生成器
            var tokenGeneratorType = Type.GetType(options.TokenGeneratorTypeName, throwOnError: true);
            typeof(ITokenGenerator).CanAssignableFromType(tokenGeneratorType);
            Builder.TryAddTransient(typeof(ITokenGenerator), tokenGeneratorType);

            // 用户认证
            var userAuthenticationType = Type.GetType(options.UserAuthenticationTypeName, throwOnError: true);
            typeof(IUserAuthentication).CanAssignableFromType(userAuthenticationType);
            Builder.TryAddTransient(typeof(IUserAuthentication), userAuthenticationType);

            return Builder;
        }

    }
}
