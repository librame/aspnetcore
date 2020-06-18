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
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Web.Pages.Account.Manage
{
    using AspNetCore.Web.Applications;
    using Extensions;
    using Extensions.Core.Builders;

    /// <summary>
    /// 下载个人数据页面模型。
    /// </summary>
    [GenericApplicationModel(typeof(IdentityGenericTypeDefinitionMapper),
        typeof(DownloadPersonalDataPageModel<>))]
    public class DownloadPersonalDataPageModel : PageModel
    {
        /// <summary>
        /// 获取方法。
        /// </summary>
        /// <returns>返回一个 <see cref="IActionResult"/>。</returns>
        public virtual IActionResult OnGet()
            => throw new NotImplementedException();

        /// <summary>
        /// 提交方法。
        /// </summary>
        /// <returns>返回一个 <see cref="Task{IActionResult}"/>。</returns>
        public virtual Task<IActionResult> OnPostAsync()
            => throw new NotImplementedException();
    }


    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class DownloadPersonalDataPageModel<TUser> : DownloadPersonalDataPageModel
        where TUser : class
    {
        private readonly UserManager<TUser> _userManager;
        private readonly CoreBuilderOptions _coreOptions;
        private readonly ILogger<DownloadPersonalDataPageModel> _logger;


        public DownloadPersonalDataPageModel(
            UserManager<TUser> userManager,
            IOptions<CoreBuilderOptions> coreOptions,
            ILogger<DownloadPersonalDataPageModel> logger)
        {
            _userManager = userManager;
            _coreOptions = coreOptions.Value;
            _logger = logger;
        }


        public override IActionResult OnGet()
        {
            return NotFound();
        }

        public override async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User).ConfigureAndResultAsync();
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation($"User with ID '{_userManager.GetUserId(User)}' asked for their personal data.");

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(TUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));

            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            var logins = await _userManager.GetLoginsAsync(user).ConfigureAndResultAsync();
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }

            personalData.Add($"Authenticator Key", await _userManager.GetAuthenticatorKeyAsync(user).ConfigureAndResultAsync());

            var json = JsonConvert.SerializeObject(personalData);

            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(json.FromEncodingString(_coreOptions.Encoding), "text/json");
        }
    }
}
