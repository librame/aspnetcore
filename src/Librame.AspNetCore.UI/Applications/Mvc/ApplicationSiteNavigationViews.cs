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
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 应用站点导航视图集合。
    /// </summary>
    public class ApplicationSiteNavigationViews : AbstractApplicationSiteNavigation
    {
        /// <summary>
        /// 构造一个 <see cref="ApplicationSiteNavigationViews"/>。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IExpressionStringLocalizer{ApplicationSiteMapResource}"/>。</param>
        /// <param name="area">给定的区域（可选）。</param>
        public ApplicationSiteNavigationViews(IExpressionStringLocalizer<ApplicationSiteMapResource> localizer, string area = null)
            : base(localizer)
        {
            About.Href = GetPath("/Home/About", area);
            AccessDenied.Href = GetPath("/Home/AccessDenied", area);
            Contact.Href = GetPath("/Home/Contact", area);
            Privacy.Href = GetPath("/Home/Privacy", area);
            Sitemap.Href = GetPath("/Home/Sitemap", area);

            Login.Href = GetPath("/Account/Login", area);
            Logout.Href = GetPath("/Account/LogOff", area);
            Register.Href = GetPath("/Account/Register", area);

            Manage.Href = GetPath("/Manage/Index", area);
        }


        private string GetPath(string relativePath, string area = null)
        {
            if (area.IsNotNullOrEmpty())
                return $"/{area}{relativePath}";

            return relativePath;
        }
    }
}
