#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Pages.Account
{
    /// <summary>
    /// 锁定页面模型。
    /// </summary>
    [AllowAnonymous]
    public class LockoutPageModel : PageModel
    {
        /// <summary>
        /// 获取方法。
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void OnGet()
        {
        }

    }
}
