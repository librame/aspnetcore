#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions.Core;

    class IdentityInterfaceSitemapWithViews : InterfaceSitemapWithViews
    {
        public IdentityInterfaceSitemapWithViews(IExpressionLocalizer<LayoutViewResource> layoutLocalizer,
            IExpressionLocalizer<InterfaceSitemapResource> localizer)
            : base(localizer, "Identity")
        {
            ManageFootbar = IdentityInterfaceSitemapHelper.GetManageFootbar(layoutLocalizer);
            ManageSidebar = GetManageSidebar(layoutLocalizer);
            CommonHeader = GetCommonHeader(layoutLocalizer);
        }


        private List<NavigationDescriptor> GetCommonHeader(IExpressionLocalizer<LayoutViewResource> layoutLocalizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(new RouteDescriptor("About", "Home", area: null), layoutLocalizer)
                {
                    TagId = "about",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(new RouteDescriptor("Contact", "Home", area: null), layoutLocalizer)
                {
                    TagId = "contact",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                }
            };
        }


        private List<NavigationDescriptor> GetManageSidebar(IExpressionLocalizer<LayoutViewResource> layoutLocalizer)
        {
            var manageController = "Manage";

            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor<LayoutViewResource>(new RouteDescriptor("Index", manageController, Area), layoutLocalizer, p => p.Profile)
                {
                    TagId = "profile",
                    Icon = "la la-user",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(new RouteDescriptor("ChangePassword", manageController, Area), layoutLocalizer)
                {
                    TagId = "change-password",
                    Icon = "la la-unlock",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(new RouteDescriptor("ManageLogins", manageController, Area), layoutLocalizer)
                {
                    TagId = "external-login",
                    Icon = "la la-key",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                    VisibilityFactory = (page, nav) => ViewDataDictionaryUtility.GetHasExternalLogins(page.ViewContext)
                },
                new NavigationDescriptor(new RouteDescriptor("AddPhoneNumber", manageController, Area), layoutLocalizer)
                {
                    TagId = "add-phone-number",
                    Icon = "la la-superscript",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                }
            };
        }

    }
}
