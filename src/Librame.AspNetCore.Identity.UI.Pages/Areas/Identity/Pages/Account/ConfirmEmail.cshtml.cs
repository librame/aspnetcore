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
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account
{
    using AspNetCore.UI;

    /// <summary>
    /// 确认电邮页面模型。
    /// </summary>
    [AllowAnonymous]
    [UiTemplateWithUser(typeof(ConfirmEmailPageModel<>))]
    public class ConfirmEmailPageModel : PageModel
    {
        /// <summary>
        /// 异步获取方法。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="token">给定的令牌。</param>
        /// <returns>返回一个包含 <see cref="IActionResult"/> 的异步操作。</returns>
        public virtual Task<IActionResult> OnGetAsync(string userId, string token)
            => throw new NotImplementedException();
    }


    internal class ConfirmEmailPageModel<TUser> : ConfirmEmailPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;


        public ConfirmEmailPageModel(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }


        public override async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return Page();
        }
    }
}
