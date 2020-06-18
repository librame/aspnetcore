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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Identity.Web.Models;
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Applications;
    using Extensions;

    /// <summary>
    /// 验证手机号码页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(VerifyPhoneNumberPageModel<>))]
    public class VerifyPhoneNumberPageModel : PageModel
    {
        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public VerifyPhoneNumberViewModel Input { get; set; }

        /// <summary>
        /// 状态消息。
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }


        /// <summary>
        /// 获取方法。
        /// </summary>
        public virtual Task<IActionResult> OnGetAsync(string phoneNumber)
            => throw new NotImplementedException();

        /// <summary>
        /// 提交方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class VerifyPhoneNumberPageModel<TUser> : VerifyPhoneNumberPageModel
        where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly IHtmlLocalizer<IndexViewResource> _indexLocalizer;
        private readonly IHtmlLocalizer<VerifyPhoneNumberViewResource> _verifyPhoneNumberLocalizer;


        public VerifyPhoneNumberPageModel(SignInManager<TUser> signInManager,
            IHtmlLocalizer<IndexViewResource> indexLocalizer,
            IHtmlLocalizer<VerifyPhoneNumberViewResource> verifyPhoneNumberLocalizer)
            : base()
        {
            _signInManager = signInManager;
            _userManager = signInManager.UserManager;
            _indexLocalizer = indexLocalizer;
            _verifyPhoneNumberLocalizer = verifyPhoneNumberLocalizer;
        }


        public override async Task<IActionResult> OnGetAsync(string phoneNumber)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAndResultAsync();
            await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber).ConfigureAndWaitAsync();

            if (phoneNumber.IsEmpty())
                return RedirectToPage("Error");

            // Send an SMS to verify the phone number
            Input.Phone = phoneNumber;
            return Page();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Generate the token and send it
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAndResultAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePhoneNumberAsync(user, Input.Phone, Input.Code).ConfigureAndResultAsync();
                if (result.Succeeded)
                {
                    await _signInManager.RefreshSignInAsync(user).ConfigureAndWaitAsync();

                    StatusMessage = _indexLocalizer.GetString(r => r.AddPhoneSuccess).Value;
                    return RedirectToPage();
                }
            }

            // If we got this far, something failed, redisplay the form
            StatusMessage = _verifyPhoneNumberLocalizer.GetString(r => r.Failed).Value;
            return RedirectToPage();
        }

    }
}
