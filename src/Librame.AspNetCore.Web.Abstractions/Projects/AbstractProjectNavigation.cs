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
    using AspNetCore.Applications;
    using Extensions;
    using Resources;
    using Routings;
    using Themepacks;

    /// <summary>
    /// 抽象项目导航。
    /// </summary>
    public abstract class AbstractProjectNavigation : IProjectNavigation
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractProjectNavigation"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{ProjectNavigationResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        protected AbstractProjectNavigation(IStringLocalizer<ProjectNavigationResource> localizer, string area = null)
        {
            Localizer = localizer.NotNull(nameof(localizer));
            Area = area;

            CommonLayout = new LayoutNavigationDescriptor();
            LoginLayout = new LayoutNavigationDescriptor();
            ManageLayout = new LayoutNavigationDescriptor();

            Layouts = new ConcurrentDictionary<string, LayoutNavigationDescriptor>();
            Layouts.AddOrUpdate(AbstractThemepackInfo.CommonLayoutKey, CommonLayout, (key, value) => CommonLayout);
            Layouts.AddOrUpdate(AbstractThemepackInfo.LoginLayoutKey, LoginLayout, (key, value) => LoginLayout);
            Layouts.AddOrUpdate(AbstractThemepackInfo.ManageLayoutKey, ManageLayout, (key, value) => ManageLayout);

            AddLibrameFootbar();
        }


        private void AddLibrameFootbar()
        {
            Repository = Localizer.AsNavigation(new RouteDescriptor(AbstractApplicationInfo.LibrameRepositoryUrl),
                p => p.Repository, optional => optional.ChangeTagTarget("_blank"));

            Issues = Localizer.AsNavigation(new RouteDescriptor($"{AbstractApplicationInfo.LibrameRepositoryUrl}/issues"),
                p => p.Issues, optional => optional.ChangeTagTarget("_blank"));

            Licenses = Localizer.AsNavigation(new RouteDescriptor($"{AbstractApplicationInfo.LibrameRepositoryUrl}/blob/master/LICENSE.txt"),
                p => p.Licenses, optional => optional.ChangeTagTarget("_blank"));

            ManageLayout.Footbar.Add(Repository);
            ManageLayout.Footbar.Add(Issues);
            ManageLayout.Footbar.Add(Licenses);
        }


        /// <summary>
        /// 本地化资源。
        /// </summary>
        public IStringLocalizer<ProjectNavigationResource> Localizer { get; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; }


        /// <summary>
        /// 首页。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Index { get; protected set; }


        /// <summary>
        /// 关于。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> About { get; protected set; }

        /// <summary>
        /// 联系。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Contact { get; protected set; }

        /// <summary>
        /// 隐私。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Privacy { get; protected set; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Sitemap { get; protected set; }

        /// <summary>
        /// 项目库。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Repository { get; protected set; }

        /// <summary>
        /// 反馈。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Issues { get; protected set; }

        /// <summary>
        /// 许可。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Licenses { get; protected set; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> AccessDenied { get; protected set; }


        /// <summary>
        /// 注册。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Register { get; protected set; }

        /// <summary>
        /// 登入。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Login { get; protected set; }

        /// <summary>
        /// 登出。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Logout { get; protected set; }

        /// <summary>
        /// 管理。
        /// </summary>
        public NavigationDescriptor<ProjectNavigationResource> Manage { get; protected set; }


        /// <summary>
        /// 布局导航集合。
        /// </summary>
        public ConcurrentDictionary<string, LayoutNavigationDescriptor> Layouts { get; }

        /// <summary>
        /// 公共布局导航。
        /// </summary>
        public LayoutNavigationDescriptor CommonLayout { get; }

        /// <summary>
        /// 登入布局导航。
        /// </summary>
        public LayoutNavigationDescriptor LoginLayout { get; }

        /// <summary>
        /// 管理布局导航。
        /// </summary>
        public LayoutNavigationDescriptor ManageLayout { get; }
    }
}
