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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Projects
{
    using AspNetCore.Identity.Web.Resources;
    using AspNetCore.Web.Descriptors;
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Resources;
    using Extensions;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityProjectNavigation : ProjectNavigationWithPage
    {
        public IdentityProjectNavigation(IHtmlLocalizer<LayoutViewResource> layoutLocalizer,
            IHtmlLocalizer<ProjectNavigationResource> localizer)
            : base(localizer, nameof(Identity))
        {
            AddManageSidebar(layoutLocalizer.NotNull(nameof(layoutLocalizer)));

            // 重置身份区域首页为管理
            Index = Manage;
        }


        private void AddManageSidebar(IHtmlLocalizer<LayoutViewResource> layoutLocalizer)
        {
            var manageRoute = Manage.Route as PageRouteDescriptor;

            // 管理首页即个人资料
            var profile = layoutLocalizer.GetNavigation(p => p.Profile, manageRoute)
                .ChangeId("profile").ChangeIcon("la la-user");

            var changePassword = layoutLocalizer.GetNavigationWithRoutePageName(p => p.ChangePassword, manageRoute)
                .ChangeId("change-password").ChangeIcon("la la-unlock");

            var externalLogins = layoutLocalizer.GetNavigationWithRoutePageName(p => p.ExternalLogins, manageRoute)
                .ChangeId("external-login").ChangeIcon("la la-key");

            var twoFactor = layoutLocalizer.GetNavigationWithRoutePageName(p => p.TwoFactorAuthentication, manageRoute)
                .ChangeId("two-factor").ChangeIcon("la la-superscript");

            var personalData = layoutLocalizer.GetNavigationWithRoutePageName(p => p.PersonalData, manageRoute)
                .ChangeId("personal-data").ChangeIcon("la la-user-times");

            ManageLayout.Sidebar.Add(profile);
            ManageLayout.Sidebar.Add(changePassword);
            ManageLayout.Sidebar.Add(externalLogins);
            ManageLayout.Sidebar.Add(twoFactor);
            ManageLayout.Sidebar.Add(personalData);
        }

    }
}
