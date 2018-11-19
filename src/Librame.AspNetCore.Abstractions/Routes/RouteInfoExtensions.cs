#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Routing;

namespace Librame.Routes
{
    /// <summary>
    /// 路由数据静态扩展。
    /// </summary>
    public static class RouteInfoExtensions
    {

        /// <summary>
        /// 当作路由信息。
        /// </summary>
        /// <param name="routeData">给定的路由数据。</param>
        /// <returns>返回路由信息。</returns>
        public static RouteInfo AsRouteInfo(this RouteData routeData)
        {
            var id = routeData.Values["id"]?.ToString();
            var action = routeData.Values["action"]?.ToString();
            var controller = routeData.Values["controller"]?.ToString();
            var area = routeData.Values["area"]?.ToString();

            return new RouteInfo
            {
                Id = id,
                Action = action,
                Controller = controller,
                Area = area
            };
        }

    }
}
