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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Themepacks.Simple
{
    using Models;

    /// <summary>
    /// 错误页面模型。
    /// </summary>
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorPageModel : PageModel
    {
        /// <summary>
        /// 视图模型。
        /// </summary>
        [SuppressMessage("Performance", "CA1822:将成员标记为 static")]
        public ErrorViewModel ViewModel
            => new ErrorViewModel();


        /// <summary>
        /// 获取方法。
        /// </summary>
        public void OnGet()
        {
            ViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }

    }
}
