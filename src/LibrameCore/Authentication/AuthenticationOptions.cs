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
using LibrameCore.Handlers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Runtime.InteropServices;

namespace LibrameCore.Authentication
{
    /// <summary>
    /// 认证选项。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public class AuthenticationOptions : ILibrameOptions
    {
        /// <summary>
        /// 默认认证方案。
        /// </summary>
        public const string DEFAULT_SCHEME = "LibrameAuthentication";


        /// <summary>
        /// 键名。
        /// </summary>
        internal static readonly string Key = nameof(Authentication);

        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (Key + ":");


        #region ProtectedUsernames

        /// <summary>
        /// 受保护用户名集合键。
        /// </summary>
        public static readonly string ProtectedUsernamesKey
            = KeyPrefix + nameof(ProtectedUsernames);

        /// <summary>
        /// 默认受保护用户名集合。
        /// </summary>
        public static readonly string DefaultProtectedUsernames = "librame,admin";

        /// <summary>
        /// 受保护用户名集合。
        /// </summary>
        public string ProtectedUsernames { get; set; } = DefaultProtectedUsernames;

        #endregion


        /// <summary>
        /// 令牌处理程序。
        /// </summary>
        public TokenHandlerSettings TokenHandler { get; set; }
            = new TokenHandlerSettings();
    }


    /// <summary>
    /// 令牌处理程序设置。
    /// </summary>
    public class TokenHandlerSettings : HandlerSettings
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix = (nameof(AuthenticationOptions.TokenHandler) + ":");


        /// <summary>
        /// 构造一个令牌处理程序设置。
        /// </summary>
        public TokenHandlerSettings()
            : base("/token")
        {
        }


        #region Issuer

        /// <summary>
        /// 签发者键。
        /// </summary>
        public static readonly string IssuerKey
            = KeyPrefix + nameof(Issuer);

        /// <summary>
        /// 默认签发者。
        /// </summary>
        public static readonly string DefaultIssuer = "http://localhost:10768/";

        /// <summary>
        /// 签发者。
        /// </summary>
        public string Issuer { get; set; } = DefaultIssuer;

        #endregion


        #region Audience

        /// <summary>
        /// 接收者键。
        /// </summary>
        public static readonly string AudienceKey
            = KeyPrefix + nameof(Audience);

        /// <summary>
        /// 默认接收者。
        /// </summary>
        public static readonly string DefaultAudience = "http://localhost:10768/";

        /// <summary>
        /// 接收者。
        /// </summary>
        public string Audience { get; set; } = "http://localhost:10768/";

        #endregion


        #region LoginSuccessful

        /// <summary>
        /// 登录成功后转向键。
        /// </summary>
        public static readonly string LoginSuccessfulKey
            = KeyPrefix + nameof(LoginSuccessful);

        /// <summary>
        /// 默认登录成功后转向。
        /// </summary>
        public static readonly string DefaultLoginSuccessful = "http://localhost:10768/User/Validate";

        /// <summary>
        /// 登录成功后转向。
        /// </summary>
        public string LoginSuccessful { get; set; } = "http://localhost:10768/User/Validate";

        #endregion


        /// <summary>
        /// 过期时间间隔。
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(30);

        /// <summary>
        /// 签名证书。
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

    }

}
