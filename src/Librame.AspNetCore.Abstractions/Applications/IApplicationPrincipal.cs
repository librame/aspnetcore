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
using System;

namespace Librame.AspNetCore
{
    /// <summary>
    /// 应用程序当事人接口。
    /// </summary>
    public interface IApplicationPrincipal
    {
        /// <summary>
        /// 是否已登入。
        /// </summary>
        Func<HttpContext, bool> IsSignedIn { get; set; }
        
        /// <summary>
        /// 用户。
        /// </summary>
        Func<HttpContext, object> User { get; set; }

        /// <summary>
        /// 用户邮箱。
        /// </summary>
        Func<HttpContext, string> UserEmail { get; set; }

        /// <summary>
        /// 用户名称。
        /// </summary>
        Func<HttpContext, string> UserName { get; set; }
    }
}
