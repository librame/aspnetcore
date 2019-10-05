#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 路由描述符。
    /// </summary>
    public class RouteDescriptor : IEquatable<RouteDescriptor>
    {
        /// <summary>
        /// 构造一个 <see cref="RouteDescriptor"/>。
        /// </summary>
        /// <param name="url">给定的 URL。</param>
        public RouteDescriptor(string url)
        {
            Url = url.NotEmpty(nameof(url));
        }

        /// <summary>
        /// 构造一个 <see cref="RouteDescriptor"/>。
        /// </summary>
        /// <param name="page">给定的页面路径。</param>
        /// <param name="area">给定的区域。</param>
        public RouteDescriptor(string page, string area)
            : this(id: null, action: null, controller: null, page, area)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="RouteDescriptor"/>。
        /// </summary>
        /// <param name="action">给定的动作。</param>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="area">给定的区域。</param>
        public RouteDescriptor(string action, string controller, string area)
            : this(id: null, action, controller, page: null, area)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="RouteDescriptor"/>。
        /// </summary>
        /// <param name="id">给定的查询参数。</param>
        /// <param name="action">给定的动作。</param>
        /// <param name="controller">给定的控制器。</param>
        /// <param name="page">给定的页面路径。</param>
        /// <param name="area">给定的区域。</param>
        internal RouteDescriptor(string id, string action, string controller, string page, string area)
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
        public string Id { get; set; }

        /// <summary>
        /// 页面。
        /// </summary>
        public string Page { get; private set; }

        /// <summary>
        /// 动作。
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// 控制器。
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        /// 区域。
        /// </summary>
        public string Area { get; private set; }

        /// <summary>
        /// URL。
        /// </summary>
        public string Url { get; private set; }


        /// <summary>
        /// 是页面模式。
        /// </summary>
        public virtual bool IsPage
            => Page.IsNotEmpty();

        /// <summary>
        /// 是 MVC 模式。
        /// </summary>
        public virtual bool IsMvc
            => Action.IsNotEmpty() || Controller.IsNotEmpty();


        /// <summary>
        /// 视图名称。
        /// </summary>
        public virtual string ViewName
            => IsPage ? Path.GetFileNameWithoutExtension(Page) : Action;


        /// <summary>
        /// 改变页面。
        /// </summary>
        /// <param name="newPage">给定的新页面。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public RouteDescriptor ChangePage(string newPage)
        {
            Page = newPage.NotEmpty(nameof(newPage));
            return this;
        }

        /// <summary>
        /// 改变动作。
        /// </summary>
        /// <param name="newAction">给定的新动作。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public RouteDescriptor ChangeAction(string newAction)
        {
            Action = newAction.NotEmpty(nameof(newAction));
            return this;
        }

        /// <summary>
        /// 改变动作。
        /// </summary>
        /// <param name="newController">给定的新控制器。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public RouteDescriptor ChangeController(string newController)
        {
            Controller = newController.NotEmpty(nameof(newController));
            return this;
        }

        /// <summary>
        /// 改变区域。
        /// </summary>
        /// <param name="newArea">给定的新区域（可选；默认清空区域）。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public RouteDescriptor ChangeArea(string newArea = null)
        {
            Area = newArea;
            return this;
        }

        /// <summary>
        /// 改变 URL。
        /// </summary>
        /// <param name="newUrl">给定的新 URL。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public RouteDescriptor ChangeUrl(string newUrl)
        {
            Url = newUrl.NotEmpty(nameof(newUrl));
            return this;
        }


        /// <summary>
        /// 用新区域以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newArea">给定的新区域（可选；默认清空区域）。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public RouteDescriptor NewArea(string newArea = null)
            => new RouteDescriptor(Id, Action, Controller, Page, newArea);


        /// <summary>
        /// 是指定的视图。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool IsView(RouteDescriptor route)
            => IsView(route.ViewName);

        /// <summary>
        /// 是指定的视图。
        /// </summary>
        /// <param name="viewName">给定的视图名称。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool IsView(string viewName)
            => ViewName.Equals(viewName, StringComparison.OrdinalIgnoreCase);


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(RouteDescriptor other)
            => ToString() == other.ToString();

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is RouteDescriptor other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode();


        /// <summary>
        /// 转换为字符串（按照默认路由模式）。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            if (Area.IsNotEmpty())
                sb.Append($"/{Area}");

            if (Controller.IsNotEmpty())
                sb.Append($"/{Controller}");

            if (Action.IsNotEmpty())
                sb.Append($"/{Action}");

            if (Id.IsNotEmpty())
                sb.Append($"/{Id}");

            return ToString(sb.ToString());
        }

        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <param name="actionLink">给定的动作链接。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string ToString(string actionLink)
        {
            if (IsPage)
            {
                var sb = new StringBuilder();

                if (Area.IsNotEmpty())
                    sb.Append($"/{Area}");

                sb.Append($"{Page}");

                if (Id.IsNotEmpty())
                    sb.Append($"/{Id}");

                return sb.ToString();
            }

            if (IsMvc)
                return actionLink;

            return Url;
        }

        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <param name="urlHelper">给定的 <see cref="IUrlHelper"/>。</param>
        /// <returns>返回字符串。</returns>
        public virtual string ToString(IUrlHelper urlHelper)
        {
            urlHelper.NotNull(nameof(urlHelper));

            var actionLink = urlHelper.Link(Area, new
            {
                area = Area,
                controller = Controller,
                action = Action,
                id = Id
            });

            return ToString(actionLink);
        }


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="a">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="b">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator ==(RouteDescriptor a, RouteDescriptor b)
            => (a?.Equals(b)).Value;

        /// <summary>
        /// 是否不等。
        /// </summary>
        /// <param name="a">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="b">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator !=(RouteDescriptor a, RouteDescriptor b)
            => !(a?.Equals(b)).Value;


        /// <summary>
        /// 隐式转换为字符串。
        /// </summary>
        /// <param name="descriptor">给定的 <see cref="RouteDescriptor"/>。</param>
        public static implicit operator string(RouteDescriptor descriptor)
            => descriptor?.ToString();
    }
}
