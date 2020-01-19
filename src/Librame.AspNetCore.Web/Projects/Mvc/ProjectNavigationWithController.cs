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

namespace Librame.AspNetCore.Web.Projects
{
    using Resources;
    using Routings;

    /// <summary>
    /// 带控制器的项目导航。
    /// </summary>
    public class ProjectNavigationWithController : AbstractProjectNavigation
    {
        /// <summary>
        /// 主页控制器。
        /// </summary>
        public const string HomeController = "Home";


        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithController"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{ProjectNavigationResource}"/>。</param>
        public ProjectNavigationWithController(IStringLocalizer<ProjectNavigationResource> localizer)
            : this(localizer, null)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithController"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{ProjectNavigationResource}"/>。</param>
        /// <param name="area">给定的区域。</param>
        protected ProjectNavigationWithController(IStringLocalizer<ProjectNavigationResource> localizer, string area)
            : base(localizer, area)
        {
            // Default: Home
            Index = Localizer.AsNavigation(RouteDescriptor.ByController(nameof(Index), HomeController));

            AccessDenied = Index.NewRouteAction(r => r.AccessDenied);
            Privacy = Index.NewRouteAction(r => r.Privacy);
            Sitemap = Index.NewRouteAction(r => r.Sitemap);

            // Area: Account
            Login = Index.NewRouteController(r => r.Login, "Account", Area);
            Logout = Login.NewRouteAction(r => r.Logout);
            Register = Login.NewRouteAction(r => r.Register);

            // Area: Manage
            Manage = Login.NewRouteController(r => r.Index, "Manage", r => r.Manage);

            AddCommonHeader();
        }


        private void AddCommonHeader()
        {
            About = Localizer.AsNavigation(RouteDescriptor.ByController("About", HomeController))
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("about"));

            Contact = Localizer.AsNavigation(RouteDescriptor.ByController("Contact", HomeController))
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("contact"));

            CommonLayout.Header.Add(About);
            CommonLayout.Header.Add(Contact);
        }

    }
}
