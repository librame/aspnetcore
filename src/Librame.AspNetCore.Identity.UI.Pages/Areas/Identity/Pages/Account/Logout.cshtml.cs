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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account
{
    using AspNetCore.UI;
    using Extensions;

    /// <summary>
    /// 登出页面模型。
    /// </summary>
    [AllowAnonymous]
    [GenericApplicationModel(typeof(LogoutPageModel<>))]
    public class LogoutPageModel : PageModel
    {
        /// <summary>
        /// 获取方法。
        /// </summary>
        public void OnGet()
        {
        }


        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        public virtual Task<IActionResult> OnPost(string returnUrl = null)
            => throw new NotImplementedException();
    }


    internal class LogoutPageModel<TUser> : LogoutPageModel where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly ILogger<LogoutPageModel> _logger;


        public LogoutPageModel(SignInManager<TUser> signInManager,
            ILogger<LogoutPageModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }


        public override async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync().ConfigureAndWaitAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                // This needs to be a redirect so that the browser performs a new
                // request and the identity for the user gets updated.
                return RedirectToPage();
            }
        }
    }
}
