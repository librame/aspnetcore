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
        /// <param name="localizer">给定的 <see cref="IExpressionStringLocalizer{ApplicationSiteMapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public InterfaceSitemapWithViews(IExpressionStringLocalizer<InterfaceSitemapResource> localizer, string area = null)
            : base(localizer, area)
        {
            var homeController = "Home";
            var accountController = "Account";
            var manageController = "Manage";

            var index = new RouteDescriptor(nameof(Index), homeController, area: null);
            Index = new NavigationDescriptor(index, Localizer[nameof(Index)]);

            var accessDenied = new RouteDescriptor(nameof(AccessDenied), homeController, area: null);
            AccessDenied = new NavigationDescriptor(accessDenied, Localizer[nameof(AccessDenied)]);

            var privacy = new RouteDescriptor(nameof(Privacy), homeController, area: null);
            Privacy = new NavigationDescriptor(privacy, Localizer[nameof(Privacy)]);

            var sitemap = new RouteDescriptor(nameof(Sitemap), homeController, area: null);
            Sitemap = new NavigationDescriptor(sitemap, Localizer[nameof(Sitemap)]);


            var login = new RouteDescriptor(nameof(Login), accountController, Area);
            Login = new NavigationDescriptor(login, Localizer[nameof(Login)]);

            var logout = new RouteDescriptor("LogOff", accountController, Area);
            Logout = new NavigationDescriptor(logout, Localizer[nameof(Logout)]);

            var register = new RouteDescriptor(nameof(Register), accountController, Area);
            Register = new NavigationDescriptor(register, Localizer[nameof(Register)]);

            var manage = new RouteDescriptor(nameof(Index), manageController, Area);
            Manage = new NavigationDescriptor(manage, Localizer[nameof(Manage)]);
        }
    }
}
