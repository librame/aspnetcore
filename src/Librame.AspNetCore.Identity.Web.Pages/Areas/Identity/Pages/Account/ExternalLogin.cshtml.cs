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
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account
{
    using AspNetCore.Web;
    using AspNetCore.Web.Utilities;
    using Extensions;
    using Extensions.Data.Stores;
    using Models;
    using Resources;
    using Stores;

    /// <summary>
    /// 外部登入确认页面模型。
    /// </summary>
    [AllowAnonymous]
    [GenericApplicationModel(typeof(ExternalLoginPageModel<>))]
    public class ExternalLoginPageModel : PageModel
    {
        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public ExternalLoginConfirmationViewModel Input { get; set; }

        /// <summary>
        /// 登入提供程序名称。
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// 返回链接。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 错误消息。
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }


        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        public virtual IActionResult OnGet()
            => throw new NotImplementedException();

        /// <summary>
        /// 提交方法。
        /// </summary>
        /// <param name="provider">给定的提供程序。</param>
        /// <param name="returnUrl">给定的返回链接。</param>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public virtual IActionResult OnPost(string provider, string returnUrl = null)
            => throw new NotImplementedException();

        /// <summary>
        /// 异步获取回调方法。
        /// </summary>
        /// <param name="returnUrl">给定的返回链接。</param>
        /// <param name="remoteError">给定的远程错误。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public virtual Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交确认方法。
        /// </summary>
        /// <param name="returnUrl">给定的返回链接。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public virtual Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ExternalLoginPageModel<TUser> : ExternalLoginPageModel
        where TUser : class, IId<string>
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly IUserStore<TUser> _userStore;
        private readonly ILogger<ExternalLoginPageModel> _logger;
        private readonly IStringLocalizer<ErrorMessageResource> _errorLocalizer;
        private readonly IdentityStoreIdentifier _storeIdentifier;


        public ExternalLoginPageModel(
            SignInManager<TUser> signInManager,
            IUserStore<TUser> userStore,
            ILogger<ExternalLoginPageModel> logger,
            IStringLocalizer<ErrorMessageResource> errorLocalizer,
            IdentityStoreIdentifier storeIdentifier)
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _userStore = userStore;
            _logger = logger;
            _errorLocalizer = errorLocalizer;
            _storeIdentifier = storeIdentifier;
        }


        public override IActionResult OnGet()
        {
            return RedirectToPage("./Login");
        }

        public override IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public override async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = _errorLocalizer.GetString(r => r.FromExternalProvider, remoteError)?.ToString();
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync().ConfigureAndResultAsync();
            if (info == null)
            {
                ErrorMessage = _errorLocalizer.GetString(r => r.LoadingExternalLogin)?.ToString();
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true).ConfigureAndResultAsync();
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;

                Input = new ExternalLoginConfirmationViewModel();

                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }

                return Page();
            }
        }

        public override async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync().ConfigureAndResultAsync();
            if (info == null)
            {
                ErrorMessage = _errorLocalizer.GetString(r => r.LoadingExternalLoginWhenConfirmation)?.ToString();
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.Id = await _storeIdentifier.GetUserIdAsync().ConfigureAndResultAsync();

                var result = await SignInManagerUtility.CreateUserByEmail(_userManager, _userStore, Input.Email, password: null, user).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info).ConfigureAndResultAsync();
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        private TUser CreateUser()
        {
            try
            {
                return typeof(TUser).EnsureCreate<TUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(TUser)}'. " +
                    $"Ensure that '{nameof(TUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in ~/Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }

    }

}
