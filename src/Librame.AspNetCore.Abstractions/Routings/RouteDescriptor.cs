#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore
{
    /// <summary>
    /// 路由描述符。
    /// </summary>
    public class RouteDescriptor
    {
        /// <summary>
        /// 构造一个 <see cref="RouteDescriptor"/> 描述符。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        /// <param name="controller">给定的控制器（可选）。</param>
        public RouteDescriptor(string action, string controller = null)
            : this(null, action, controller, null)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="RouteDescriptor"/> 描述符。
        /// </summary>
        /// <param name="id">给定的查询参数。</param>
        /// <param name="action">给定的动作。</param>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="area">给定的区域。</param>
        public RouteDescriptor(string id, string action, string controller, string area)
        {
            Id = id;
            Action = action;
            Controller = controller;
            Area = area;
            Page = string.Empty;
        }


        /// <summary>
        /// 查询参数。
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 页面。
        /// </summary>
        public string Page { get; set; }

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
}
