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
    /// 带页面的项目导航。
    /// </summary>
    public class ProjectNavigationWithPage : AbstractProjectNavigation
    {
        /// <summary>
        /// 基础帐户路径。
        /// </summary>
        public const string BaseAccountPath = "/Account";

        /// <summary>
        /// 基础管理路径。
        /// </summary>
        public const string BaseManagePath = BaseAccountPath + "/Manage";


        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithPage"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{ProjectNavigationResource}"/>。</param>
        public ProjectNavigationWithPage(IStringLocalizer<ProjectNavigationResource> localizer)
            : this(localizer, null)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="ProjectNavigationWithPage"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{ProjectNavigationResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        protected ProjectNavigationWithPage(IStringLocalizer<ProjectNavigationResource> localizer, string area)
            : base(localizer, area)
        {
            // Default: Home
            Index = Localizer.AsNavigation(RouteDescriptor.ByPage("/"), p => p.Index);

            AccessDenied = Index.NewRoutePage("/AccessDenied");
            Privacy = Index.NewRoutePage("/Privacy");
            Sitemap = Index.NewRoutePage("/Sitemap");

            // Area: Account
            Login = Index.NewRoutePage($"{BaseAccountPath}/Login", Area);
            Logout = Login.NewRoutePage($"{BaseAccountPath}/Logout");
            Register = Login.NewRoutePage($"{BaseAccountPath}/Register");

            // Area: Manage
            Manage = Login.NewRoutePage($"{BaseManagePath}/Index", nameof(Manage), Area);

            AddCommonHeader();
        }


        private void AddCommonHeader()
        {
            About = Localizer.AsNavigation(RouteDescriptor.ByPage("/About"))
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("about"));

            Contact = Localizer.AsNavigation(RouteDescriptor.ByPage("/Contact"))
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("contact"));

            CommonLayout.Header.Add(About);
            CommonLayout.Header.Add(Contact);
        }

    }
}
