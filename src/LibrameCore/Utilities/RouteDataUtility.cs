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

namespace LibrameStandard.Utilities
{
    /// <summary>
    /// 路由信息。
    /// </summary>
    public class RouteInfo
    {
        /// <summary>
        /// 编号。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 动作。
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 控制器。
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; set; }
    }


    /// <summary>
    /// <see cref="RouteData"/> 实用工具。
    /// </summary>
    public static class RouteDataUtility
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
