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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Applications;
    using Extensions;

    /// <summary>
    /// 生成恢复码集合页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(GenerateRecoveryCodesPageModel<>))]
    public class GenerateRecoveryCodesPageModel : PageModel
    {
        /// <summary>
        /// 恢复码集合。
        /// </summary>
        [TempData]
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
        public string[] RecoveryCodes { get; set; }

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
    internal class GenerateRecoveryCodesPageModel<TUser> : GenerateRecoveryCodesPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger<GenerateRecoveryCodesPageModel> _logger;
        private readonly IStringLocalizer<StatusMessageResource> _statusLocalizer;


        public GenerateRecoveryCodesPageModel(
            UserManager<TUser> userManager,
            ILogger<GenerateRecoveryCodesPageModel> logger,
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

            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user).ConfigureAndResultAsync();
            if (!isTwoFactorEnabled)
            {
                var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
                throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' because they do not have 2FA enabled.");
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

            var isTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user).ConfigureAndResultAsync();
            var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
            if (!isTwoFactorEnabled)
            {
                throw new InvalidOperationException($"Cannot generate recovery codes for user with ID '{userId}' as they do not have 2FA enabled.");
            }

            var dependency = HttpContext.RequestServices.GetRequiredService<IdentityWebBuilderDependency>();
            var recoveryCodes = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user,
                dependency.TwoFactorRecoveryCodeLength).ConfigureAndResultAsync();

            RecoveryCodes = recoveryCodes.ToArray();
            _logger.LogInformation("User with ID '{UserId}' has generated new 2FA recovery codes.", userId);

            StatusMessage = _statusLocalizer.GetString(r => r.GenerateRecoveryCodes)?.ToString();
            return RedirectToPage("./ShowRecoveryCodes");
        }

    }
}
