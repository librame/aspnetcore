#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;

namespace Librame.AspNetCore.UI
{
    /// <summary>
    /// 带页面集合的界面站点地图。
    /// </summary>
    public class InterfaceSitemapWithPages : AbstractInterfaceSitemap
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceSitemapWithPages"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{InterfaceSitemapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public InterfaceSitemapWithPages(IStringLocalizer<InterfaceSitemapResource> localizer, string area = null)
            : base(localizer, area)
        {
            var index = Localizer.AsNavigation(RouteDescriptor.ByPage("/"), p => p.Index);
            var login = index.NewRoutePage("/Account/Login", Area);

            // Home
            Index = index;
            AccessDenied = index.NewRoutePage("/Home/AccessDenied");
            Privacy = index.NewRoutePage("/Home/Privacy");
            Sitemap = index.NewRoutePage("/Home/Sitemap");

            // Account
            Login = login;
            Logout = login.NewRoutePage("/Account/Logout");
            Register = login.NewRoutePage("/Account/Register");

            // Manage
            Manage = login.NewRoutePage("/Account/Manage/Index", nameof(Manage), Area);
        }
    }
}
