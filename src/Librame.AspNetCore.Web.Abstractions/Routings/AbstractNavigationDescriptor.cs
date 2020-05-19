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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Routings
{
    using Extensions;

    /// <summary>
    /// 抽象导航描述符可选配置信息。
    /// </summary>
    public abstract class AbstractNavigationDescriptor : INavigationDescriptorOptionalInfo, IEquatable<AbstractNavigationDescriptor>
    {
        private readonly NavigationDescriptorOptional _optional
               = new NavigationDescriptorOptional();

        private IStringLocalizer _localizer = null;
        private string _localizerName = null;

        private string _text = null;


        /// <summary>
        /// 构造一个 <see cref="AbstractNavigationDescriptor"/>。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="localizerName">给定用于导航的字符串定位器名称（可选；默认使用路由视图名称）。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        protected AbstractNavigationDescriptor(RouteDescriptor route,
            IStringLocalizer localizer, string localizerName = null,
            Action<INavigationDescriptorOptional> optionalAction = null)
            : this(route, optionalAction)
        {
            _localizer = localizer.NotNull(nameof(localizer));
            _localizerName = localizerName.NotEmptyOrDefault(route.ViewName);
        }

        /// <summary>
        /// 构造一个 <see cref="AbstractNavigationDescriptor"/>。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="text">给定用于导航的文本。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        protected AbstractNavigationDescriptor(RouteDescriptor route,
            string text,
            Action<INavigationDescriptorOptional> optionalAction = null)
            : this(route, optionalAction)
        {
            _text = text.NotEmpty(nameof(text));
        }

        /// <summary>
        /// 构造一个 <see cref="AbstractNavigationDescriptor"/>。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        private AbstractNavigationDescriptor(RouteDescriptor route,
            Action<INavigationDescriptorOptional> optionalAction = null)
        {
            Route = route.NotNull(nameof(route));
            optionalAction?.Invoke(_optional);
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
            => _localizer.IsNotNull() ? _localizer[_localizerName] : _text;

        /// <summary>
        /// 图标。
        /// </summary>
        public string Icon
            => _optional.Icon;

        /// <summary>
        /// 子级导航。
        /// </summary>
        public List<AbstractNavigationDescriptor> Children
            => _optional.Children;


        /// <summary>
        /// 可见性工厂方法（默认显示）。
        /// </summary>
        public Func<dynamic, AbstractNavigationDescriptor, bool> VisibilityFactory
            => _optional.VisibilityFactory;

        /// <summary>
        /// 激活类名工厂方法（默认为空 CSS 类名）。
        /// </summary>
        public Func<dynamic, AbstractNavigationDescriptor, string> ActiveCssClassNameFactory
            => _optional.ActiveCssClassNameFactory;

        /// <summary>
        /// 激活样式工厂方法（默认为空样式）。
        /// </summary>
        public Func<dynamic, AbstractNavigationDescriptor, string> ActiveStyleFactory
            => _optional.ActiveStyleFactory;


        /// <summary>
        /// 标签标识。
        /// </summary>
        /// <value>返回标签标识或名称。</value>
        public string TagId
            => _optional.TagId;

        /// <summary>
        /// 标签名称。
        /// </summary>
        public string TagName
            => _optional.TagName;

        /// <summary>
        /// 标签标题。
        /// </summary>
        public string TagTitle
            => _optional.TagTitle.NotEmptyOrDefault(Text);

        /// <summary>
        /// 标签目标。
        /// </summary>
        public string TagTarget
            => _optional.TagTarget;

        /// <summary>
        /// 标签目标格式化。
        /// </summary>
        public string TagTargetFormat
            => TagTarget.IsNotEmpty() ? $" target='{TagTarget}'" : string.Empty;


        /// <summary>
        /// 改变路由。
        /// </summary>
        /// <param name="newRoute">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="AbstractNavigationDescriptor"/>。</returns>
        internal AbstractNavigationDescriptor ChangeRoute(RouteDescriptor newRoute)
        {
            Route = newRoute.NotNull(nameof(newRoute));
            return this;
        }


        /// <summary>
        /// 改变可选配置。
        /// </summary>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作。</param>
        /// <returns>返回 <see cref="AbstractNavigationDescriptor"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        internal AbstractNavigationDescriptor ChangeOptional(Action<INavigationDescriptorOptional> optionalAction)
        {
            optionalAction.NotNull(nameof(optionalAction));

            optionalAction.Invoke(_optional);
            return this;
        }


        /// <summary>
        /// 改变本地化定位器。
        /// </summary>
        /// <param name="newLocalizer">给定的新 <see cref="IStringLocalizer"/>。</param>
        /// <returns>返回 <see cref="AbstractNavigationDescriptor"/>。</returns>
        internal AbstractNavigationDescriptor ChangeLocalizer(IStringLocalizer newLocalizer)
        {
            _localizer = newLocalizer.NotNull(nameof(newLocalizer));
            return this;
        }

        /// <summary>
        /// 改变本地化定位器名称。
        /// </summary>
        /// <param name="newLocalizerName">给定的新本地化定位器名称。</param>
        /// <returns>返回 <see cref="AbstractNavigationDescriptor"/>。</returns>
        internal AbstractNavigationDescriptor ChangeLocalizerName(string newLocalizerName)
        {
            _localizerName = newLocalizerName.NotEmpty(nameof(newLocalizerName));
            return this;
        }


        /// <summary>
        /// 改变文本。
        /// </summary>
        /// <param name="newText">给定的新文本。</param>
        /// <returns>返回 <see cref="AbstractNavigationDescriptor"/>。</returns>
        internal AbstractNavigationDescriptor ChangeText(string newText)
        {
            newText.NotEmpty(nameof(newText));

            // 如果标签标题使用的文本，则关联更新
            if (_optional.TagTitle == Text)
                _optional.ChangeTagTitle(newText);

            _text = newText;
            return this;
        }


        /// <summary>
        /// 用新路由以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newRoute">给定的新 <see cref="RouteDescriptor"/>。</param>
        /// <param name="newLocalizerName">给定用于导航的新字符串定位器名称。</param>
        /// <returns>返回 <see cref="AbstractNavigationDescriptor"/>。</returns>
        internal AbstractNavigationDescriptor NewRoute(RouteDescriptor newRoute, string newLocalizerName)
        {
            newLocalizerName.NotEmpty(nameof(newLocalizerName));

            var descriptor = CreateDescriptor(newRoute, _localizer, newLocalizerName);
            _optional.EnsurePopulate(descriptor._optional);

            return descriptor;
        }

        /// <summary>
        /// 创建导航描述符。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="localizerName">给定用于导航的字符串定位器名称。</param>
        /// <returns>返回 <see cref="AbstractNavigationDescriptor"/>。</returns>
        protected abstract AbstractNavigationDescriptor CreateDescriptor(RouteDescriptor route,
            IStringLocalizer localizer, string localizerName);


        /// <summary>
        /// 转换为路由链接。
        /// </summary>
        /// <param name="urlHelper">给定的 <see cref="IUrlHelper"/>。</param>
        /// <param name="routeName">给定的路由名称（可选）。</param>
        /// <returns>返回字符串。</returns>
        public virtual string ToRouteString(IUrlHelper urlHelper, string routeName = null)
            => Route.ToRouteString(urlHelper, routeName);


        /// <summary>
        /// 是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool Equals(AbstractNavigationDescriptor other)
            => ToString() == other?.ToString();

        /// <summary>
        /// 重写是否相等。
        /// </summary>
        /// <param name="obj">给定要比较的对象。</param>
        /// <returns>返回布尔值。</returns>
        public override bool Equals(object obj)
            => (obj is AbstractNavigationDescriptor other) ? Equals(other) : false;


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
        /// <param name="a">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        /// <param name="b">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator ==(AbstractNavigationDescriptor a, AbstractNavigationDescriptor b)
            => (a?.Equals(b)).Value;

        /// <summary>
        /// 是否不等。
        /// </summary>
        /// <param name="a">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        /// <param name="b">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool operator !=(AbstractNavigationDescriptor a, AbstractNavigationDescriptor b)
            => !(a?.Equals(b)).Value;


        /// <summary>
        /// 隐式转换为字符串。
        /// </summary>
        /// <param name="info">给定的 <see cref="AbstractNavigationDescriptor"/>。</param>
        public static implicit operator string(AbstractNavigationDescriptor info)
            => info?.ToString();
    }
}
