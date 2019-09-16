﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
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
        dynamic GetSignedUser(HttpContext context);

        /// <summary>
        /// 获取已登入用户标识。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        string GetSignedUserId(HttpContext context);

        /// <summary>
        /// 获取已登入用户名称。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        string GetSignedUserName(HttpContext context);

        /// <summary>
        /// 获取已登入用户电邮。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        string GetSignedUserEmail(HttpContext context);

        /// <summary>
        /// 获取已登入用户电话号码。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        string GetSignedUserPhoneNumber(HttpContext context);

        /// <summary>
        /// 获取已登入用户头像。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回字符串。</returns>
        string GetSignedUserPortrait(HttpContext context);

        /// <summary>
        /// 获取已登入用户角色列表。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回 <see cref="IList{String}"/>。</returns>
        IList<string> GetSignedUserRoles(HttpContext context);
    }
}