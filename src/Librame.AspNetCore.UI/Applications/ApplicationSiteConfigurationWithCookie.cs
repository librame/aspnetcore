#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 带 Cookie 认证的应用站点配置。
    /// </summary>
    public class ApplicationSiteConfigurationWithCookie : ApplicationSiteConfiguration,
        IPostConfigureOptions<CookieAuthenticationOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="ApplicationSiteConfiguration"/>。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="area">指定的区域。</param>
        protected ApplicationSiteConfigurationWithCookie(IApplicationContext context, string area)
            : base(context, area)
        {
        }


        /// <summary>
        /// 后置配置。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="options">给定的 <see cref="CookieAuthenticationOptions"/>。</param>
        public virtual void PostConfigure(string name, CookieAuthenticationOptions options)
        {
            options = options ?? new CookieAuthenticationOptions();

            if (IdentityConstants.ApplicationScheme.Equals(name, StringComparison.Ordinal))
            {
                options.LoginPath = $"/{Area}{BuilderOptions.Safety.LoginAreaRelativePath}";
                options.LogoutPath = $"/{Area}{BuilderOptions.Safety.LogoutAreaRelativePath}";
                options.AccessDeniedPath = $"/{Area}{BuilderOptions.Safety.AccessAreaDeniedRelativePath}";
            }
        }

    }
}
