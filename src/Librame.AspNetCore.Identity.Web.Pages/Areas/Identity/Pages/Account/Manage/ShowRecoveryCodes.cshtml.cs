#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    /// <summary>
    /// 显示恢复码页面模型。
    /// </summary>
    public class ShowRecoveryCodesPageModel : PageModel
    {
        /// <summary>
        /// 恢复码集合。
        /// </summary>
        [TempData]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public string[] RecoveryCodes { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <returns></returns>
        public IActionResult OnGet()
        {
            if (RecoveryCodes == null || RecoveryCodes.Length == 0)
            {
                return RedirectToPage("./TwoFactorAuthentication");
            }

            return Page();
        }

    }
}
