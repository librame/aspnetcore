#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace Librame.AspNetCore.Web.Applications
{
    using Projects;
    using Routings;

    /// <summary>
    /// 抽象应用上下文静态扩展。
    /// </summary>
    public static class AbstractionApplicationContextExtensions
    {
        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回 <see cref="IProjectInfo"/>。</returns>
        public static (IProjectInfo Info, IProjectNavigation Navigation) SetCurrentProject(this IApplicationContext context, ViewContext viewContext)
            => context.SetCurrentProject(viewContext?.HttpContext);

        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="httpContext">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回 <see cref="IProjectInfo"/>。</returns>
        public static (IProjectInfo Info, IProjectNavigation Navigation) SetCurrentProject(this IApplicationContext context, HttpContext httpContext)
            => (context?.Project.SetCurrent(httpContext?.GetRouteData()?.AsRouteDescriptor()?.Area)).Value;

        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="IProjectInfo"/>。</returns>
        public static (IProjectInfo Info, IProjectNavigation Navigation) SetCurrentProject(this IApplicationContext context, RouteDescriptor route)
            => (context?.Project.SetCurrent(route?.Area)).Value;
    }
}
