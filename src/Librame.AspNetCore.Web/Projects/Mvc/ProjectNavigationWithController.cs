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

    /// <summary>
    /// 带控制器的项目导航。
    /// </summary>
    public class ProjectNavigationWithController : AbstractProjectNavigation
    {
        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithController"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</param>
        protected ProjectNavigationWithController(IHtmlLocalizer<ProjectNavigationResource> localizer)
            : base(localizer)
        {
            Initialize();
        }

        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithController"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</param>
        /// <param name="area">给定的区域。</param>
        /// <param name="rootNavigation">给定的根 <see cref="IProjectNavigation"/>。</param>
        protected ProjectNavigationWithController(IHtmlLocalizer<ProjectNavigationResource> localizer,
            string area, IProjectNavigation rootNavigation = null)
            : base(localizer, area, rootNavigation ?? new RootProjectNavigationWithController(localizer))
        {
            Initialize();
        }


        private void Initialize()
        {
            // Default: Home
            var indexRoute = new ActionRouteDescriptor(nameof(Index), "Home", Area);

            Index = Localizer.GetNavigation(indexRoute);
            AccessDenied = Localizer.GetNavigationWithRouteAction(p => p.AccessDenied, indexRoute);
            Privacy = Localizer.GetNavigationWithRouteAction(p => p.Privacy, indexRoute);
            About = Localizer.GetNavigationWithRouteAction(p => p.About, indexRoute);
            Contact = Localizer.GetNavigationWithRouteAction(p => p.Contact, indexRoute);
            Sitemap = Localizer.GetNavigationWithRouteAction(p => p.Sitemap, indexRoute);

            // Area: Account
            var loginRoute = indexRoute.WithActionAndController(nameof(Login), "Account");

            Login = Localizer.GetNavigation(loginRoute);
            ExternalLogin = Localizer.GetNavigationWithRouteAction(p => p.ExternalLogin, loginRoute);
            Logout = Localizer.GetNavigationWithRouteAction(p => p.Logout, loginRoute);
            Register = Localizer.GetNavigationWithRouteAction(p => p.Register, loginRoute);

            // Area: Manage
            var manageRoute = indexRoute.WithActionAndController(nameof(Index), nameof(Manage));

            Manage = Localizer.GetNavigation(manageRoute, manageRoute.Controller);

            AddCommonHeader();
            AddCommonFooter();
        }


        private void AddCommonHeader()
        {
            CommonLayout.Header.Add(About);
            CommonLayout.Header.Add(Contact);
        }

        private void AddCommonFooter()
        {
            CommonLayout.Footer.Add(Sitemap);
        }

    }
}
