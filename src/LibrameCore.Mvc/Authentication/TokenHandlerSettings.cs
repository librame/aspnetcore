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

namespace LibrameStandard.Authentication
{
    using Handlers;

    /// <summary>
    /// 令牌处理程序设置。
    /// </summary>
    public class TokenHandlerSettings : HandlerSettings
    {
        /// <summary>
        /// 构造一个令牌处理程序设置。
        /// </summary>
        public TokenHandlerSettings()
            : base("/token")
        {
        }


        /// <summary>
        /// 认证方信息。
        /// </summary>
        public string Issuer { get; set; } = "http://localhost:10768/";

        /// <summary>
        /// 使用方信息。
        /// </summary>
        public string Audience { get; set; } = "http://localhost:10768/";

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
