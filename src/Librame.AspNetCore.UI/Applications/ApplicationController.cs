#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 应用控制器。
    /// </summary>
    public abstract class ApplicationController : Controller
    {
        /// <summary>
        /// 构造一个 <see cref="ApplicationController"/>。
        /// </summary>
        /// <param name="application">给定的 <see cref="IApplicationContext"/>。</param>
        public ApplicationController(IApplicationContext application)
            : base()
        {
            Application = application.NotNull(nameof(application));
            if (Application.CurrentInterfaceInfo.IsNull())
            {
                var route = RouteData.AsRouteDescriptor();
                Application.SetCurrentInterfaceInfo(route.Area);
            }
        }


        /// <summary>
        /// 应用上下文。
        /// </summary>
        public IApplicationContext Application { get; set; }


        /// <summary>
        /// 重定向到站点首页。
        /// </summary>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        public IActionResult RedirectToSitemapIndex()
            => Redirect(Application.CurrentInterfaceInfo.Sitemap.Index.ToRouteString(Url));

        /// <summary>
        /// 重定向到返回 URL 或站点首页。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        public IActionResult RedirectToLocalUrlOrSitemapIndex(string returnUrl)
            => Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToSitemapIndex();
    }
}
