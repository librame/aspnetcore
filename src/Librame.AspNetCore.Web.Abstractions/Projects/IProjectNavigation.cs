#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Localization;

namespace Librame.AspNetCore.Web.Projects
{
    using Descriptors;
    using Resources;
    using Themepacks;

    /// <summary>
    /// 项目导航接口。
    /// </summary>
    public interface IProjectNavigation
    {
        /// <summary>
        /// 根导航（如果当前是根导航，则为自身实例）。
        /// </summary>
        /// <value>返回 <see cref="IProjectNavigation"/>。</value>
        IProjectNavigation RootNavigation { get; }


        /// <summary>
        /// 布局内容集合。
        /// </summary>
        /// <value>返回 <see cref="LayoutContentCollection{NavigationDescriptor}"/>。</value>
        LayoutContentCollection<NavigationDescriptor> LayoutContents { get; }

        /// <summary>
        /// 本地化器。
        /// </summary>
        /// <value>返回 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</value>
        IHtmlLocalizer<ProjectNavigationResource> Localizer { get; }

        /// <summary>
        /// 区域。
        /// </summary>
        string Area { get; }


        /// <summary>
        /// 公共布局导航。
        /// </summary>
        LayoutContent<NavigationDescriptor> CommonLayout { get; }

        /// <summary>
        /// 登入布局导航。
        /// </summary>
        LayoutContent<NavigationDescriptor> LoginLayout { get; }

        /// <summary>
        /// 管理布局导航。
        /// </summary>
        LayoutContent<NavigationDescriptor> ManageLayout { get; }


        /// <summary>
        /// 首页。
        /// </summary>
        NavigationDescriptor Index { get; }


        /// <summary>
        /// 关于。
        /// </summary>
        NavigationDescriptor About { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        NavigationDescriptor Contact { get; }

        /// <summary>
        /// 隐私。
        /// </summary>
        NavigationDescriptor Privacy { get; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        NavigationDescriptor Sitemap { get; }

        /// <summary>
        /// 项目库。
        /// </summary>
        NavigationDescriptor Repository { get; }

        /// <summary>
        /// 反馈。
        /// </summary>
        NavigationDescriptor Issues { get; }

        /// <summary>
        /// 许可。
        /// </summary>
        NavigationDescriptor Licenses { get; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        NavigationDescriptor AccessDenied { get; }


        /// <summary>
        /// 注册。
        /// </summary>
        NavigationDescriptor Register { get; }

        /// <summary>
        /// 登入。
        /// </summary>
        NavigationDescriptor Login { get; }

        /// <summary>
        /// 扩展登入。
        /// </summary>
        NavigationDescriptor ExternalLogin { get; }

        /// <summary>
        /// 登出。
        /// </summary>
        NavigationDescriptor Logout { get; }

        /// <summary>
        /// 管理。
        /// </summary>
        NavigationDescriptor Manage { get; }
    }
}
