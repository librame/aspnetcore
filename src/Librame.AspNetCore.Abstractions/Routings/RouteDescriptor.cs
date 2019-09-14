#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.IO;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// 路由描述符。
    /// </summary>
    public class RouteDescriptor
    {
        /// <summary>
        /// 构造一个 <see cref="RouteDescriptor"/> 描述符。
        /// </summary>
        /// <param name="id">给定的查询参数。</param>
        /// <param name="action">给定的动作。</param>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="page">给定的页面路径。</param>
        /// <param name="area">给定的区域。</param>
        public RouteDescriptor(string id, string action, string controller, string page, string area)
        {
            Id = id;
            Action = action;
            Controller = controller;
            Area = area;
            Page = page;
        }


        /// <summary>
        /// 查询参数。
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 页面。
        /// </summary>
        public string Page { get; }

        /// <summary>
        /// 动作。
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// 控制器。
        /// </summary>
        public string Controller { get; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; }


        /// <summary>
        /// 是页面模式。
        /// </summary>
        public bool IsPage
            => Page.IsNotNullOrEmpty();

        /// <summary>
        /// 是 MVC 模式。
        /// </summary>
        public bool IsMvc
            => Action.IsNotNullOrEmpty();


        /// <summary>
        /// 是当前视图。
        /// </summary>
        /// <param name="viewName">给定的视图名称。</param>
        /// <returns>返回布尔值。</returns>
        public bool IsCurrentView(string viewName)
        {
            viewName.NotNullOrEmpty(nameof(viewName));

            if (IsPage)
            {
                var pageName = Path.GetFileNameWithoutExtension(Page);
                return pageName.Equals(viewName, StringComparison.OrdinalIgnoreCase);
            }

            return Action.Equals(viewName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
