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
    /// 带页面的项目导航。
    /// </summary>
    public class ProjectNavigationWithPage : AbstractProjectNavigation
    {
        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithPage"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</param>
        protected ProjectNavigationWithPage(IHtmlLocalizer<ProjectNavigationResource> localizer)
            : base(localizer)
        {
            Initialize();
        }

        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithPage"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{ProjectNavigationResource}"/>。</param>
        /// <param name="area">给定的区域。</param>
        /// <param name="rootNavigation">给定的根 <see cref="IProjectNavigation"/>。</param>
        protected ProjectNavigationWithPage(IHtmlLocalizer<ProjectNavigationResource> localizer,
            string area, IProjectNavigation rootNavigation = null)
            : base(localizer, area, rootNavigation ?? new RootProjectNavigationWithPage(localizer))
        {
            Initialize();
        }


        private void Initialize()
        {
            // Default: Root
            var indexRoute = new PageRouteDescriptor($"/{nameof(Index)}", Area);

            Index = Localizer.GetNavigation(indexRoute);
            AccessDenied = Localizer.GetNavigationWithRoutePageName(p => p.AccessDenied, indexRoute);
            Privacy = Localizer.GetNavigationWithRoutePageName(p => p.Privacy, indexRoute);
            About = Localizer.GetNavigationWithRoutePageName(p => p.About, indexRoute);
            Contact = Localizer.GetNavigationWithRoutePageName(p => p.Contact, indexRoute);
            Sitemap = Localizer.GetNavigationWithRoutePageName(p => p.Sitemap, indexRoute);

            // Area: Account
            var loginRoute = indexRoute.WithPage($"/Account/{nameof(Login)}");

            Login = Localizer.GetNavigation(loginRoute);
            ExternalLogin = Localizer.GetNavigationWithRoutePageName(p => p.ExternalLogin, loginRoute);
            Logout = Localizer.GetNavigationWithRoutePageName(p => p.Logout, loginRoute);
            Register = Localizer.GetNavigationWithRoutePageName(p => p.Register, loginRoute);

            // Area: Manage
            Manage = Localizer.GetNavigation(indexRoute.WithPage($"/Account/Manage/{nameof(Index)}"), nameof(Manage));

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
