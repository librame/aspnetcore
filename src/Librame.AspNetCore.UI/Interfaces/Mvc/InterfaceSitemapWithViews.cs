#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 带视图集合的界面站点地图。
    /// </summary>
    public class InterfaceSitemapWithViews : AbstractInterfaceSitemap
    {
        /// <summary>
        /// 构造一个 <see cref="InterfaceSitemapWithViews"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionStringLocalizer{ApplicationSiteMapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public InterfaceSitemapWithViews(IExpressionStringLocalizer<InterfaceSitemapResource> localizer, string area = null)
            : base(localizer, area)
        {
            AccessDenied.RelativePath = "/Home/AccessDenied";
            Privacy.RelativePath = "/Home/Privacy";
            Sitemap.RelativePath = "/Home/Sitemap";

            Login.RelativePath = "/Account/Login";
            Logout.RelativePath = "/Account/LogOff";
            Register.RelativePath = "/Account/Register";

            Manage.RelativePath = "/Manage/Index";
        }
    }
}
