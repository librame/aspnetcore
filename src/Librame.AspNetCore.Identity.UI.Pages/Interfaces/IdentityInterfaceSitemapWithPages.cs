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
                new NavigationDescriptor(new RouteDescriptor("/Home/About", area: null), layoutLocalizer[p => p.About])
                {
                    Id = "about",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(new RouteDescriptor("/Home/Contact", area: null), layoutLocalizer[p => p.Contact])
                {
                    Id = "contact",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                }
            };
        }


        private List<NavigationDescriptor> GetManageSidebar(IExpressionStringLocalizer<LayoutViewResource> layoutLocalizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(new RouteDescriptor("/Account/Manage/Index", Area), layoutLocalizer[p => p.Profile])
                {
                    Id = "profile",
                    Icon = "la la-user",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(new RouteDescriptor("/Account/Manage/ChangePassword", Area), layoutLocalizer[p => p.ChangePassword])
                {
                    Id = "change-password",
                    Icon = "la la-unlock",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(new RouteDescriptor("/Account/Manage/ExternalLogins", Area), layoutLocalizer[p => p.ExternalLogins])
                {
                    Id = "external-login",
                    Icon = "la la-key",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                    VisibilityFactory = (page, nav) => ViewDataDictionaryUtility.GetHasExternalLogins(page.ViewContext)
                },
                new NavigationDescriptor(new RouteDescriptor("/Account/Manage/TwoFactorAuthentication", Area), layoutLocalizer[p => p.TwoFactorAuthentication])
                {
                    Id = "two-factor",
                    Icon = "la la-superscript",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                },
                new NavigationDescriptor(new RouteDescriptor("/Account/Manage/PersonalData", Area), layoutLocalizer[p => p.PersonalData])
                {
                    Id = "personal-data",
                    Icon = "la la-user-times",
                    ActiveCssClassNameFactory = (page, nav) => ViewContextUtility.ActiveViewCssClassNameOrEmpty(page.ViewContext, nav),
                }
            };
        }

    }
}
