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
    public class IdentityInterfaceSitemapHelper
    {
        /// <summary>
        /// 获取管理底栏站点地图。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionStringLocalizer{LayoutViewResource}"/>。</param>
        /// <returns>返回 <see cref="List{NavigationDescriptor}"/>。</returns>
        public static List<NavigationDescriptor> GetManageFootbar(IExpressionStringLocalizer<LayoutViewResource> localizer)
        {
            localizer.NotNull(nameof(localizer));

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
