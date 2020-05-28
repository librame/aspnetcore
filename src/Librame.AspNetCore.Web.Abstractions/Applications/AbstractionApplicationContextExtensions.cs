#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace Librame.AspNetCore.Web.Applications
{
    using Descriptors;
    using Projects;

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
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        public static ProjectDescriptor SetCurrentProject(this IApplicationContext context, ViewContext viewContext)
            => context.SetCurrentProject(viewContext?.HttpContext);

        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="httpContext">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        public static ProjectDescriptor SetCurrentProject(this IApplicationContext context, HttpContext httpContext)
            => context?.Project.SetCurrent(httpContext?.GetRouteData()?.Values.AsRouteDescriptor()?.Area);

        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IApplicationContext"/>。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        public static ProjectDescriptor SetCurrentProject(this IApplicationContext context, AbstractRouteDescriptor route)
            => context?.Project.SetCurrent(route?.Area);
    }
}
