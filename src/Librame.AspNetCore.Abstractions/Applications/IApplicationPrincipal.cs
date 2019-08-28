#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Librame.AspNetCore
{
    /// <summary>
    /// 应用当事人接口。
    /// </summary>
    public interface IApplicationPrincipal
    {
        /// <summary>
        /// 是否已登入。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        bool IsSignedIn(HttpContext context);


        /// <summary>
        /// 异步获取已登入用户。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个包含用户的异步操作。</returns>
        Task<dynamic> GetSignedUserAsync(HttpContext context);


        /// <summary>
        /// 获取已登入用户标识。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        string GetSignedUserId(HttpContext context);

        /// <summary>
        /// 异步获取已登入用户标识。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个包含字符串的异步操作。</returns>
        Task<string> GetSignedUserIdAsync(HttpContext context);


        /// <summary>
        /// 获取已登入用户名。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        string GetSignedUserName(HttpContext context);

        /// <summary>
        /// 异步获取已登入用户名。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个包含字符串的异步操作。</returns>
        Task<string> GetSignedUserNameAsync(HttpContext context);


        /// <summary>
        /// 异步获取已登入用户电邮。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个包含字符串的异步操作。</returns>
        Task<string> GetSignedUserEmailAsync(HttpContext context);
    }
}
