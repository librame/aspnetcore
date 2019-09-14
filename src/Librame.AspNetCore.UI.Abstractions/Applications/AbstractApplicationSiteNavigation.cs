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
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 抽象应用站点导航。
    /// </summary>
    public abstract class AbstractApplicationSiteNavigation : IApplicationSiteNavigation
    {
        private readonly IExpressionStringLocalizer<ApplicationSiteMapResource> _localizer;


        /// <summary>
        /// 构造一个 <see cref="AbstractApplicationSiteNavigation"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionStringLocalizer{ApplicationSiteMapResource}"/>。</param>
        protected AbstractApplicationSiteNavigation(IExpressionStringLocalizer<ApplicationSiteMapResource> localizer)
        {
            _localizer = localizer.NotNull(nameof(localizer));

            Index = new NavigationDescriptor(_localizer[nameof(Index)], "/");

            About = new NavigationDescriptor(_localizer[nameof(About)]);
            AccessDenied = new NavigationDescriptor(_localizer[nameof(AccessDenied)]);
            Contact = new NavigationDescriptor(_localizer[nameof(Contact)]);
            Privacy = new NavigationDescriptor(_localizer[nameof(Privacy)]);
            Sitemap = new NavigationDescriptor(_localizer[nameof(Sitemap)]);

            Login = new NavigationDescriptor(_localizer[nameof(Login)]);
            Logout = new NavigationDescriptor(_localizer[nameof(Logout)]);
            Register = new NavigationDescriptor(_localizer[nameof(Register)]);

            Manage = new NavigationDescriptor(_localizer[nameof(Manage)]);
        }


        /// <summary>
        /// 关于。
        /// </summary>
        public NavigationDescriptor About { get; }

        /// <summary>
        /// 拒绝访问。
        /// </summary>
        public NavigationDescriptor AccessDenied { get; }

        /// <summary>
        /// 联系。
        /// </summary>
        public NavigationDescriptor Contact { get; }

        /// <summary>
        /// 首页。
        /// </summary>
        public NavigationDescriptor Index { get; }

        /// <summary>
        /// 登入。
        /// </summary>
        public NavigationDescriptor Login { get; }

        /// <summary>
        /// 登出。
        /// </summary>
        public NavigationDescriptor Logout { get; }

        /// <summary>
        /// 管理。
        /// </summary>
        public NavigationDescriptor Manage { get; }

        /// <summary>
        /// 隐私。
        /// </summary>
        public NavigationDescriptor Privacy { get; }

        /// <summary>
        /// 注册。
        /// </summary>
        public NavigationDescriptor Register { get; }

        /// <summary>
        /// 站点地图。
        /// </summary>
        public NavigationDescriptor Sitemap { get; }
    }
}
