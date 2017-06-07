#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Runtime.InteropServices;

namespace LibrameStandard.Authentication
{
    using Adaptation;
    using Managers;
    using Utilities;

    /// <summary>
    /// 认证适配器设置。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationAdapterSettings : IAdapterSettings
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (nameof(Authentication) + ":");


        #region Token

        /// <summary>
        /// 令牌编解码器类型名键。
        /// </summary>
        public static readonly string TokenCodecTypeNameKey
            = (KeyPrefix + nameof(TokenCodecTypeName));

        /// <summary>
        /// 默认令牌编解码器类型名。
        /// </summary>
        public static readonly string DefaultTokenCodecTypeName
            = typeof(TokenCodec).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 令牌编解码器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultTokenCodecTypeName"/>。
        /// </value>
        public string TokenCodecTypeName { get; set; } = DefaultTokenCodecTypeName;


        /// <summary>
        /// 令牌处理程序类型名键。
        /// </summary>
        public static readonly string TokenHandlerTypeNameKey
            = (KeyPrefix + nameof(TokenHandlerTypeName));

        /// <summary>
        /// 默认令牌处理程序类型名。
        /// </summary>
        public static readonly string DefaultTokenHandlerTypeName
            = typeof(TokenHandler).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 令牌处理程序类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultTokenHandlerTypeName"/>。
        /// </value>
        public string TokenHandlerTypeName { get; set; } = DefaultTokenHandlerTypeName;


        /// <summary>
        /// 令牌管理器类型名键。
        /// </summary>
        public static readonly string TokenManagerTypeNameKey
            = (KeyPrefix + nameof(TokenManagerTypeName));

        /// <summary>
        /// 默认令牌管理器类型名。
        /// </summary>
        public static readonly string DefaultTokenManagerTypeName
            = typeof(TokenManager).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 令牌管理器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultTokenManagerTypeName"/>。
        /// </value>
        public string TokenManagerTypeName { get; set; } = DefaultTokenManagerTypeName;

        #endregion


        #region User

        /// <summary>
        /// 用户管理器类型名键。
        /// </summary>
        public static readonly string UserManagerTypeNameKey
            = (KeyPrefix + nameof(UserManagerTypeName));

        /// <summary>
        /// 默认用户管理器类型名。
        /// </summary>
        public static readonly string DefaultUserManagerTypeName
            = typeof(UserManager).AsAssemblyQualifiedNameWithoutVCP();

        /// <summary>
        /// 用户管理器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultUserManagerTypeName"/>。
        /// </value>
        public string UserManagerTypeName { get; set; } = DefaultUserManagerTypeName;

        #endregion

    }
}
