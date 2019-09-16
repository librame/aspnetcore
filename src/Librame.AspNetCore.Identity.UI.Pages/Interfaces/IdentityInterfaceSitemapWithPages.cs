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

    class IdentityInterfaceSitemapWithPages : InterfaceSitemapWithPages
    {
        public IdentityInterfaceSitemapWithPages(IExpressionStringLocalizer<LayoutViewResource> layoutLocalizer,
            IExpressionStringLocalizer<InterfaceSitemapResource> localizer)
            : base(localizer, "Identity")
        {
            ManageFootbar = IdentityInterfaceSitemapHelper.GetManageFootbar(layoutLocalizer);
            ManageSidebar = GetManageSidebar(layoutLocalizer);
            CommonHeader = GetCommonHeader(layoutLocalizer);
        }


        private List<NavigationDescriptor> GetCommonHeader(IExpressionStringLocalizer<LayoutViewResource> layoutLocalizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(layoutLocalizer[p => p.About], "/Home/About")
                {
                    Id = "about",
                    ActiveClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(layoutLocalizer[p => p.Contact], "/Home/Contact")
                {
                    Id = "contact",
                    ActiveClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                }
            };
        }


        private List<NavigationDescriptor> GetManageSidebar(IExpressionStringLocalizer<LayoutViewResource> layoutLocalizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(layoutLocalizer[p => p.Profile], "./Index")
                {
                    Id = "profile",
                    Icon = "la la-user",
                    ActiveClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(layoutLocalizer[p => p.ChangePassword], "./ChangePassword")
                {
                    Id = "change-password",
                    Icon = "la la-unlock",
                    ActiveClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(layoutLocalizer[p => p.ExternalLogins], "./ExternalLogins")
                {
                    Id = "external-login",
                    Icon = "la la-key",
                    ActiveClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                    VisibilityFactory = (page, nav) => ViewDataDictionaryUtility.GetHasExternalLogins(page.ViewContext)
                },
                new NavigationDescriptor(layoutLocalizer[p => p.TwoFactorAuthentication], "./TwoFactorAuthentication")
                {
                    Id = "two-factor",
                    Icon = "la la-superscript",
                    ActiveClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(layoutLocalizer[p => p.PersonalData], "./PersonalData")
                {
                    Id = "personal-data",
                    Icon = "la la-user-times",
                    ActiveClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                }
            };
        }

    }
}
