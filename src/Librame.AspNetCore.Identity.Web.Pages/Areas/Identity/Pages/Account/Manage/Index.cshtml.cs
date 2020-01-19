#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Web;
    using Extensions;
    using Extensions.Network.Services;
    using Resources;

    /// <summary>
    /// 首页页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(IndexPageModel<>))]
    public class IndexPageModel : PageModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 是否已确认电邮。
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// 输入模型。
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// 电邮。
            /// </summary>
            [EmailAddress(ErrorMessageResourceName = nameof(EmailAddressAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
            [Display(Name = nameof(Email), ResourceType = typeof(UserViewModelResource))]
            public string Email { get; set; }

            /// <summary>
            /// 电话。
            /// </summary>
            //[Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
            [Display(Name = nameof(Phone), ResourceType = typeof(UserViewModelResource))]
            public string Phone { get; set; }
        }


        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnGetAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 提交方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 提交发送电邮验证码方法。
        /// </summary>
        /// <returns></returns>
        public virtual Task<IActionResult> OnPostSendVerificationEmailAsync()
            => throw new NotImplementedException();

        /// <summary>
        /// 提交发送手机验证码方法。
        /// </summary>
        /// <returns></returns>
        public virtual Task<IActionResult> OnPostSendVerificationPhoneAsync()
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IndexPageModel<TUser> : IndexPageModel where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly IEmailService _emailService;
        //private readonly ISmsService _smsService;
        private readonly IStringLocalizer<RegisterViewResource> _registerLocalizer;
        private readonly IStringLocalizer<StatusMessageResource> _statusLocalizer;


        public IndexPageModel(SignInManager<TUser> signInManager,
            IEmailService emailService,
            //ISmsService smsService,
            IStringLocalizer<RegisterViewResource> registerLocalizer,
            IStringLocalizer<StatusMessageResource> statusLocalizer)
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _emailService = emailService;
            //_smsService = smsService;
            _registerLocalizer = registerLocalizer;
            _statusLocalizer = statusLocalizer;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //var userName = await _userManager.GetUserNameAsync(user).ConfigureAndResultAsync();
            var email = await _userManager.GetEmailAsync(user).ConfigureAndResultAsync();
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user).ConfigureAndResultAsync();
            
            Input = new InputModel
            {
                Email = email,
                Phone = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user).ConfigureAndResultAsync();

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

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

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user).ConfigureAndResultAsync();
            if (Input.Phone != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.Phone).ConfigureAndResultAsync();
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user).ConfigureAndWaitAsync();

            StatusMessage = _statusLocalizer.GetString(r => r.ProfileUpdated)?.ToString();

            return RedirectToPage();
        }

        public override async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
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
        }

    }
}
