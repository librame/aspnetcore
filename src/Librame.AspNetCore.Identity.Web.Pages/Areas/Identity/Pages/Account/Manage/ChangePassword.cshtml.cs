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
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Web.Models;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Applications;
    using Extensions;
    using Extensions.Core.Services;

    /// <summary>
    /// 修改密码页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(ChangePasswordPageModel<>))]
    public class ChangePasswordPageModel : ApplicationPageModel
    {
        /// <summary>
        /// 构造一个 <see cref="ChangePasswordPageModel"/>。
        /// </summary>
        /// <param name="injection">给定的 <see cref="IInjectionService"/>。</param>
        public ChangePasswordPageModel(IInjectionService injection)
            : base(injection)
        {
        }


        /// <summary>
        /// 本地化资源。
        /// </summary>
        [InjectionService]
        public IHtmlLocalizer<RegisterViewResource> RegisterLocalizer { get; set; }

        /// <summary>
        /// 构建器选项。
        /// </summary>
        [InjectionService]
        public IOptions<IdentityBuilderOptions> BuilderOptions { get; set; }

        /// <summary>
        /// 选项。
        /// </summary>
        [InjectionService]
        public IOptions<IdentityOptions> Options { get; set; }

        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public ChangePasswordViewModel Input { get; set; }


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
    internal class ChangePasswordPageModel<TUser> : ChangePasswordPageModel
        where TUser : class
    {
        [InjectionService]
        private SignInManager<TUser> _signInManager = null;

        [InjectionService]
        private ILogger<ChangePasswordPageModel> _logger = null;

        [InjectionService]
        private IStringLocalizer<StatusMessageResource> _statusLocalizer = null;


        private readonly UserManager<TUser> _userManager;


        public ChangePasswordPageModel(IInjectionService injection)
            : base(injection)
        {
            _userManager = _signInManager.UserManager;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var hasPassword = await _userManager.HasPasswordAsync(user).ConfigureAwait();
                if (!hasPassword)
                {
                    return RedirectToPage("./SetPassword");
                }

                return Page();
            })
            .ConfigureAwait();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return await VerifyLoginUserActionResult(_userManager, async user =>
            {
                var result = await _userManager.ChangePasswordAsync(user,
                    Input.OldPassword, Input.NewPassword).ConfigureAwait();

                if (!result.Succeeded)
                {
                    AddModelErrors(result);
                    return Page();
                }

                await _signInManager.RefreshSignInAsync(user).ConfigureAwait();
                _logger.LogInformation("User changed their password successfully.");

                StatusMessage = _statusLocalizer.GetString(r => r.ChangePassword);
                return RedirectToPage();
            })
            .ConfigureAwait();
        }

    }
}
