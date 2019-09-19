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
        /// <param name="localizer">给定的 <see cref="IExpressionStringLocalizer{ApplicationSiteMapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public InterfaceSitemapWithPages(IExpressionStringLocalizer<InterfaceSitemapResource> localizer, string area = null)
            : base(localizer, area)
        {
            var index = new RouteDescriptor("/", area: null);
            Index = new NavigationDescriptor(index, Localizer[nameof(Index)]);

            var accessDenied = new RouteDescriptor("/Home/AccessDenied", area: null);
            AccessDenied = new NavigationDescriptor(accessDenied, Localizer[nameof(AccessDenied)]);

            var privacy = new RouteDescriptor("/Home/Privacy", area: null);
            Privacy = new NavigationDescriptor(privacy, Localizer[nameof(Privacy)]);

            var sitemap = new RouteDescriptor("/Home/Sitemap", area: null);
            Sitemap = new NavigationDescriptor(sitemap, Localizer[nameof(Sitemap)]);

            var login = new RouteDescriptor("/Account/Login", Area);
            Login = new NavigationDescriptor(login, Localizer[nameof(Login)]);

            var logout = new RouteDescriptor("/Account/Logout", Area);
            Logout = new NavigationDescriptor(logout, Localizer[nameof(Logout)]);

            var register = new RouteDescriptor("/Account/Register", Area);
            Register = new NavigationDescriptor(register, Localizer[nameof(Register)]);

            var manage = new RouteDescriptor("/Account/Manage/Index", Area);
            Manage = new NavigationDescriptor(manage, Localizer[nameof(Manage)]);
        }
    }
}
