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
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account.Manage
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// 设置密码页面模型。
    /// </summary>
    [InterfaceTemplateWithUser(typeof(SetPasswordPageModel<>))]
    public class SetPasswordPageModel : PageModel
    {
        /// <summary>
        /// 构造一个 <see cref="SetPasswordPageModel"/>。
        /// </summary>
        /// <param name="registerLocalizer">给定的 <see cref="IExpressionHtmlLocalizer{RegisterViewResource}"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityOptions}"/>。</param>
        public SetPasswordPageModel(IExpressionHtmlLocalizer<RegisterViewResource> registerLocalizer,
            IOptions<IdentityBuilderOptions> builderOptions, IOptions<IdentityOptions> options)
        {
            RegisterLocalizer = registerLocalizer;
            BuilderOptions = builderOptions.Value;
            Options = options.Value;
        }


        /// <summary>
        /// 本地化资源。
        /// </summary>
        public IExpressionHtmlLocalizer<RegisterViewResource> RegisterLocalizer { get; }

        /// <summary>
        /// 构建器选项。
        /// </summary>
        public IdentityBuilderOptions BuilderOptions { get; }

        /// <summary>
        /// 选项。
        /// </summary>
        public IdentityOptions Options { get; }

        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public SetPasswordViewModel Input { get; set; }

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


    internal class SetPasswordPageModel<TUser> : SetPasswordPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly SignInManager<TUser> _signInManager;
        private readonly IExpressionStringLocalizer<StatusMessageResource> _statusLocalizer;


        public SetPasswordPageModel(
            UserManager<TUser> userManager,
            SignInManager<TUser> signInManager,
            IExpressionStringLocalizer<StatusMessageResource> statusLocalizer,
            IExpressionHtmlLocalizer<RegisterViewResource> registerLocalizer,
            IOptions<IdentityBuilderOptions> builderOptions,
            IOptions<IdentityOptions> options)
            : base(registerLocalizer, builderOptions, options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _statusLocalizer = statusLocalizer;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToPage("./ChangePassword");
            }

            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
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

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);

            StatusMessage = _statusLocalizer[r => r.SetPassword]?.ToString();

            return RedirectToPage();
        }
    }
}
