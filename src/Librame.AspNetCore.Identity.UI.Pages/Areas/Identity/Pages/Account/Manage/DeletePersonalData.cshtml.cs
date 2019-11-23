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
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account.Manage
{
    using AspNetCore.UI;
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 删除个人数据页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(DeletePersonalDataPageModel<>))]
    public class DeletePersonalDataPageModel : PageModel
    {
        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public DeletePersonalDataViewModel Input { get; set; }

        /// <summary>
        /// 强制密码。
        /// </summary>
        public bool RequirePassword { get; set; }


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


    internal class DeletePersonalDataPageModel<TUser> : DeletePersonalDataPageModel
        where TUser: class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly ILogger<DeletePersonalDataPageModel> _logger;
        private readonly IStringLocalizer<ErrorMessageResource> _errorLocalizer;


        public DeletePersonalDataPageModel(
            SignInManager<TUser> signInManager,
            ILogger<DeletePersonalDataPageModel> logger,
            IStringLocalizer<ErrorMessageResource> errorLocalizer)
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _logger = logger;
            _errorLocalizer = errorLocalizer;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user).ConfigureAndResultAsync();
            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user).ConfigureAndResultAsync();
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password).ConfigureAndResultAsync())
                {
                    ModelState.AddModelError(string.Empty, _errorLocalizer.GetString(r => r.PasswordNotCorrect));
                    return Page();
                }
            }

            var result = await _userManager.DeleteAsync(user).ConfigureAndResultAsync();
            var userId = await _userManager.GetUserIdAsync(user).ConfigureAndResultAsync();
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleteing user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync().ConfigureAndWaitAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
