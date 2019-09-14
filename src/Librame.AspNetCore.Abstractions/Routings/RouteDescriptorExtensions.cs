#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

namespace Librame.Extensions
{
    /// <summary>
    /// 路由描述符静态扩展。
    /// </summary>
    public static class RouteDescriptorExtensions
    {
        /// <summary>
        /// 当作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的 <see cref="IDictionary{String, String}"/>。</param>
        /// <returns>返回路由信息。</returns>
        public static RouteDescriptor AsRouteDescriptor(this IDictionary<string, string> routeValues)
        {
            routeValues.NotNull(nameof(routeValues));

            var area = routeValues.TryGetValue("area", out string _area)
                ? _area : null;

            var controller = routeValues.TryGetValue("controller", out string _controller)
                ? _controller : null;

            var action = routeValues.TryGetValue("action", out string _action)
                ? _action : null;

            var page = routeValues.TryGetValue("page", out string _page)
                ? _page : null;

            var id = routeValues.TryGetValue("id", out string _id)
                ? _id : null;

            return new RouteDescriptor(id, action, controller, page, area);
        }

        /// <summary>
        /// 当作路由描述符。
        /// </summary>
        /// <param name="routeData">给定的 <see cref="RouteData"/>。</param>
        /// <returns>返回路由信息。</returns>
        public static RouteDescriptor AsRouteDescriptor(this RouteData routeData)
        {
            routeData.NotNull(nameof(routeData));

            var area = routeData.Values.TryGetValue("area", out object _area)
                ? _area?.ToString() : null;

            var controller = routeData.Values.TryGetValue("controller", out object _controller)
                ? _controller?.ToString() : null;

            var action = routeData.Values.TryGetValue("action", out object _action)
                ? _action?.ToString() : null;

            var page = routeData.Values.TryGetValue("page", out object _page)
                ? _page?.ToString() : null;

            var id = routeData.Values.TryGetValue("id", out object _id)
                ? _id?.ToString() : null;

            return new RouteDescriptor(id, action, controller, page, area);
        }

    }
}
