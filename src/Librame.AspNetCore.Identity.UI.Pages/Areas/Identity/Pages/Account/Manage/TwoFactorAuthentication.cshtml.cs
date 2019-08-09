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
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account.Manage
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// 抽象双因子验证页面模型。
    /// </summary>
    [PageApplicationModelWithUser(typeof(TwoFactorAuthenticationPageModel<>))]
    public abstract class AbstractTwoFactorAuthenticationPageModel : PageModel
    {
        /// <summary>
        /// 包含验证器。
        /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
        /// 剩余恢复码数。
        /// </summary>
        public int RecoveryCodesLeft { get; set; }

        /// <summary>
        /// 是否启用双因子验证。
        /// </summary>
        [BindProperty]
        public bool Is2faEnabled { get; set; }

        /// <summary>
        /// 是否记住此设备。
        /// </summary>
        public bool IsMachineRemembered { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


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

    internal class TwoFactorAuthenticationPageModel<TUser> : AbstractTwoFactorAuthenticationPageModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly ILogger<AbstractTwoFactorAuthenticationPageModel> _logger;
        private readonly IExpressionStringLocalizer<StatusMessageResource> _statusLocalizer;

        public TwoFactorAuthenticationPageModel(
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager,
            ILogger<AbstractTwoFactorAuthenticationPageModel> logger,
            IExpressionStringLocalizer<StatusMessageResource> statusLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _statusLocalizer = statusLocalizer;
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _signInManager.ForgetTwoFactorClientAsync();

            StatusMessage = _statusLocalizer[r => r.TwoFactorAuthentication]?.ToString();

            return RedirectToPage();
        }
    }
}
