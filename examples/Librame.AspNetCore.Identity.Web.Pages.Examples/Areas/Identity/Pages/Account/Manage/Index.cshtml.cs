using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Examples
{
    using AspNetCore.Identity.Stores;
    using AspNetCore.Identity.Web.Models;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Applications;
    using Extensions;
    using Extensions.Core.Services;
    using Extensions.Network.Services;

    /// <summary>
    /// 首页模型。
    /// </summary>
    public class IndexPageModel : ApplicationPageModel
    {
        [InjectionService]
        private IEmailService _emailService = null;

        [InjectionService]
        private IStringLocalizer<RegisterViewResource> _registerLocalizer = null;

        [InjectionService]
        private IStringLocalizer<StatusMessageResource> _statusLocalizer = null;

        [InjectionService]
        private SignInManager<DefaultIdentityUser<Guid, Guid>> _signInManager = null;


        private readonly UserManager<DefaultIdentityUser<Guid, Guid>> _userManager = null;


        /// <summary>
        /// 构造一个 <see cref="IndexPageModel"/>。
        /// </summary>
        /// <param name="injection">给定的 <see cref="IInjectionService"/>。</param>
        public IndexPageModel(IInjectionService injection)
            : base(injection)
        {
            _userManager = _signInManager.UserManager;
        }


        /// <summary>
        /// 是否电邮确认。
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 资料视图模型。
        /// </summary>
        public ProfileViewModel Profile { get; set; }

        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public UserViewModel Input { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                Profile = new ProfileViewModel
                {
                    HasPassword = await _userManager.HasPasswordAsync(user).ConfigureAndResultAsync(),
                    PhoneNumber = await _userManager.GetPhoneNumberAsync(user).ConfigureAndResultAsync(),
                    TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user).ConfigureAndResultAsync(),
                    Logins = await _userManager.GetLoginsAsync(user).ConfigureAndResultAsync(),
                    BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user).ConfigureAndResultAsync(),
                    AuthenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user).ConfigureAndResultAsync()
                };

                var userName = await _userManager.GetUserNameAsync(user).ConfigureAndResultAsync();
                var email = await _userManager.GetEmailAsync(user).ConfigureAndResultAsync();

                Input = new UserViewModel
                {
                    Name = userName,
                    Email = email
                };

                IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user).ConfigureAndResultAsync();
                return Page();
            })
            .ConfigureAndResultAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var email = await _userManager.GetEmailAsync(user).ConfigureAndResultAsync();
                if (Input.Email != email)
                {
                    var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email).ConfigureAndResultAsync();
                    if (!setEmailResult.Succeeded)
                    {
                        var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
                        throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                    }

                    // In our UI email and user name are one and the same, so when we update the email
                    // we need to update the user name.
                    var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.Email).ConfigureAndResultAsync();
                    if (!setUserNameResult.Succeeded)
                    {
                        var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
                        throw new InvalidOperationException($"Unexpected error occurred setting name for user with ID '{userId}'.");
                    }
                }

                await _signInManager.RefreshSignInAsync(user).ConfigureAndWaitAsync();

                StatusMessage = _statusLocalizer.GetString(r => r.ProfileUpdated)?.ToString();
                return RedirectToPage();
            })
            .ConfigureAndResultAsync();
        }

        public async Task<IActionResult> OnPostRemovePhoneNumberAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                    StatusMessage = "Remove phone number succeeded.";
                }
                else
                {
                    StatusMessage = result.Errors.FirstOrDefault()?.Description;
                }

                return RedirectToPage();
            })
            .ConfigureAndResultAsync();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
                var email = await _userManager.GetEmailAsync(user).ConfigureAndResultAsync();
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAndResultAsync();

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId, token },
                    protocol: Request.Scheme);

                await _emailService.SendAsync(
                    email,
                    _registerLocalizer.GetString(r => r.ConfirmYourEmail)?.ToString(),
                    _registerLocalizer.GetString(r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl))?.ToString()).ConfigureAndWaitAsync();

                StatusMessage = _statusLocalizer.GetString(r => r.VerificationEmailSent)?.ToString();
                return RedirectToPage();
            })
            .ConfigureAndResultAsync();
        }

        public async Task<IActionResult> OnPostEnableTwoFactorAuthenticationAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true).ConfigureAndWaitAsync();
                await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAndWaitAsync();
                //_logger.LogInformation(2, "User enabled two-factor authentication.");

                StatusMessage = "User enabled two-factor authentication.";
                return RedirectToAction(nameof(Index));
            })
            .ConfigureAndResultAsync();
        }

        public async Task<IActionResult> OnPostResetAuthenticatorKeyAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                await _userManager.ResetAuthenticatorKeyAsync(user).ConfigureAndResultAsync();
                //_logger.LogInformation(1, "User reset authenticator key.");

                StatusMessage = "User reset authenticator key.";
                return RedirectToAction(nameof(Index));
            })
            .ConfigureAndResultAsync();
        }

    }
}
