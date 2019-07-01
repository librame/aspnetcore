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
    using Models;
    using Extensions.Core;

    /// <summary>
    /// 抽象双因子登入页面模型。
    /// </summary>
    [AllowAnonymous]
    [ThemepackTemplate(typeof(LoginWith2faPageModel<>))]
    public abstract class AbstractLoginWith2faPageModel : PageModel
    {
        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public LoginWith2faViewModel Input { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public bool RememberMe { get; set; }

        /// <summary>
        /// 返回链接。
        /// </summary>
        public string ReturnUrl { get; set; }


        /// <summary>
        /// 异步获取方法。
        /// </summary>
        /// <param name="rememberMe">是否记住我。</param>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        public virtual Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <param name="rememberMe">是否记住我。</param>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        public virtual Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
            => throw new NotImplementedException();
    }


    internal class LoginWith2faPageModel<TUser> : AbstractLoginWith2faPageModel where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger<AbstractLoginWith2faPageModel> _logger;
        private readonly IExpressionStringLocalizer<ErrorMessageResource> _errorLocalizer;

        public LoginWith2faPageModel(
            SignInManager<TUser> signInManager,
            ILogger<AbstractLoginWith2faPageModel> logger,
            IExpressionStringLocalizer<ErrorMessageResource> errorLocalizer)
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _logger = logger;
            _errorLocalizer = errorLocalizer;
        }

        public override async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }
            
            var userId = await _userManager.GetUserIdAsync(user);
            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);
            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);
            if (result.Succeeded)
            {
                _logger.LogInformation("User with ID '{UserId}' logged in with 2fa.", userId);
                return LocalRedirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                _logger.LogWarning("User with ID '{UserId}' account locked out.", userId);
                return RedirectToPage("./Lockout");
            }
            else
            {
                _logger.LogWarning("Invalid authenticator code entered for user with ID '{UserId}'.", userId);
                ModelState.AddModelError(string.Empty, _errorLocalizer[r => r.InvalidAuthenticatorCode]?.ToString());
                return Page();
            }
        }

    }
}
