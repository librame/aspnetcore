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
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account
{
    using AspNetCore.Identity.Web.Models;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Applications;
    using Extensions;

    /// <summary>
    /// 抽象双因子登入页面模型。
    /// </summary>
    [AllowAnonymous]
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(LoginWith2faPageModel<>))]
    public class LoginWith2faPageModel : PageModel
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
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ReturnUrl { get; set; }


        /// <summary>
        /// 异步获取方法。
        /// </summary>
        /// <param name="rememberMe">是否记住我。</param>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public virtual Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <param name="rememberMe">是否记住我。</param>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings")]
        public virtual Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class LoginWith2faPageModel<TUser> : LoginWith2faPageModel where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger<LoginWith2faPageModel> _logger;
        private readonly IStringLocalizer<ErrorMessageResource> _errorLocalizer;


        public LoginWith2faPageModel(
            SignInManager<TUser> signInManager,
            ILogger<LoginWith2faPageModel> logger,
            IStringLocalizer<ErrorMessageResource> errorLocalizer)
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _logger = logger;
            _errorLocalizer = errorLocalizer;
        }

        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        public override async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            // Ensure the user has gone through the username & password screen first
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            return Page();
        }

        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        public override async Task<IActionResult> OnPostAsync(bool rememberMe, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync().ConfigureAwait();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }
            
            var userId = await _userManager.GetUserIdAsync(user).ConfigureAwait();
            var authenticatorCode = Input.TwoFactorCode.CompatibleReplace(" ", string.Empty)
                .CompatibleReplace("-", string.Empty);

            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine).ConfigureAwait();
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
                ModelState.AddModelError(string.Empty, _errorLocalizer.GetString(r => r.InvalidAuthenticatorCode)?.ToString());
                return Page();
            }
        }

    }
}
