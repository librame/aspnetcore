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
using MailKit.Security;
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
        /// 默认 COOKIE 名称。
        /// </summary>
        public const string DEFAULT_COOKIE_NAME = "LibrameAuthenticationCookie";

        /// <summary>
        /// 默认令牌 COOKIE 名称。
        /// </summary>
        public const string DEFAULT_TOKEN_COOKIE_NAME = "LibrameAuthenticationTokenCookie";

        /// <summary>
        /// 默认登录路径。
        /// </summary>
        public const string DEFAULT_LOGIN_PATH = "/Account/Login";


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
        /// SMTP 选项。
        /// </summary>
        public SmtpOptions Smtp { get; set; }
            = new SmtpOptions();


        /// <summary>
        /// 令牌选项。
        /// </summary>
        public TokenOptions Token { get; set; }
            = new TokenOptions();
    }


    /// <summary>
    /// SMTP 选项。
    /// </summary>
    public class SmtpOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (nameof(AuthenticationOptions.Smtp) + ":");


        #region Server

        /// <summary>
        /// 服务器键。
        /// </summary>
        public static readonly string ServerKey
            = KeyPrefix + nameof(Server);

        /// <summary>
        /// 默认服务器。
        /// </summary>
        public static readonly string DefaultServer = "smtp.qq.com";

        /// <summary>
        /// 服务器。
        /// </summary>
        public string Server { get; set; } = DefaultServer;

        #endregion


        #region Port

        /// <summary>
        /// 端口键。
        /// </summary>
        public static readonly string PortKey
            = KeyPrefix + nameof(Port);

        /// <summary>
        /// 默认端口。
        /// </summary>
        public static readonly int DefaultPort = 25;

        /// <summary>
        /// 端口。
        /// </summary>
        public int Port { get; set; } = DefaultPort;

        #endregion


        #region Nickname

        /// <summary>
        /// 昵称键。
        /// </summary>
        public static readonly string NicknameKey
            = KeyPrefix + nameof(Nickname);

        /// <summary>
        /// 默认昵称。
        /// </summary>
        public static readonly string DefaultNickname = "Librame Pang";

        /// <summary>
        /// 昵称。
        /// </summary>
        public string Nickname { get; set; } = DefaultNickname;

        #endregion


        #region Username

        /// <summary>
        /// 用户键。
        /// </summary>
        public static readonly string UsernameKey
            = KeyPrefix + nameof(Username);

        /// <summary>
        /// 默认用户。
        /// </summary>
        public static readonly string DefaultUsername = "test@qq.com";

        /// <summary>
        /// 用户。
        /// </summary>
        public string Username { get; set; } = DefaultUsername;

        #endregion


        #region Password

        /// <summary>
        /// 密码键。
        /// </summary>
        public static readonly string PasswordKey
            = KeyPrefix + nameof(Password);

        /// <summary>
        /// 默认密码。
        /// </summary>
        public static readonly string DefaultPassword = "test";

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; } = DefaultPassword;

        #endregion


        /// <summary>
        /// 安全套接字。
        /// </summary>
        public SecureSocketOptions SecureSocket { get; set; }
            = SecureSocketOptions.None;
    }


    /// <summary>
    /// 令牌选项。
    /// </summary>
    public class TokenOptions
    {
        /// <summary>
        /// 键名前缀。
        /// </summary>
        internal static readonly string KeyPrefix
            = (nameof(AuthenticationOptions.Token) + ":");

        
        #region Path

        /// <summary>
        /// 路径键。
        /// </summary>
        public static readonly string PathKey
            = KeyPrefix + nameof(Path);

        /// <summary>
        /// 默认路径。
        /// </summary>
        public static readonly string DefaultPath = "/token";

        /// <summary>
        /// 路径。
        /// </summary>
        public string Path { get; set; } = DefaultPath;

        #endregion


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
        public string Audience { get; set; } = DefaultAudience;

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
        public static readonly string DefaultLoginSuccessful = "http://localhost:10768/Account/Validate?token={token}";

        /// <summary>
        /// 登录成功后转向。
        /// </summary>
        public string LoginSuccessful { get; set; } = DefaultLoginSuccessful;

        #endregion


        /// <summary>
        /// 过期时间间隔（默认1天后过期）。
        /// </summary>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// 签名证书。
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

    }

}
