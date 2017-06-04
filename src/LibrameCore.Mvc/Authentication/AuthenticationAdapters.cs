#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace LibrameStandard.Authentication
{
    using Adaptation;
    using Managers;

    /// <summary>
    /// 认证适配器接口。
    /// </summary>
    public interface IAuthenticationAdapter : IAdapter
    {
        /// <summary>
        /// 令牌生成器。
        /// </summary>
        ITokenCodec TokenCodec { get; }
        
        /// <summary>
        /// 令牌处理程序。
        /// </summary>
        ITokenHandler TokenHandler { get; }

        /// <summary>
        /// 令牌管理器。
        /// </summary>
        ITokenManager TokenManager { get; }

        /// <summary>
        /// 用户管理器。
        /// </summary>
        IUserManager UserManager { get; }


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
        public AuthenticationAdapter(ILibrameBuilder builder)
            : base(nameof(Authentication), builder)
        {
            TryAddAuthentication();
        }


        /// <summary>
        /// 令牌编解码器。
        /// </summary>
        public ITokenCodec TokenCodec => TokenHandler.TokenManager.Codec;

        /// <summary>
        /// 令牌处理程序。
        /// </summary>
        public ITokenHandler TokenHandler => Builder.GetService<ITokenHandler>();

        /// <summary>
        /// 令牌管理器。
        /// </summary>
        public ITokenManager TokenManager => TokenHandler.TokenManager;

        /// <summary>
        /// 用户管理器。
        /// </summary>
        public IUserManager UserManager => TokenHandler.UserManager;


        /// <summary>
        /// 尝试添加认证模块。
        /// </summary>
        /// <returns>返回 Librame 构建器。</returns>
        public virtual ILibrameBuilder TryAddAuthentication()
        {
            var options = (Builder.Options as LibrameMvcOptions).Authentication;

            // 用户管理器
            var userManagerType = Type.GetType(options.UserManagerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(IUserManager), userManagerType);

            // 令牌编解码器
            var tokenCodecType = Type.GetType(options.TokenCodecTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(ITokenCodec), tokenCodecType);
            
            // 令牌处理程序
            var tokenHandlerType = Type.GetType(options.TokenHandlerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(ITokenHandler), tokenHandlerType);

            // 令牌管理器
            var tokenManagerType = Type.GetType(options.TokenManagerTypeName, throwOnError: true);
            Builder.TryAddTransient(typeof(ITokenManager), tokenManagerType);

            return Builder;
        }

    }
}
