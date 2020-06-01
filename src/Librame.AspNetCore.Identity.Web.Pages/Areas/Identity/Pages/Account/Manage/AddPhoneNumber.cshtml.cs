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
    using AspNetCore.Web;
    using Extensions;
    using Extensions.Network.Services;

    /// <summary>
    /// 添加手机号页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(AddPhoneNumberPageModel<>))]
    public class AddPhoneNumberPageModel : PageModel
    {
        /// <summary>
        /// 输入模型。
        /// </summary>
        [BindProperty]
        public AddPhoneNumberViewModel Input { get; set; }


        /// <summary>
        /// 获取方法。
        /// </summary>
        public virtual void OnGet()
        {
        }

        /// <summary>
        /// 提交方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class AddPhoneNumberPageModel<TUser> : AddPhoneNumberPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly ISmsService _smsService;
        private readonly IHtmlLocalizer<AddPhoneNumberViewResource> _addPhoneNumberLocalizer;


        public AddPhoneNumberPageModel(SignInManager<TUser> signInManager,
            ISmsService smsService, IHtmlLocalizer<AddPhoneNumberViewResource> addPhoneNumberLocalizer)
            : base()
        {
            _userManager = signInManager.UserManager;
            _smsService = smsService;
            _addPhoneNumberLocalizer = addPhoneNumberLocalizer;
        }


        public override async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Generate the token and send it
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAndResultAsync();
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, Input.Phone).ConfigureAndResultAsync();

            await _smsService.SendAsync(Input.Phone, _addPhoneNumberLocalizer.GetString(r => r.YourSecurityCodeIs).Value + code).ConfigureAndWaitAsync();

            return RedirectToPage("./VerifyPhoneNumber", new { phoneNumber = Input.Phone });
        }

    }

}
