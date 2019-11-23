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
    using Extensions;

    /// <summary>
    /// 身份界面站点地图助手。
    /// </summary>
    public static class IdentityInterfaceSitemapHelper
    {
        /// <summary>
        /// 获取管理底栏站点地图。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{LayoutViewResource}"/>。</param>
        /// <returns>返回 <see cref="List{NavigationDescriptor}"/>。</returns>
        public static List<NavigationDescriptor> GetManageFootbar(IStringLocalizer<LayoutViewResource> localizer)
        {
            localizer.NotNull(nameof(localizer));

            return new List<NavigationDescriptor>
            {
                localizer.AsNavigation(new RouteDescriptor("https://github.com/librame/LibrameCore"),
                    p => p.Repository, optional => optional.ChangeTagTarget("_blank")),

                localizer.AsNavigation(new RouteDescriptor("https://github.com/librame/LibrameCore/issues"),
                    p => p.Issues, optional => optional.ChangeTagTarget("_blank")),

                localizer.AsNavigation(new RouteDescriptor("https://github.com/librame/LibrameCore/blob/master/LICENSE.txt"),
                    p => p.Licenses, optional => optional.ChangeTagTarget("_blank"))
            };
        }

    }
}
