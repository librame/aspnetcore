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
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.UI.Pages.Account.Manage
{
    using AspNetCore.UI;

    /// <summary>
    /// 抽象个人数据页面模型。
    /// </summary>
    [PageApplicationModelWithUser(typeof(PersonalDataPageModel<>))]
    public abstract class AbstractPersonalDataPageModel : PageModel
    {
        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnGetAsync()
            => throw new NotImplementedException();
    }

    internal class PersonalDataPageModel<TUser> : AbstractPersonalDataPageModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger<AbstractPersonalDataPageModel> _logger;

        public PersonalDataPageModel(
            UserManager<TUser> userManager,
            ILogger<AbstractPersonalDataPageModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
