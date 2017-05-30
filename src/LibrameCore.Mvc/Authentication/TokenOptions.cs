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
    using Handlers;

    /// <summary>
    /// 令牌选项。
    /// </summary>
    public class TokenOptions : HandlerOptions
    {
        /// <summary>
        /// 构造一个令牌选项。
        /// </summary>
        public TokenOptions()
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
