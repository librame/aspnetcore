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
    /// 带视图集合的界面站点地图。
    /// </summary>
    public class InterfaceSitemapWithViews : AbstractInterfaceSitemap
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceSitemapWithViews"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionLocalizer{InterfaceSitemapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public InterfaceSitemapWithViews(IExpressionLocalizer<InterfaceSitemapResource> localizer, string area = null)
            : base(localizer, area)
        {
            var homeController = "Home";
            var accountController = "Account";
            var manageController = "Manage";

            // Index
            Index = new NavigationDescriptor(new RouteDescriptor(nameof(Index), homeController, area: null), Localizer);

            // Home
            AccessDenied = new NavigationDescriptor(new RouteDescriptor(nameof(AccessDenied), homeController, area: null), Localizer);

            Privacy = new NavigationDescriptor(new RouteDescriptor(nameof(Privacy), homeController, area: null), Localizer);

            Sitemap = new NavigationDescriptor(new RouteDescriptor(nameof(Sitemap), homeController, area: null), Localizer);

            // Account
            Login = new NavigationDescriptor(new RouteDescriptor(nameof(Login), accountController, Area), Localizer);

            Logout = new NavigationDescriptor<InterfaceSitemapResource>(new RouteDescriptor("LogOff", accountController, Area), Localizer, p => p.Logout);

            Register = new NavigationDescriptor(new RouteDescriptor(nameof(Register), accountController, Area), Localizer);

            // Manage
            Manage = new NavigationDescriptor<InterfaceSitemapResource>(new RouteDescriptor(nameof(Index), manageController, Area), Localizer, p => p.Manage);
        }
    }
}
