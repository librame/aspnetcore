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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Applications;
    using Extensions;

    /// <summary>
    /// 重置验证器页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(ResetAuthenticatorPageModel<>))]
    public class ResetAuthenticatorPageModel : PageModel
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
    internal class ResetAuthenticatorPageModel<TUser> : ResetAuthenticatorPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly ILogger<ResetAuthenticatorPageModel> _logger;
        private readonly IStringLocalizer<StatusMessageResource> _statusLocalizer;


        public ResetAuthenticatorPageModel(
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager,
            ILogger<ResetAuthenticatorPageModel> logger,
            IStringLocalizer<StatusMessageResource> statusLocalizer)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _statusLocalizer = statusLocalizer;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }

        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        public override async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAwait();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _userManager.SetTwoFactorEnabledAsync(user, false).ConfigureAwait();
            await _userManager.ResetAuthenticatorKeyAsync(user).ConfigureAwait();
            var userId = await _userManager.GetUserIdAsync(user).ConfigureAwait();
            _logger.LogInformation("User with ID '{UserId}' has reset their authentication app key.", userId);

            await _signInManager.RefreshSignInAsync(user).ConfigureAwait();

            StatusMessage = _statusLocalizer.GetString(r => r.ResetAuthenticator)?.ToString();

            return RedirectToPage("./EnableAuthenticator");
        }
    }
}