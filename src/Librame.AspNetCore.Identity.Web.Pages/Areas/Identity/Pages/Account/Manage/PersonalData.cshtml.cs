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
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Web;
    using Extensions;

    /// <summary>
    /// 个人数据页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(PersonalDataPageModel<>))]
    public class PersonalDataPageModel : PageModel
    {
        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnGetAsync()
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class PersonalDataPageModel<TUser> : PersonalDataPageModel where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly ILogger<PersonalDataPageModel> _logger;


        public PersonalDataPageModel(
            UserManager<TUser> userManager,
            ILogger<PersonalDataPageModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }


        public override async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
