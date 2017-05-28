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

namespace LibrameCore.Authentication
{
    using Utility;

    /// <summary>
    /// 认证选项。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationOptions : ILibrameOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (nameof(Authentication) + ":");


        /// <summary>
        /// 令牌生成器类型名键。
        /// </summary>
        public static readonly string TokenGeneratorTypeNameKey
            = (KeyPrefix + nameof(TokenGeneratorTypeName));

        /// <summary>
        /// 默认令牌生成器类型名。
        /// </summary>
        public static readonly string DefaultTokenGeneratorTypeName
            = typeof(TokenGenerator).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 令牌生成器类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultTokenGeneratorTypeName"/>。
        /// </value>
        public string TokenGeneratorTypeName { get; set; } = DefaultTokenGeneratorTypeName;


        /// <summary>
        /// 用户认证类型名键。
        /// </summary>
        public static readonly string UserAuthenticationTypeNameKey
            = (KeyPrefix + nameof(UserAuthenticationTypeName));

        /// <summary>
        /// 默认用户认证类型名。
        /// </summary>
        public static readonly string DefaultUserAuthenticationTypeName
            = typeof(UserAuthentication).AssemblyQualifiedNameWithoutVcp();

        /// <summary>
        /// 用户认证类型名。
        /// </summary>
        /// <value>
        /// 默认为 <see cref="DefaultUserAuthenticationTypeName"/>。
        /// </value>
        public string UserAuthenticationTypeName { get; set; } = DefaultUserAuthenticationTypeName;

    }
}
