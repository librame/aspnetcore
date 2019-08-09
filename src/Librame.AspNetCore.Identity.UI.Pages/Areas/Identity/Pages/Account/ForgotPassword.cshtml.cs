#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account
{
    using AspNetCore.UI;
    using Extensions.Network;
    using Models;

    /// <summary>
    /// 抽象忘记密码页面模型。
    /// </summary>
    [AllowAnonymous]
    [PageApplicationModelWithUser(typeof(ForgotPasswordPageModel<>))]
    public abstract class AbstractForgotPasswordPageModel : PageModel
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractForgotPasswordPageModel"/> 实例。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionHtmlLocalizer{ForgotPasswordViewResource}"/>。</param>
        protected AbstractForgotPasswordPageModel(IExpressionHtmlLocalizer<ForgotPasswordViewResource> localizer)
        {
            Localizer = localizer;
        }


        /// <summary>
        /// 本地化资源。
        /// </summary>
        public IExpressionHtmlLocalizer<ForgotPasswordViewResource> Localizer { get; set; }

        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public ForgotPasswordViewModel Input { get; set; }

        /// <summary>
        /// 支持用户邮箱。
        /// </summary>
        public bool SupportsUserEmail { get; set; }


        /// <summary>
        /// 获取方法。
        /// </summary>
        public virtual void OnGet()
            => throw new NotImplementedException();

        /// <summary>
        /// 异步提交方法。
        /// </summary>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();
    }


    internal class ForgotPasswordPageModel<TUser> : AbstractForgotPasswordPageModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly IEmailService _emailService;

        public ForgotPasswordPageModel(
            UserManager<TUser> userManager,
            IEmailService emailSender,
            IExpressionHtmlLocalizer<ForgotPasswordViewResource> localizer)
            : base(localizer)
        {
            _userManager = userManager;
            _emailService = emailSender;
        }


        public override void OnGet()
        {
            SupportsUserEmail = _userManager.SupportsUserEmail;
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { token },
                    protocol: Request.Scheme);

                await _emailService.SendAsync(
                    Input.Email,
                    Localizer[r => r.ResetPassword]?.Value,
                    Localizer[r => r.ResetPasswordFormat, HtmlEncoder.Default.Encode(callbackUrl)]?.Value);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }

}
