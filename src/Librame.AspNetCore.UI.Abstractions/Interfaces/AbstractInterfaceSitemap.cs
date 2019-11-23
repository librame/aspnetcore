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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 抽象界面站点地图。
    /// </summary>
    public abstract class AbstractInterfaceSitemap : IInterfaceSitemap
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractInterfaceSitemap"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{ApplicationSiteMapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        protected AbstractInterfaceSitemap(IStringLocalizer<InterfaceSitemapResource> localizer, string area = null)
        {
            Localizer = localizer.NotNull(nameof(localizer));
            Area = area;
        }


        /// <summary>
        /// 本地化界面站点地图。
        /// </summary>
        protected IStringLocalizer<InterfaceSitemapResource> Localizer { get; }


        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; }


        /// <summary>
        /// 首页。
        /// </summary>
        public NavigationDescriptor Index { get; protected set; }

        /// <summary>
        /// 隐私。
        /// </summary>
        public NavigationDescriptor Privacy { get; protected set; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        public NavigationDescriptor Sitemap { get; protected set; }


        /// <summary>
        /// 登入。
        /// </summary>
        public NavigationDescriptor Login { get; protected set; }

        /// <summary>
        /// 登出。
        /// </summary>
        public NavigationDescriptor Logout { get; protected set; }

        /// <summary>
        /// 注册。
        /// </summary>
        public NavigationDescriptor Register { get; protected set; }


        /// <summary>
        /// 拒绝访问。
        /// </summary>
        public NavigationDescriptor AccessDenied { get; protected set; }

        /// <summary>
        /// 管理。
        /// </summary>
        public NavigationDescriptor Manage { get; protected set; }


        /// <summary>
        /// 公共顶栏导航列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NavigationDescriptor> CommonHeader { get; protected set; }

        /// <summary>
        /// 公共侧边栏导航列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NavigationDescriptor> CommonSidebar { get; protected set; }

        /// <summary>
        /// 公共底栏导航列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NavigationDescriptor> CommonFootbar { get; protected set; }


        /// <summary>
        /// 管理顶栏导航列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NavigationDescriptor> ManageHeader { get; protected set; }

        /// <summary>
        /// 管理侧边栏导航列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NavigationDescriptor> ManageSidebar { get; protected set; }

        /// <summary>
        /// 管理底栏导航列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public List<NavigationDescriptor> ManageFootbar { get; protected set; }
    }
}
