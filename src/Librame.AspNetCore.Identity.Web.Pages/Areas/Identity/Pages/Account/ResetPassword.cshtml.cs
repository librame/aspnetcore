#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account
{
    using AspNetCore.Web;
    using Builders;
    using Extensions;
    using Models;
    using Resources;

    /// <summary>
    /// 重置密码页面模型。
    /// </summary>
    [AllowAnonymous]
    [GenericApplicationModel(typeof(ResetPasswordPageModel<>))]
    public class ResetPasswordPageModel : PageModel
    {
        /// <summary>
        /// 构造一个 <see cref="RegisterPageModel"/> 实例。
        /// </summary>
        /// <param name="registerLocalizer">给定的 <see cref="IHtmlLocalizer{RegisterViewResource}"/>。</param>
        /// <param name="builderOptions">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityOptions}"/>。</param>
        protected ResetPasswordPageModel(IHtmlLocalizer<RegisterViewResource> registerLocalizer,
            IOptions<IdentityBuilderOptions> builderOptions, IOptions<IdentityOptions> options)
        {
            RegisterLocalizer = registerLocalizer;
            BuilderOptions = builderOptions.NotNull(nameof(builderOptions)).Value;
            Options = options.NotNull(nameof(options)).Value;
        }


        /// <summary>
        /// 本地化资源。
        /// </summary>
        public IHtmlLocalizer<RegisterViewResource> RegisterLocalizer { get; }

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
        public ResetPasswordViewModel Input { get; set; }


        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <param name="token">给定的令牌。</param>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        public virtual IActionResult OnGet(string token = null)
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ResetPasswordPageModel<TUser> : ResetPasswordPageModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;


        public ResetPasswordPageModel(
            UserManager<TUser> userManager,
            IHtmlLocalizer<RegisterViewResource> registerLocalizer,
            IOptions<IdentityBuilderOptions> builderOptions,
            IOptions<IdentityOptions> options)
            : base(registerLocalizer, builderOptions, options)
        {
            _userManager = userManager;
        }


        public override IActionResult OnGet(string token = null)
        {
            if (token == null)
            {
                return BadRequest("A token must be supplied for password reset.");
            }
            else
            {
                Input = new ResetPasswordViewModel
                {
                    Code = token
                };

                return Page();
            }
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Input.Email).ConfigureAndResultAsync();
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password).ConfigureAndResultAsync();
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

    }
}
