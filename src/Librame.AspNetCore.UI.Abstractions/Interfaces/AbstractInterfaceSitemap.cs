#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 抽象界面站点地图。
    /// </summary>
    public abstract class AbstractInterfaceSitemap : IInterfaceSitemap
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractInterfaceSitemap"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionStringLocalizer{ApplicationSiteMapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        protected AbstractInterfaceSitemap(IExpressionStringLocalizer<InterfaceSitemapResource> localizer, string area = null)
        {
            Localizer = localizer.NotNull(nameof(localizer));

            Area = area;

            Index = new NavigationDescriptor(Localizer[nameof(Index)], "/");

            AccessDenied = new NavigationDescriptor(Localizer[nameof(AccessDenied)], area: area);
            Privacy = new NavigationDescriptor(Localizer[nameof(Privacy)], area: area);
            Sitemap = new NavigationDescriptor(Localizer[nameof(Sitemap)], area: area);

            Login = new NavigationDescriptor(Localizer[nameof(Login)], area: area);
            Logout = new NavigationDescriptor(Localizer[nameof(Logout)], area: area);
            Register = new NavigationDescriptor(Localizer[nameof(Register)], area: area);

            Manage = new NavigationDescriptor(Localizer[nameof(Manage)], area: area);
        }


        /// <summary>
        /// 本地化界面站点地图。
        /// </summary>
        protected IExpressionStringLocalizer<InterfaceSitemapResource> Localizer { get; }


        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; }


        /// <summary>
        /// 首页。
        /// </summary>
        public NavigationDescriptor Index { get; }

        /// <summary>
        /// 隐私。
        /// </summary>
        public NavigationDescriptor Privacy { get; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        public NavigationDescriptor Sitemap { get; }


        /// <summary>
        /// 登入。
        /// </summary>
        public NavigationDescriptor Login { get; }

        /// <summary>
        /// 登出。
        /// </summary>
        public NavigationDescriptor Logout { get; }

        /// <summary>
        /// 注册。
        /// </summary>
        public NavigationDescriptor Register { get; }


        /// <summary>
        /// 拒绝访问。
        /// </summary>
        public NavigationDescriptor AccessDenied { get; }

        /// <summary>
        /// 管理。
        /// </summary>
        public NavigationDescriptor Manage { get; }


        /// <summary>
        /// 公共顶栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> CommonHeader { get; protected set; }

        /// <summary>
        /// 公共侧边栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> CommonSidebar { get; protected set; }

        /// <summary>
        /// 公共底栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> CommonFootbar { get; protected set; }


        /// <summary>
        /// 管理顶栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> ManageHeader { get; protected set; }

        /// <summary>
        /// 管理侧边栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> ManageSidebar { get; protected set; }

        /// <summary>
        /// 管理底栏导航列表。
        /// </summary>
        public List<NavigationDescriptor> ManageFootbar { get; protected set; }
    }
}
