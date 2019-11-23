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
using System.Collections.Generic;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    class IdentityInterfaceSitemapWithPages : InterfaceSitemapWithPages
    {
        public IdentityInterfaceSitemapWithPages(IStringLocalizer<LayoutViewResource> layoutLocalizer,
            IStringLocalizer<InterfaceSitemapResource> localizer)
            : base(localizer, "Identity")
        {
            ManageFootbar = IdentityInterfaceSitemapHelper.GetManageFootbar(layoutLocalizer);
            ManageSidebar = GetManageSidebar(layoutLocalizer);
            CommonHeader = GetCommonHeader(layoutLocalizer);
        }


        private List<NavigationDescriptor> GetCommonHeader(IStringLocalizer<LayoutViewResource> layoutLocalizer)
        {
            return new List<NavigationDescriptor>
            {
                layoutLocalizer.AsNavigation(RouteDescriptor.ByPage("/Home/About"))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("about")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByPage("/Home/Contact"))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("contact"))
            };
        }


        private List<NavigationDescriptor> GetManageSidebar(IStringLocalizer<LayoutViewResource> layoutLocalizer)
        {
            return new List<NavigationDescriptor>
            {
                layoutLocalizer.AsNavigation(RouteDescriptor.ByPage("/Account/Manage/Index", Area), p => p.Profile)
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("profile").ChangeIcon("la la-user")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByPage("/Account/Manage/ChangePassword", Area))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("change-password").ChangeIcon("la la-unlock")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByPage("/Account/Manage/ExternalLogins", Area))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("external-login").ChangeIcon("la la-key")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByPage("/Account/Manage/TwoFactorAuthentication", Area))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("two-factor").ChangeIcon("la la-superscript")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByPage("/Account/Manage/PersonalData", Area))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("personal-data").ChangeIcon("la la-user-times")),
            };
        }

    }
}
