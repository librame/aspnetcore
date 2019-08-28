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
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// 身份服务器用户界面信息。
    /// </summary>
    public class IdentityServerUiInfo : AbstractUiInfo
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public override string Name
            => "IdentityServer";

        /// <summary>
        /// 标题。
        /// </summary>
        public override string Title
            => "身份服务器";

        /// <summary>
        /// 作者。
        /// </summary>
        public override string Author
            => nameof(Librame);

        /// <summary>
        /// 联系。
        /// </summary>
        public override string Contact
            => "https://github.com/librame/LibrameCore";

        /// <summary>
        /// 版权。
        /// </summary>
        public override string Copyright
            => "librame.net";


        /// <summary>
        /// 添加本地化信息集合。
        /// </summary>
        /// <param name="localizers">给定的 <see cref="ConcurrentDictionary{String, IStringLocalizer}"/>。</param>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public override void AddLocalizers(ConcurrentDictionary<string, IStringLocalizer> localizers, ServiceFactoryDelegate serviceFactory)
        {
            var layoutLocalizer = serviceFactory.GetRequiredService<IExpressionStringLocalizer<LayoutViewResource>>();

            localizers.AddOrUpdateManageLayoutLocalizer(layoutLocalizer);
        }

        /// <summary>
        /// 添加导航信息集合。
        /// </summary>
        /// <param name="navigations">给定的 <see cref="ConcurrentDictionary{String, List}"/>。</param>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        public override void AddNavigations(ConcurrentDictionary<string, List<NavigationDescriptor>> navigations, ServiceFactoryDelegate serviceFactory)
        {
            var layoutLocalizer = serviceFactory.GetRequiredService<IExpressionStringLocalizer<LayoutViewResource>>();

            var manageSidebarNavigations = CreateManageSidebarNavigation(layoutLocalizer);
            navigations.AddOrUpdateManageSidebarNavigation(manageSidebarNavigations);

            var manageFootbarNavigations = CreateManageFootbarNavigation(layoutLocalizer);
            navigations.AddOrUpdateManageFooterNavigation(manageFootbarNavigations);
        }

        private List<NavigationDescriptor> CreateManageSidebarNavigation(IExpressionStringLocalizer<LayoutViewResource> localizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(localizer[p => p.Profile], "./Index")
                {
                    Id = "profile",
                    Icon = "la la-user",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("Profile"),
                },
                new NavigationDescriptor(localizer[p => p.ChangePassword], "./ChangePassword")
                {
                    Id = "change-password",
                    Icon = "la la-unlock",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("ChangePassword"),
                },
                new NavigationDescriptor(localizer[p => p.ExternalLogins], "./ExternalLogins")
                {
                    Id = "external-login",
                    Icon = "la la-key",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("ExternalLogins"),
                    VisibilityFactory = page => (bool)(page as RazorPage<IApplicationContext>).ViewData["ManageNav.HasExternalLogins"]
                },
                new NavigationDescriptor(localizer[p => p.TwoFactorAuthentication], "./TwoFactorAuthentication")
                {
                    Id = "two-factor",
                    Icon = "la la-superscript",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("TwoFactorAuthentication"),
                },
                new NavigationDescriptor(localizer[p => p.PersonalData], "./PersonalData")
                {
                    Id = "personal-data",
                    Icon = "la la-user-times",
                    ActiveClassNameFactory = page => page.ViewContext.ActiveCssClassNameOrEmpty("PersonalData"),
                }
            };
        }

        private List<NavigationDescriptor> CreateManageFootbarNavigation(IExpressionStringLocalizer<LayoutViewResource> localizer)
        {
            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor(localizer[p => p.Repository], "https://github.com/librame/LibrameCore")
                {
                    Target = "_blank"
                },
                new NavigationDescriptor(localizer[p => p.Issues], "https://github.com/librame/LibrameCore/issues")
                {
                    Target = "_blank"
                },
                new NavigationDescriptor(localizer[p => p.Licenses], "https://github.com/librame/LibrameCore/blob/master/LICENSE.txt")
                {
                    Target = "_blank"
                }
            };
        }

    }
}
