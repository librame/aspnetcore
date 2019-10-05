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
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 导航描述符。
    /// </summary>
    /// <typeparam name="TResource">指定的资源类型。</typeparam>
    public class NavigationDescriptor<TResource> : NavigationDescriptor
        where TResource : class, IResource
    {
        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/> 实例。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="propertyExpression">给定的本地化属性表达式。</param>
        /// <param name="icon">给定的图标样式（可选）。</param>
        /// <param name="children">给定的子级导航（可选；默认列表不为空，长度为“0”）。</param>
        /// <param name="visibilityFactory">给定的可见性工厂方法（可选；默认可见）。</param>
        public NavigationDescriptor(RouteDescriptor route, IExpressionLocalizer<TResource> localizer,
            Expression<Func<TResource, string>> propertyExpression,
            string icon = null, IList<NavigationDescriptor> children = null,
            Func<dynamic, NavigationDescriptor, bool> visibilityFactory = null)
            : base(route, localizer, propertyExpression.AsPropertyName(), icon, children, visibilityFactory)
        {
        }
    }


    /// <summary>
    /// 导航描述符。
    /// </summary>
    public class NavigationDescriptor : IEquatable<NavigationDescriptor>
    {
        private readonly IStringLocalizer _localizer = null;
        private readonly string _name = null;

        private string _text = null;
        private string _targetTitle = null;


        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/> 实例。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="name">给定的本地化名称（可选；默认使用 <see cref="RouteDescriptor.ViewName"/>）。</param>
        /// <param name="icon">给定的图标样式（可选）。</param>
        /// <param name="children">给定的子级导航（可选；默认列表不为空，长度为“0”）。</param>
        /// <param name="visibilityFactory">给定的可见性工厂方法（可选；默认可见）。</param>
        public NavigationDescriptor(RouteDescriptor route, IStringLocalizer localizer, string name = null,
            string icon = null, IList<NavigationDescriptor> children = null,
            Func<dynamic, NavigationDescriptor, bool> visibilityFactory = null)
            : this(route, icon, children, visibilityFactory,
                  activeCssClassNameFactory: null, activeStyleFactory: null,
                  tagId: null, tagName: null, tagTarget: null, tagTitle: null)
        {
            _localizer = localizer.NotNull(nameof(localizer));
            _name = name.NotEmptyOrDefault(route.ViewName);
        }

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
            : this(route, icon, children, visibilityFactory,
                  activeCssClassNameFactory: null, activeStyleFactory: null,
                  tagId: null, tagName: null, tagTarget: null, tagTitle: null)
        {
            _text = text.NotEmpty(nameof(text));
        }

        private NavigationDescriptor(RouteDescriptor route, string icon = null,
            IList<NavigationDescriptor> children = null,
            Func<dynamic, NavigationDescriptor, bool> visibilityFactory = null,
            Func<dynamic, NavigationDescriptor, string> activeCssClassNameFactory = null,
            Func<dynamic, NavigationDescriptor, string> activeStyleFactory = null,
            string tagId = null, string tagName = null, string tagTarget = null, string tagTitle = null)
        {
            Route = route.NotNull(nameof(route));

            Icon = icon ?? "glyphicon glyphicon-link";
            Children = children ?? new List<NavigationDescriptor>();

            VisibilityFactory = visibilityFactory ?? ((page, nav) => true);
            ActiveCssClassNameFactory = activeCssClassNameFactory;
            ActiveStyleFactory = activeStyleFactory;

            _targetTitle = tagTitle;

            if (tagTarget.IsNotEmpty())
                tagTarget = CheckTarget(tagTarget);

            TagTarget = tagTarget ?? "_self";
            TagName = tagName;
            TagId = tagId;
        }


        /// <summary>
        /// 路由。
        /// </summary>
        /// <value>返回 <see cref="RouteDescriptor"/>。</value>
        public RouteDescriptor Route { get; private set; }

        /// <summary>
        /// 文本。
        /// </summary>
        public string Text
            => _localizer.IsNotNull() && _name.IsNotEmpty() ? _localizer[_name] : _text;

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
        /// 标签标识。
        /// </summary>
        /// <value>返回标签标识或名称。</value>
        public string TagId { get; set; }

        /// <summary>
        /// 标签名称。
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 标签标题。
        /// </summary>
        public string TagTitle
            => _targetTitle.NotEmptyOrDefault(Text);

        /// <summary>
        /// 标签目标。
        /// </summary>
        public string TagTarget { get; private set; }

        /// <summary>
        /// 标签目标格式化。
        /// </summary>
        public string TagTargetFormat
            => TagTarget.IsNotEmpty() ? $" target='{TagTarget}'" : string.Empty;


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
            newText.NotEmpty(nameof(newText));

            if (_targetTitle == Text)
                _targetTitle = newText;

            _text = newText;
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
            Children = newChildren.NotEmpty(nameof(newChildren));
            return this;
        }

        /// <summary>
        /// 改变目标。
        /// </summary>
        /// <param name="newTagTarget">给定的新目标。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public NavigationDescriptor ChangeTagTarget(string newTagTarget)
        {
            TagTarget = CheckTarget(newTagTarget);
            return this;
        }

        /// <summary>
        /// 检测目标。
        /// </summary>
        /// <param name="target">给定的目标。</param>
        /// <returns>返回目标或抛出异常。</returns>
        protected string CheckTarget(string target)
        {
            if (!target.StartsWith("_", StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid target.", nameof(target));

            return target;
        }


        /// <summary>
        /// 用新区域以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newArea">给定的新区域（可选；默认清空区域）。</param>
        /// <returns>返回 <see cref="RouteDescriptor"/>。</returns>
        public virtual NavigationDescriptor NewRouteArea(string newArea = null)
        {
            NavigationDescriptor descriptor;

            if (_localizer.IsNotNull() && _name.IsNotEmpty())
            {
                descriptor = new NavigationDescriptor(Route.NewArea(newArea), _localizer, _name, Icon, Children, VisibilityFactory);
            }
            else
            {
                descriptor = new NavigationDescriptor(Route.NewArea(newArea), Text, Icon, Children, VisibilityFactory);
            }

            descriptor.ActiveCssClassNameFactory = ActiveCssClassNameFactory;
            descriptor.ActiveStyleFactory = ActiveStyleFactory;
            descriptor.TagId = TagId;
            descriptor.TagName = TagName;
            descriptor.TagTarget = TagTarget;
            descriptor._targetTitle = TagTitle;

            return descriptor;
        }


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
            => ToString() == other?.ToString();

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
            => ToString().GetHashCode(StringComparison.OrdinalIgnoreCase);


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
