#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 带页面集合的界面站点地图。
    /// </summary>
    public class InterfaceSitemapWithPages : AbstractInterfaceSitemap
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceSitemapWithPages"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionLocalizer{InterfaceSitemapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public InterfaceSitemapWithPages(IExpressionLocalizer<InterfaceSitemapResource> localizer, string area = null)
            : base(localizer, area)
        {
            // Index
            Index = new NavigationDescriptor<InterfaceSitemapResource>(new RouteDescriptor("/", area: null), Localizer, p => p.Index);

            // Home
            AccessDenied = new NavigationDescriptor(new RouteDescriptor("/Home/AccessDenied", area: null), Localizer);

            Privacy = new NavigationDescriptor(new RouteDescriptor("/Home/Privacy", area: null), Localizer);

            Sitemap = new NavigationDescriptor(new RouteDescriptor("/Home/Sitemap", area: null), Localizer);

            // Account
            Login = new NavigationDescriptor(new RouteDescriptor("/Account/Login", Area), Localizer);

            Logout = new NavigationDescriptor(new RouteDescriptor("/Account/Logout", Area), Localizer);

            Register = new NavigationDescriptor(new RouteDescriptor("/Account/Register", Area), Localizer);

            // Manage
            Manage = new NavigationDescriptor<InterfaceSitemapResource>(new RouteDescriptor("/Account/Manage/Index", Area), Localizer, p => p.Manage);
        }
    }
}
