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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityInterfaceSitemapWithViews : InterfaceSitemapWithViews
    {
        public IdentityInterfaceSitemapWithViews(IStringLocalizer<LayoutViewResource> layoutLocalizer,
            IStringLocalizer<InterfaceSitemapResource> localizer)
            : base(localizer, "Identity")
        {
            Logout.Route.ChangeAction("LogOff");

            ManageFootbar = IdentityInterfaceSitemapHelper.GetManageFootbar(layoutLocalizer);
            ManageSidebar = GetManageSidebar(layoutLocalizer);
            CommonHeader = GetCommonHeader(layoutLocalizer);
        }


        private List<NavigationDescriptor> GetCommonHeader(IStringLocalizer<LayoutViewResource> layoutLocalizer)
        {
            var controller = "Home";

            return new List<NavigationDescriptor>
            {
                layoutLocalizer.AsNavigation(RouteDescriptor.ByController("About", controller))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("about")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByController("Contact", controller))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("contact"))
            };
        }


        private List<NavigationDescriptor> GetManageSidebar(IStringLocalizer<LayoutViewResource> layoutLocalizer)
        {
            var controller = "Manage";

            return new List<NavigationDescriptor>
            {
                layoutLocalizer.AsNavigation(RouteDescriptor.ByController("Index", controller, Area), p => p.Profile)
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("profile").ChangeIcon("la la-user")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByController("ChangePassword", controller, Area))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("change-password").ChangeIcon("la la-unlock")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByController("ManageLogins", controller, Area), p => p.ExternalLogins)
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("external-login").ChangeIcon("la la-key")),

                layoutLocalizer.AsNavigation(RouteDescriptor.ByController("AddPhoneNumber", controller, Area))
                    .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("add-phone-number").ChangeIcon("la la-superscript"))
            };
        }

    }
}
