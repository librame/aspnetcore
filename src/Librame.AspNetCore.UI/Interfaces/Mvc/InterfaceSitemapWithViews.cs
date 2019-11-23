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
    /// 带视图集合的界面站点地图。
    /// </summary>
    public class InterfaceSitemapWithViews : AbstractInterfaceSitemap
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceSitemapWithViews"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{InterfaceSitemapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public InterfaceSitemapWithViews(IStringLocalizer<InterfaceSitemapResource> localizer, string area = null)
            : base(localizer, area)
        {
            var index = Localizer.AsNavigation(RouteDescriptor.ByHomeController(nameof(Index)));
            var login = index.NewRouteController(r => r.Login, "Account", Area);

            // Home
            Index = index;
            AccessDenied = index.NewRouteAction(r => r.AccessDenied);
            Privacy = index.NewRouteAction(r => r.Privacy);
            Sitemap = index.NewRouteAction(r => r.Sitemap);

            // Account
            Login = login;
            Logout = login.NewRouteAction(r => r.Logout);
            Register = login.NewRouteAction(r => r.Register);

            // Manage
            Manage = login.NewRouteController(r => r.Index, "Manage", r => r.Manage);
        }
    }
}
