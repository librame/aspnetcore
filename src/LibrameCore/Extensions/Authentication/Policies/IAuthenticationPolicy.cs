#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;

namespace LibrameCore.Extensions.Authentication
{
    using Managers;
    
    /// <summary>
    /// 认证策略接口。
    /// </summary>
    public interface IAuthenticationPolicy
    {
        /// <summary>
        /// 令牌管理器。
        /// </summary>
        ITokenManager TokenManager { get; }

        /// <summary>
        /// Cookie 选项。
        /// </summary>
        CookieAuthenticationOptions CookieOptions { get; }

        /// <summary>
        /// 认证选项。
        /// </summary>
        AuthenticationExtensionOptions Options { get; }


        /// <summary>
        /// 认证身份。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回认证状态与身份标识。</returns>
        (AuthenticationStatus Status, LibrameIdentity Identity) Authenticate(HttpContext context);


        /// <summary>
        /// 添加 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <param name="expires">给定的过期时间。</param>
        /// <param name="token">给定的令牌。</param>
        /// <returns>返回令牌。</returns>
        string AddCookieToken(HttpContext context, DateTimeOffset expires, string token);


        /// <summary>
        /// 删除 Cookie 令牌。
        /// </summary>
        /// <param name="context">给定的 HTTP 上下文。</param>
        /// <returns>返回令牌。</returns>
        string DeleteCookieToken(HttpContext context);
    }
}
