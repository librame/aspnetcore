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

namespace Librame.AspNetCore.Web.Projects
{
    using Descriptors;

    /// <summary>
    /// 抽象项目上下文静态扩展。
    /// </summary>
    public static class AbstractionProjectContextExtensions
    {
        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IProjectContext"/>。</param>
        /// <param name="viewContext">给定的 <see cref="ViewContext"/>。</param>
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        public static ProjectDescriptor SetCurrent(this IProjectContext context, ViewContext viewContext)
            => context.SetCurrent(viewContext?.HttpContext);

        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IProjectContext"/>。</param>
        /// <param name="httpContext">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        public static ProjectDescriptor SetCurrent(this IProjectContext context, HttpContext httpContext)
            => context?.SetCurrent(httpContext?.GetRouteData()?.Values.AsRouteDescriptor()?.Area);

        /// <summary>
        /// 设置当前项目。
        /// </summary>
        /// <param name="context">给定的 <see cref="IProjectContext"/>。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="ProjectDescriptor"/>。</returns>
        public static ProjectDescriptor SetCurrent(this IProjectContext context, AbstractRouteDescriptor route)
            => context?.SetCurrent(route?.Area);
    }
}
