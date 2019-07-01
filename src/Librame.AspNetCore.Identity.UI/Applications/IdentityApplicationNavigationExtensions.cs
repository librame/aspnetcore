#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions;

    /// <summary>
    /// 身份应用导航静态扩展。
    /// </summary>
    public static class IdentityApplicationNavigationExtensions
    {

        /// <summary>
        /// 绑定身份导航。
        /// </summary>
        /// <param name="navigation">给定的 <see cref="IApplicationNavigation"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <returns>返回 <see cref="IApplicationNavigation"/>。</returns>
        public static IApplicationNavigation BindIdentityNavigation(this IApplicationNavigation navigation, IStringLocalizer localizer)
        {
            navigation.NotNull(nameof(navigation));
            localizer.NotNull(nameof(localizer));

            // GetManageSidebarDescriptors
            navigation.AddOrUpdateManageSidebar(GetManageSidebarDescriptors(navigation, localizer));

            // GetManageFootbarDescriptors
            navigation.AddOrUpdateManageFooter(GetManageFootbarDescriptors(navigation, localizer));

            return navigation;
        }


        private static IList<NavigationDescriptor> GetManageSidebarDescriptors(IApplicationNavigation navigation, IStringLocalizer localizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(localizer["Profile"], "./Index")
                {
                    Id = "profile",
                    Icon = "la la-user",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("Profile"),
                },
                new NavigationDescriptor(localizer["ChangePassword"], "./ChangePassword")
                {
                    Id = "change-password",
                    Icon = "la la-unlock",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("ChangePassword"),
                },
                new NavigationDescriptor(localizer["ExternalLogins"], "./ExternalLogins")
                {
                    Id = "external-login",
                    Icon = "la la-key",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("ExternalLogins"),
                    VisibilityFactory = page => (bool)(page as RazorPage<IApplicationContext>).ViewData["ManageNav.HasExternalLogins"]
                },
                new NavigationDescriptor(localizer["TwoFactorAuthentication"], "./TwoFactorAuthentication")
                {
                    Id = "two-factor",
                    Icon = "la la-superscript",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("TwoFactorAuthentication"),
                },
                new NavigationDescriptor(localizer["PersonalData"], "./PersonalData")
                {
                    Id = "personal-data",
                    Icon = "la la-user-times",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("PersonalData"),
                }
            };
        }

        private static IList<NavigationDescriptor> GetManageFootbarDescriptors(IApplicationNavigation navigation, IStringLocalizer localizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(localizer["Repository"], "https://github.com/librame/LibrameCore")
                {
                    Target = "_blank"
                },
                new NavigationDescriptor(localizer["Issues"], "https://github.com/librame/LibrameCore/issues")
                {
                    Target = "_blank"
                },
                new NavigationDescriptor(localizer["Licenses"], "https://github.com/librame/LibrameCore/blob/master/LICENSE.txt")
                {
                    Target = "_blank"
                }
            };
        }

    }
}
