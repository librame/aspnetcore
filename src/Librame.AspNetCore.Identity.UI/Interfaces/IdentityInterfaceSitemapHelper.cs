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
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 身份界面站点地图助手。
    /// </summary>
    public static class IdentityInterfaceSitemapHelper
    {
        /// <summary>
        /// 获取管理底栏站点地图。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionLocalizer{LayoutViewResource}"/>。</param>
        /// <returns>返回 <see cref="List{NavigationDescriptor}"/>。</returns>
        public static List<NavigationDescriptor> GetManageFootbar(IExpressionLocalizer<LayoutViewResource> localizer)
        {
            localizer.NotNull(nameof(localizer));

            return new List<NavigationDescriptor>
            {
                new NavigationDescriptor<LayoutViewResource>(new RouteDescriptor("https://github.com/librame/LibrameCore"),
                    localizer, p => p.Repository).ChangeTagTarget("_blank"),

                new NavigationDescriptor<LayoutViewResource>(new RouteDescriptor("https://github.com/librame/LibrameCore/issues"),
                    localizer, p => p.Issues).ChangeTagTarget("_blank"),

                new NavigationDescriptor<LayoutViewResource>(new RouteDescriptor("https://github.com/librame/LibrameCore/blob/master/LICENSE.txt"),
                    localizer, p => p.Licenses).ChangeTagTarget("_blank")
            };
        }

    }
}
