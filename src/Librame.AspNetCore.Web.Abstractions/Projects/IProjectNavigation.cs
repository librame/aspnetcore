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
using System.Collections.Concurrent;

namespace Librame.AspNetCore.Web.Projects
{
    using Resources;
    using Routings;
    using Themepacks;

    /// <summary>
    /// 项目导航接口。
    /// </summary>
    public interface IProjectNavigation
    {
        /// <summary>
        /// 本地化资源。
        /// </summary>
        IStringLocalizer<ProjectNavigationResource> Localizer { get; }

        /// <summary>
        /// 区域。
        /// </summary>
        string Area { get; }


        /// <summary>
        /// 首页。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Index { get; }


        /// <summary>
        /// 关于。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> About { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Contact { get; }

        /// <summary>
        /// 隐私。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Privacy { get; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Sitemap { get; }

        /// <summary>
        /// 项目库。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Repository { get; }

        /// <summary>
        /// 反馈。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Issues { get; }

        /// <summary>
        /// 许可。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Licenses { get; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> AccessDenied { get; }


        /// <summary>
        /// 注册。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Register { get; }

        /// <summary>
        /// 登入。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Login { get; }

        /// <summary>
        /// 登出。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Logout { get; }

        /// <summary>
        /// 管理。
        /// </summary>
        NavigationDescriptor<ProjectNavigationResource> Manage { get; }


        /// <summary>
        /// 布局导航集合。
        /// </summary>
        ConcurrentDictionary<string, LayoutNavigationDescriptor> Layouts { get; }

        /// <summary>
        /// 公共布局导航。
        /// </summary>
        LayoutNavigationDescriptor CommonLayout { get; }

        /// <summary>
        /// 登入布局导航。
        /// </summary>
        LayoutNavigationDescriptor LoginLayout { get; }

        /// <summary>
        /// 管理布局导航。
        /// </summary>
        LayoutNavigationDescriptor ManageLayout { get; }
    }
}
