#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.IdentityModel.Tokens;
using System;

namespace LibrameCore.Authentication
{
    /// <summary>
    /// 令牌生成选项。
    /// </summary>
    public class TokenGenerateOptions
    {
        /// <summary>
        /// 认证路径。
        /// </summary>
        public string Path { get; set; } = "/token";

        /// <summary>
        /// 认证方信息。
        /// </summary>
        public string Issuer { get; set; } = "http://server.librame.net";

        /// <summary>
        /// 使用方信息。
        /// </summary>
        public string Audience { get; set; } = "http://client.librame.net";

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
