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
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Web;
    using Extensions;
    using Resources;

    /// <summary>
    /// 禁用双因子验证页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(Disable2faPageModel<>))]
    public class Disable2faPageModel : PageModel
    {
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


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class Disable2faPageModel<TUser> : Disable2faPageModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger<Disable2faPageModel> _logger;
        private readonly IStringLocalizer<StatusMessageResource> _statusLocalizer;


        public Disable2faPageModel(
            UserManager<TUser> userManager,
            ILogger<Disable2faPageModel> logger,
            IStringLocalizer<StatusMessageResource> statusLocalizer)
        {
            _userManager = userManager;
            _logger = logger;
            _statusLocalizer = statusLocalizer;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!await _userManager.GetTwoFactorEnabledAsync(user).ConfigureAndResultAsync())
            {
                throw new InvalidOperationException($"Cannot disable 2FA for user with ID '{_userManager.GetUserId(User)}' as it's not currently enabled.");
            }

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var disable2faResult = await _userManager.SetTwoFactorEnabledAsync(user, false).ConfigureAndResultAsync();
            if (!disable2faResult.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred disabling 2FA for user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' has disabled 2fa.", _userManager.GetUserId(User));

            StatusMessage = _statusLocalizer.GetString(r => r.DisableTwoFactor)?.ToString();

            return RedirectToPage("./TwoFactorAuthentication");
        }
    }
}