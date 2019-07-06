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

    /// <summary>
    /// ����ǳ�ҳ��ģ�͡�
    /// </summary>
    [AllowAnonymous]
    [ThemepackTemplate(typeof(LogoutPageModel<>))]
    public abstract class AbstractLogoutPageModel : PageModel
    {
        /// <summary>
        /// ��ȡ������
        /// </summary>
        public void OnGet()
        {
        }


        /// <summary>
        /// �첽�ύ������
        /// </summary>
        /// <param name="returnUrl">�����ķ��� URL��</param>
        /// <returns>����һ������ <see cref="IActionResult"/> ���첽������</returns>
        public virtual Task<IActionResult> OnPost(string returnUrl = null)
            => throw new NotImplementedException();
    }


    internal class LogoutPageModel<TUser> : AbstractLogoutPageModel where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly ILogger<AbstractLogoutPageModel> _logger;

        public LogoutPageModel(SignInManager<TUser> signInManager,
            ILogger<AbstractLogoutPageModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public override async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
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