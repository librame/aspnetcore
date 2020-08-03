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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account
{
    using AspNetCore.Web.Applications;
    using Extensions;

    /// <summary>
    /// 登出页面模型。
    /// </summary>
    [AllowAnonymous]
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(LogoutPageModel<>))]
    public class LogoutPageModel : PageModel
    {
        /// <summary>
        /// 获取方法。
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public void OnGet()
        {
        }


        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public virtual Task<IActionResult> OnPost(string returnUrl = null)
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
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

        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        public override async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync().ConfigureAwait();
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
