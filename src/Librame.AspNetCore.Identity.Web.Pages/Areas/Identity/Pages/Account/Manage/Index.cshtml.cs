#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Identity.Web.Models;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Applications;
    using Extensions;
    using Extensions.Core.Services;
    using Extensions.Network.Services;

    /// <summary>
    /// 首页页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(IndexPageModel<>))]
    public class IndexPageModel : ApplicationPageModel
    {
        /// <summary>
        /// 构造一个 <see cref="ApplicationPageModel"/>。
        /// </summary>
        /// <param name="injection">给定的 <see cref="IInjectionService"/>。</param>
        public IndexPageModel(IInjectionService injection)
            : base(injection)
        {
        }


        /// <summary>
        /// 是否已确认电邮。
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 资料视图模型。
        /// </summary>
        public ProfileViewModel Profile { get; set; }

        /// <summary>
        /// 用户视图模型。
        /// </summary>
        [BindProperty]
        public UserViewModel Input { get; set; }


        /// <summary>
        /// 异步获取方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnGetAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交移除手机号码。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnPostRemovePhoneNumberAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交发送电邮验证码方法。
        /// </summary>
        /// <returns></returns>
        public virtual Task<IActionResult> OnPostSendVerificationEmailAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交发送手机验证码方法。
        /// </summary>
        /// <returns></returns>
        public virtual Task<IActionResult> OnPostSendVerificationPhoneAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交启用双因子认证方法。
        /// </summary>
        /// <returns></returns>
        public virtual Task<IActionResult> OnPostEnableTwoFactorAuthenticationAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交重置认证器密钥方法。
        /// </summary>
        /// <returns></returns>
        public virtual Task<IActionResult> OnPostResetAuthenticatorKeyAsync()
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IndexPageModel<TUser> : IndexPageModel where TUser : class
    {
        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        [InjectionService]
        private IEmailService _emailService = null;

        [InjectionService]
        private IStringLocalizer<RegisterViewResource> _registerLocalizer = null;

        [InjectionService]
        private IStringLocalizer<StatusMessageResource> _statusLocalizer = null;


        private readonly UserManager<TUser> _userManager;


        public IndexPageModel(IInjectionService injection)
            : base(injection)
        {
            _userManager = _signInManager?.UserManager;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                Profile = new ProfileViewModel
                {
                    HasPassword = await _userManager.HasPasswordAsync(user).ConfigureAwait(),
                    PhoneNumber = await _userManager.GetPhoneNumberAsync(user).ConfigureAwait(),
                    TwoFactor = await _userManager.GetTwoFactorEnabledAsync(user).ConfigureAwait(),
                    Logins = await _userManager.GetLoginsAsync(user).ConfigureAwait(),
                    BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user).ConfigureAwait(),
                    AuthenticatorKey = await _userManager.GetAuthenticatorKeyAsync(user).ConfigureAwait()
                };

                var userName = await _userManager.GetUserNameAsync(user).ConfigureAwait();
                var email = await _userManager.GetEmailAsync(user).ConfigureAwait();

                Input = new UserViewModel
                {
                    Name = userName,
                    Email = email
                };

                IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user).ConfigureAwait();
                return Page();
            })
            .ConfigureAwait();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var email = await _userManager.GetEmailAsync(user).ConfigureAwait();
                if (Input.Email != email)
                {
                    var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email).ConfigureAwait();
                    if (!setEmailResult.Succeeded)
                    {
                        var userId = await _userManager.GetUserIdAsync(user).ConfigureAwait();
                        throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                    }

                    // In our UI email and user name are one and the same, so when we update the email
                    // we need to update the user name.
                    var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.Email).ConfigureAwait();
                    if (!setUserNameResult.Succeeded)
                    {
                        var userId = await _userManager.GetUserIdAsync(user).ConfigureAwait();
                        throw new InvalidOperationException($"Unexpected error occurred setting name for user with ID '{userId}'.");
                    }
                }

                await _signInManager.RefreshSignInAsync(user).ConfigureAwait();

                StatusMessage = _statusLocalizer.GetString(r => r.ProfileUpdated)?.ToString();
                return RedirectToPage();
            })
            .ConfigureAwait();
        }

        public override async Task<IActionResult> OnPostRemovePhoneNumberAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var result = await _userManager.SetPhoneNumberAsync(user, null).ConfigureAwait();
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait();
                    StatusMessage = "Remove phone number succeeded.";
                }
                else
                {
                    StatusMessage = result.Errors.FirstOrDefault()?.Description;
                }

                return RedirectToPage();
            })
            .ConfigureAwait();
        }

        public override async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var userId = await _userManager.GetUserIdAsync(user).ConfigureAwait();
                var email = await _userManager.GetEmailAsync(user).ConfigureAwait();
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait();

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { userId, token },
                    protocol: Request.Scheme);

                await _emailService.SendAsync(
                    email,
                    _registerLocalizer.GetString(r => r.ConfirmYourEmail)?.ToString(),
                    _registerLocalizer.GetString(r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl))?.ToString()).ConfigureAwait();

                StatusMessage = _statusLocalizer.GetString(r => r.VerificationEmailSent)?.ToString();
                return RedirectToPage();
            })
            .ConfigureAwait();
        }

        public override async Task<IActionResult> OnPostEnableTwoFactorAuthenticationAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                await _userManager.SetTwoFactorEnabledAsync(user, true).ConfigureAwait();
                await _signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait();
                //_logger.LogInformation(2, "User enabled two-factor authentication.");

                StatusMessage = "User enabled two-factor authentication.";
                return RedirectToAction(nameof(Index));
            })
            .ConfigureAwait();
        }

        public override async Task<IActionResult> OnPostResetAuthenticatorKeyAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                await _userManager.ResetAuthenticatorKeyAsync(user).ConfigureAwait();
                //_logger.LogInformation(1, "User reset authenticator key.");

                StatusMessage = "User reset authenticator key.";
                return RedirectToAction(nameof(Index));
            })
            .ConfigureAwait();
        }

    }
}
