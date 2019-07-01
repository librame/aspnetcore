#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace Librame.AspNetCore.Identity.UI.Pages
{
    /// <summary>
    /// 错误页面模型。
    /// </summary>
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorPageModel : PageModel
    {
        /// <summary>
        /// 请求标识。
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 显示请求标识。
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);


        /// <summary>
        /// 获取方法。
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

    }
}
