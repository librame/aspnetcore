using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Librame.AspNetCore.Identity.UI.Pages.Examples.Areas.Identity.Pages.Account.Manage
{
    using Extensions.Core;
    using Extensions.Network;
    using UI;

    /// <summary>
    /// 首页模型。
    /// </summary>
    public class IndexModel : PageModel
    {
        [InjectionService]
        private UserManager<DefaultIdentityUser> _userManager = null;
        [InjectionService]
        private SignInManager<DefaultIdentityUser> _signInManager = null;
        [InjectionService]
        private IEmailService _emailService = null;
        //[InjectionService]
        //private ISmsService _smsService = null;
        [InjectionService]
        private IExpressionStringLocalizer<RegisterViewResource> _registerLocalizer = null;
        [InjectionService]
        private IExpressionStringLocalizer<StatusMessageResource> _statusLocalizer = null;

        public IndexModel(IInjectionService injectionService)
        {
            injectionService.Inject(this);
        }
        
        /// <summary>
        /// 是否邮箱确认。
        /// </summary>
        public bool IsEmailConfirmed { get; set; }

        /// <summary>
        /// 是否已确认电话。
        /// </summary>
        public bool IsPhoneConfirmed { get; set; }

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
        [ViewResourceMapping("Index")]
        public class InputModel : Models.IndexViewModel
        {
            [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
            [Range(0, 199, ErrorMessageResourceName = nameof(RangeAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
            [Display(Name = nameof(Age))]
            public int Age { get; set; } = 18;
        }


        /// <summary>
        /// 异步获取方法。
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userName = await _userManager.GetUserNameAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Input = new InputModel
            {
                Name = userName,
                Email = email,
                Phone = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            IsPhoneConfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);

            return Page();
        }

        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.Name != user.UserName)
            {
                user.UserName = Input.Name;
            }

            var updateProfileResult = await _userManager.UpdateAsync(user);
            if (!updateProfileResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error ocurred updating the profile for user with ID '{user.Id}'");
            }

            if (Input.Email != user.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            if (Input.Phone != user.PhoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.Phone);
                if (!setPhoneResult.Succeeded)
                {
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = _statusLocalizer[r => r.ProfileUpdated];

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { user.Id, token },
                protocol: Request.Scheme);

            await _emailService.SendAsync(
                email,
                _registerLocalizer[r => r.ConfirmYourEmail],
                _registerLocalizer[r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl)]);

            StatusMessage = _statusLocalizer[r => r.VerificationEmailSent];

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSendVerificationPhoneAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var email = await _userManager.GetEmailAsync(user);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            //var callbackUrl = Url.Page(
            //    "/Account/ConfirmPhone",
            //    pageHandler: null,
            //    values: new { userId, token },
            //    protocol: Request.Scheme);

            //await _smsSender.SendAsync(token);

            //await _emailSender.SendAsync(
            //    email,
            //    _registerLocalizer[r => r.ConfirmYourEmail],
            //    _registerLocalizer[r => r.ConfirmYourEmailFormat, HtmlEncoder.Default.Encode(callbackUrl)]);

            StatusMessage = _statusLocalizer[r => r.VerificationSmsSent];

            return RedirectToPage();
        }
    }
}
