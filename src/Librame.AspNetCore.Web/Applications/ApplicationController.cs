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
using System;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Applications
{
    using Extensions;
    using Projects;
    using Routings;

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
            Application.SetCurrentProject(HttpContext);
        }


        /// <summary>
        /// 应用上下文。
        /// </summary>
        public IApplicationContext Application { get; set; }


        /// <summary>
        /// 获取区域导航路径。
        /// </summary>
        /// <param name="navigationFactory">指定定位导航的工厂方法。</param>
        /// <param name="routeName">给定的路由名称（可选）。</param>
        /// <returns>返回字符串。</returns>
        public string GetAreaNavigationPath(Func<IProjectNavigation, NavigationDescriptor> navigationFactory, string routeName = null)
            => navigationFactory?.Invoke(Application.CurrentProject.Navigation)?.ToRouteString(Url, routeName);


        /// <summary>
        /// 重定向到区域导航首页。
        /// </summary>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        public IActionResult RedirectToAreaNavigationIndex()
            => RedirectToAreaNavigation(nav => nav.Index);

        /// <summary>
        /// 重定向到区域导航。
        /// </summary>
        /// <param name="navigationFactory">指定定位导航的工厂方法。</param>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        public IActionResult RedirectToAreaNavigation(Func<IProjectNavigation, NavigationDescriptor> navigationFactory)
            => Redirect(GetAreaNavigationPath(navigationFactory));

        /// <summary>
        /// 重定向到本地 URL 或默认路径。
        /// </summary>
        /// <param name="returnUrl">给定的返回 URL。</param>
        /// <param name="defaultPath">给定的默认路径（可选）。</param>
        /// <returns>返回 <see cref="IActionResult"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "returnUrl")]
        public IActionResult RedirectToLocalUrlOrDefaultPath(string returnUrl, string defaultPath = null)
        {
            if (returnUrl.IsEmpty() && defaultPath.IsNotEmpty())
                return Redirect(defaultPath);

            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            // 如果默认路径为空，则使用站点地图首页路径
            return Redirect(defaultPath.NotEmptyOrDefault(() => GetAreaNavigationPath(nav => nav.Index)));
        }

    }
}
