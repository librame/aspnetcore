#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Microsoft.AspNetCore.Routing;

namespace Librame.AspNetCore
{
    /// <summary>
    /// <see cref="RouteData"/> 静态扩展。
    /// </summary>
    public static class RouteDataExtensions
    {
        /// <summary>
        /// 当作路由描述符。
        /// </summary>
        /// <param name="routeData">给定的 <see cref="RouteData"/>。</param>
        /// <returns>返回路由信息。</returns>
        public static RouteDescriptor AsRouteDescriptor(this RouteData routeData)
        {
            routeData.NotNull(nameof(routeData));

            var area = routeData.Values["area"]?.ToString();
            var controller = routeData.Values["controller"]?.ToString();
            var action = routeData.Values["action"]?.ToString();
            var id = routeData.Values["id"]?.ToString();

            return new RouteDescriptor(id, action, controller, area)
            {
                Page = routeData.Values["page"]?.ToString()
            };
        }

    }
}
