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
    using AspNetCore.Applications;
    using AspNetCore.Web.Descriptors;
    using AspNetCore.Web.Resources;
    using AspNetCore.Web.Themepacks;
    using Extensions;

    /// <summary>
    /// 抽象项目导航。
    /// </summary>
    public abstract class AbstractProjectNavigation : IProjectNavigation
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractProjectNavigation"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</param>
        protected AbstractProjectNavigation(IHtmlLocalizer<ProjectNavigationResource> localizer)
            : this(localizer, area: null)
        {
            RootNavigation = this;
        }

        /// <summary>
        /// 构造一个 <see cref="AbstractProjectNavigation"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</param>
        /// <param name="area">给定的区域。</param>
        /// <param name="rootNavigation">给定的根 <see cref="IProjectNavigation"/>。</param>
        protected AbstractProjectNavigation(IHtmlLocalizer<ProjectNavigationResource> localizer,
            string area, IProjectNavigation rootNavigation)
            : this(localizer, area)
        {
            RootNavigation = rootNavigation.NotNull(nameof(rootNavigation));
        }

        private AbstractProjectNavigation(IHtmlLocalizer<ProjectNavigationResource> localizer, string area)
        {
            LayoutContents = new LayoutContentCollection<NavigationDescriptor>();

            Localizer = localizer.NotNull(nameof(localizer));
            Area = area;

            AddLibrameFootbar();
        }


        private void AddLibrameFootbar()
        {
            Repository = Localizer.GetNavigation(p => p.Repository,
                AbstractApplicationInfo.AspNetCoreRepositoryUrl).ChangeTargetBlank();

            Issues = Localizer.GetNavigation(p => p.Issues,
                $"{AbstractApplicationInfo.AspNetCoreRepositoryUrl}/issues").ChangeTargetBlank();

            Licenses = Localizer.GetNavigation(p => p.Licenses,
                $"{AbstractApplicationInfo.AspNetCoreRepositoryUrl}/blob/master/LICENSE").ChangeTargetBlank();

            ManageLayout.Footer.Add(Repository);
            ManageLayout.Footer.Add(Issues);
            ManageLayout.Footer.Add(Licenses);
        }


        /// <summary>
        /// 根导航（如果当前是根导航，则为自身实例）。
        /// </summary>
        /// <value>返回 <see cref="IProjectNavigation"/>。</value>
        public IProjectNavigation RootNavigation { get; }

        /// <summary>
        /// 身份导航。
        /// </summary>
        /// <value>返回 <see cref="IProjectNavigation"/> 或 NULL。</value>
        public IProjectNavigation IdentityNavigation { get; set; }


        /// <summary>
        /// 布局内容集合。
        /// </summary>
        public LayoutContentCollection<NavigationDescriptor> LayoutContents { get; }

        /// <summary>
        /// 本地化器。
        /// </summary>
        /// <value>返回 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</value>
        public IHtmlLocalizer<ProjectNavigationResource> Localizer { get; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; }


        /// <summary>
        /// 公共布局导航。
        /// </summary>
        public LayoutContent<NavigationDescriptor> CommonLayout
            => LayoutContents.Common;

        /// <summary>
        /// 登入布局导航。
        /// </summary>
        public LayoutContent<NavigationDescriptor> LoginLayout
            => LayoutContents.Login;

        /// <summary>
        /// 管理布局导航。
        /// </summary>
        public LayoutContent<NavigationDescriptor> ManageLayout
            => LayoutContents.Manage;


        /// <summary>
        /// 首页。
        /// </summary>
        public virtual NavigationDescriptor Index { get; protected set; }


        /// <summary>
        /// 关于。
        /// </summary>
        public virtual NavigationDescriptor About { get; protected set; }

        /// <summary>
        /// 联系。
        /// </summary>
        public virtual NavigationDescriptor Contact { get; protected set; }

        /// <summary>
        /// 隐私。
        /// </summary>
        public virtual NavigationDescriptor Privacy { get; protected set; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        public virtual NavigationDescriptor Sitemap { get; protected set; }

        /// <summary>
        /// 项目库。
        /// </summary>
        public virtual NavigationDescriptor Repository { get; protected set; }

        /// <summary>
        /// 反馈。
        /// </summary>
        public virtual NavigationDescriptor Issues { get; protected set; }

        /// <summary>
        /// 许可。
        /// </summary>
        public virtual NavigationDescriptor Licenses { get; protected set; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        public virtual NavigationDescriptor AccessDenied { get; protected set; }


        /// <summary>
        /// 注册。
        /// </summary>
        public virtual NavigationDescriptor Register { get; protected set; }

        /// <summary>
        /// 登入。
        /// </summary>
        public virtual NavigationDescriptor Login { get; protected set; }

        /// <summary>
        /// 扩展登入。
        /// </summary>
        public virtual NavigationDescriptor ExternalLogin { get; protected set; }

        /// <summary>
        /// 登出。
        /// </summary>
        public virtual NavigationDescriptor Logout { get; protected set; }

        /// <summary>
        /// 管理。
        /// </summary>
        public virtual NavigationDescriptor Manage { get; protected set; }
    }
}
