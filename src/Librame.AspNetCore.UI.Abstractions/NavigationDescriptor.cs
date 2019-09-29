#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    using Extensions;

    /// <summary>
    /// 导航描述符。
    /// </summary>
    public class NavigationDescriptor : IEquatable<NavigationDescriptor>
    {
        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/> 实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="route"/> or <paramref name="text"/> is null.
        /// </exception>
        /// <param name="route">给定的路由。</param>
        /// <param name="text">给定的文本。</param>
        /// <param name="icon">给定的图标样式（可选）。</param>
        /// <param name="children">给定的子级导航（可选；默认列表不为空，长度为“0”）。</param>
        /// <param name="visibilityFactory">给定的可见性工厂方法（可选；默认可见）。</param>
        public NavigationDescriptor(RouteDescriptor route, string text,
            string icon = null, IList<NavigationDescriptor> children = null,
            Func<dynamic, NavigationDescriptor, bool> visibilityFactory = null)
            : this(route, text, icon, children, visibilityFactory,
                  activeCssClassNameFactory: null, activeStyleFactory: null,
                  id: null, name: null, target: null, title: null)
        {
        }

        private NavigationDescriptor(RouteDescriptor route, string text,
            string icon = null, IList<NavigationDescriptor> children = null,
            Func<dynamic, NavigationDescriptor, bool> visibilityFactory = null,
            Func<dynamic, NavigationDescriptor, string> activeCssClassNameFactory = null,
            Func<dynamic, NavigationDescriptor, string> activeStyleFactory = null,
            string id = null, string name = null, string target = null, string title = null)
        {
            Route = route.NotNull(nameof(route));
            Text = text.NotNullOrEmpty(nameof(text));

            Icon = icon ?? "glyphicon glyphicon-link";
            Children = children ?? new List<NavigationDescriptor>();

            VisibilityFactory = visibilityFactory ?? ((page, nav) => true);
            ActiveCssClassNameFactory = activeCssClassNameFactory;
            ActiveStyleFactory = activeStyleFactory;

            if (target.IsNotNullOrEmpty())
                target = CheckTarget(target);

            Target = target ?? "_self";
            Name = name;
            Id = id;
            Title = title ?? Text;
        }


        /// <summary>
        /// 路由。
        /// </summary>
        /// <value>返回 <see cref="RouteDescriptor"/>。</value>
        public RouteDescriptor Route { get; private set; }

        /// <summary>
        /// 文本。
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 子级导航。
        /// </summary>
        public IList<NavigationDescriptor> Children { get; private set; }


        /// <summary>
        /// 可见性工厂方法。
        /// </summary>
        public Func<dynamic, NavigationDescriptor, bool> VisibilityFactory { get; set; }

        /// <summary>
        /// 激活类名工厂方法。
        /// </summary>
        public Func<dynamic, NavigationDescriptor, string> ActiveCssClassNameFactory { get; set; }

        /// <summary>
        /// 激活样式工厂方法。
        /// </summary>
        public Func<dynamic, NavigationDescriptor, string> ActiveStyleFactory { get; set; }


        /// <summary>
        /// 标识。
        /// </summary>
        /// <value>返回标识或名称。</value>
        public string Id { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 目标。
        /// </summary>
        public string Target { get; private set; }

        /// <summary>
        /// 目标格式化。
        /// </summary>
        public string TargetFormat
            => Target.IsNotNullOrEmpty() ? $" target='{Target}'" : string.Empty;


        /// <summary>
        /// 改变路由。
        /// </summary>
        /// <param name="newRoute">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeRoute(RouteDescriptor newRoute)
        {
            Route = newRoute.NotNull(nameof(newRoute));
            return this;
        }

        /// <summary>
        /// 改变文本。
        /// </summary>
        /// <param name="newText">给定的新文本。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeText(string newText)
        {
            newText.NotNullOrEmpty(nameof(newText));

            if (Title == Text)
                Title = newText;

            Text = newText;
            return this;
        }

        /// <summary>
        /// 改变子级。
        /// </summary>
        /// <param name="newChildrenFactory">给定新子级的工厂方法。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeChildren(Func<IList<NavigationDescriptor>, IList<NavigationDescriptor>> newChildrenFactory)
        {
            var newChildren = newChildrenFactory.NotNull(nameof(newChildrenFactory)).Invoke(Children);
            return ChangeChildren(newChildren);
        }
        /// <summary>
        /// 改变子级。
        /// </summary>
        /// <param name="newChildren">给定的新 <see cref="IList{NavigationDescriptor}"/>。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeChildren(IList<NavigationDescriptor> newChildren)
        {
            Children = newChildren.NotNullOrEmpty(nameof(newChildren));
            return this;
        }

        /// <summary>
        /// 改变目标。
        /// </summary>
        /// <param name="newTarget">给定的新目标。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeTarget(string newTarget)
        {
            Target = CheckTarget(newTarget);
            return this;
        }

        /// <summary>
        /// 检测目标。
        /// </summary>
        /// <param name="target">给定的目标。</param>
        /// <returns>返回目标或抛出异常。</returns>
        protected string CheckTarget(string target)
        {
            if (!target.StartsWith("_"))
                throw new ArgumentException("Invalid target.", nameof(target));

            return target;
        }


        /// <summary>
        /// 用新区域以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newArea">给定的新区域（可选；默认清空区域）。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public virtual NavigationDescriptor NewRouteArea(string newArea = null)
            => new NavigationDescriptor(Route.NewArea(newArea), Text, Icon, Children,
                VisibilityFactory, ActiveCssClassNameFactory, ActiveStyleFactory, Id, Name, Target, Title);


        /// <summary>
        /// 转换为路由链接。
        /// </summary>
        /// <param name="urlHelper">给定的 <see cref="IUrlHelper"/>。</param>
        /// <returns>返回字符串。</returns>
        public virtual string ToRouteString(IUrlHelper urlHelper)
            => Route.ToString(urlHelper);


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(NavigationDescriptor other)
            => ToString() == other.ToString();

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is NavigationDescriptor other) ? Equals(other) : false;


        /// <summary>
        /// 获取哈希码。
        /// </summary>
        /// <returns>返回 32 位整数。</returns>
        public override int GetHashCode()
            => ToString().GetHashCode();


        /// <summary>
        /// 转换为字符串。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
            => $"{Text}={Route}";


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="a">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <param name="b">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator ==(NavigationDescriptor a, NavigationDescriptor b)
            => (a?.Equals(b)).Value;

        /// <summary>
        /// 是否不等。
        /// </summary>
        /// <param name="a">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <param name="b">给定的 <see cref="NavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator !=(NavigationDescriptor a, NavigationDescriptor b)
            => !(a?.Equals(b)).Value;


        /// <summary>
        /// 隐式转换为字符串。
        /// </summary>
        /// <param name="descriptor">给定的 <see cref="NavigationDescriptor"/>。</param>
        public static implicit operator string(NavigationDescriptor descriptor)
            => descriptor?.ToString();
    }
}
