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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Projects
{
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Resources;
    using AspNetCore.Web.Routings;
    using Extensions;
    using Resources;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityProjectNavigation : ProjectNavigationWithController
    {
        private readonly IStringLocalizer<LayoutViewResource> _layoutLocalizer;


        public IdentityProjectNavigation(IStringLocalizer<ProjectNavigationResource> localizer,
            IStringLocalizer<LayoutViewResource> layoutLocalizer)
            : base(localizer, nameof(Identity))
        {
            _layoutLocalizer = layoutLocalizer.NotNull(nameof(layoutLocalizer));

            Logout.Route.ChangeAction("LogOff");

            AddManageSidebar();
        }


        private void AddManageSidebar()
        {
            var controller = "Manage";

            ManageLayout.Sidebar.Add(_layoutLocalizer.AsNavigation(RouteDescriptor.ByController("Index", controller, Area), p => p.Profile)
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("profile").ChangeIcon("la la-user")));

            ManageLayout.Sidebar.Add(_layoutLocalizer.AsNavigation(RouteDescriptor.ByController("ChangePassword", controller, Area))
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("change-password").ChangeIcon("la la-unlock")));

            ManageLayout.Sidebar.Add(_layoutLocalizer.AsNavigation(RouteDescriptor.ByController("ManageLogins", controller, Area), p => p.ExternalLogins)
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("external-login").ChangeIcon("la la-key")));

            ManageLayout.Sidebar.Add(_layoutLocalizer.AsNavigation(RouteDescriptor.ByController("AddPhoneNumber", controller, Area))
                .ChangeOptional(optional => optional.ChangeActiveCssClassNameForPage().ChangeTagId("add-phone-number").ChangeIcon("la la-superscript")));
        }

    }
}
