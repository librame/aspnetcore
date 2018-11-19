#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account
{
    using Models.AccountViewModels;

    /// <summary>
    /// 抽象登录模型。
    /// </summary>
    [AllowAnonymous]
    [InternalUIIdentity(typeof(LoginModel<>))]
    public abstract class AbstractLoginModel : PageModel
    {
        /// <summary>
        /// 输入视图模型。
        /// </summary>
        [BindProperty]
        public LoginViewModel Input { get; set; }

        /// <summary>
        /// 外部登录方案列表。
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// 返回 URL。
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 错误消息。
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }


        /// <summary>
        /// 异步获取任务。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个异步操作。</returns>
        public virtual Task OnGetAsync(string returnUrl = null) => throw new NotImplementedException();

        /// <summary>
        /// 异步提交任务。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回一个包含动作结果的异步操作。</returns>
        public virtual Task<IActionResult> OnPostAsync(string returnUrl = null) => throw new NotImplementedException();
    }


    internal class LoginModel<TUser> : AbstractLoginModel where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly ILogger<AbstractLoginModel> _logger;


        public LoginModel(SignInManager<TUser> signInManager, ILogger<AbstractLoginModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }


        public override async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public override async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}
