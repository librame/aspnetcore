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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account.Manage
{
    using AspNetCore.UI;
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 启用验证器页面模型。
    /// </summary>
    [ApplicationSiteTemplateWithUser(typeof(EnableAuthenticatorPageModel<>))]
    public class EnableAuthenticatorPageModel : PageModel
    {
        /// <summary>
        /// 共享密钥。
        /// </summary>
        public string SharedKey { get; set; }

        /// <summary>
        /// 验证器 URI。
        /// </summary>
        public string AuthenticatorUri { get; set; }

        /// <summary>
        /// 恢复码集合。
        /// </summary>
        [TempData]
        public string[] RecoveryCodes { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public EnableAuthenticatorViewModel Input { get; set; }


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
    }


    internal class EnableAuthenticatorPageModel<TUser> : EnableAuthenticatorPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly UrlEncoder _urlEncoder;
        private readonly ILogger<EnableAuthenticatorPageModel> _logger;
        private readonly IExpressionStringLocalizer<StatusMessageResource> _statusLocalizer;
        private readonly IExpressionStringLocalizer<ErrorMessageResource> _errorLocalizer;
        private readonly IdentityBuilderOptions _options;


        public EnableAuthenticatorPageModel(
            UserManager<TUser> userManager,
            UrlEncoder urlEncoder,
            ILogger<EnableAuthenticatorPageModel> logger,
            IExpressionStringLocalizer<StatusMessageResource> statusLocalizer,
            IExpressionStringLocalizer<ErrorMessageResource> errorLocalizer,
            IOptions<IdentityBuilderOptions> options)
        {
            _userManager = userManager;
            _urlEncoder = urlEncoder;
            _logger = logger;
            _statusLocalizer = statusLocalizer;
            _errorLocalizer = errorLocalizer;
            _options = options.Value;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadSharedKeyAndQrCodeUriAsync(user);

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadSharedKeyAndQrCodeUriAsync(user);
                return Page();
            }

            // Strip spaces and hypens
            var verificationCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var is2faTokenValid = await _userManager.VerifyTwoFactorTokenAsync(
                user, _userManager.Options.Tokens.AuthenticatorTokenProvider, verificationCode);

            if (!is2faTokenValid)
            {
                ModelState.AddModelError(nameof(Input.TwoFactorCode), _errorLocalizer[r => r.InvalidVerificationCode]);
                await LoadSharedKeyAndQrCodeUriAsync(user);
                return Page();
            }

            await _userManager.SetTwoFactorEnabledAsync(user, true);
            var userId = await _userManager.GetUserIdAsync(user);
            _logger.LogInformation("User with ID '{UserId}' has enabled 2FA with an authenticator app.", userId);

            StatusMessage = _statusLocalizer[r => r.EnableAuthenticator];

            if (await _userManager.CountRecoveryCodesAsync(user) == 0)
            {
                var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                RecoveryCodes = recoveryCodes.ToArray();
                return RedirectToPage("./ShowRecoveryCodes");
            }
            else
            {
                return RedirectToPage("./TwoFactorAuthentication");
            }
        }

        private async Task LoadSharedKeyAndQrCodeUriAsync(TUser user)
        {
            // Load the authenticator key & QR code URI to display on the form
            var unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(unformattedKey))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                unformattedKey = await _userManager.GetAuthenticatorKeyAsync(user);
            }

            SharedKey = FormatKey(unformattedKey);

            var email = await _userManager.GetEmailAsync(user);
            AuthenticatorUri = GenerateQrCodeUri(email, unformattedKey);
        }

        private string FormatKey(string unformattedKey)
        {
            var result = new StringBuilder();
            int currentPosition = 0;
            while (currentPosition + 4 < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition, 4)).Append(" ");
                currentPosition += 4;
            }
            if (currentPosition < unformattedKey.Length)
            {
                result.Append(unformattedKey.Substring(currentPosition));
            }

            return result.ToString().ToLowerInvariant();
        }

        private string GenerateQrCodeUri(string email, string unformattedKey)
        {
            // "Microsoft.AspNetCore.Identity.UI"
            var schemeName = _urlEncoder.Encode("Librame.AspNetCore.Identity.UI");
            email = _urlEncoder.Encode(email);

            var descriptor = new IdentityAuthenticatorDescriptor(schemeName, email, unformattedKey);
            var qrCodeUri = _options.AuthenticatorUriFactory?.Invoke(descriptor);
            if (qrCodeUri.IsNullOrEmpty())
                qrCodeUri = descriptor.BuildOtpAuthUriString();

            return qrCodeUri;
        }
    }
}
