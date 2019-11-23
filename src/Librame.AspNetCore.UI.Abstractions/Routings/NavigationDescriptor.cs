#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using System;
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
        /// 构造一个 <see cref="NavigationDescriptor{TResource}"/>。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{TResource}"/>。</param>
        /// <param name="propertyExpression">给定用于导航的字符串定位器属性表达式（可选；默认使用路由视图名称）。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        public NavigationDescriptor(RouteDescriptor route,
            IStringLocalizer<TResource> localizer,
            Expression<Func<TResource, string>> propertyExpression = null,
            Action<INavigationDescriptorOptional> optionalAction = null)
            : this(route, localizer, propertyExpression?.AsPropertyName(), optionalAction)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor{TResource}"/>。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer{TResource}"/>。</param>
        /// <param name="localizerName">给定用于导航的字符串定位器名称（可选；默认使用路由视图名称）。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        protected NavigationDescriptor(RouteDescriptor route,
            IStringLocalizer<TResource> localizer, string localizerName = null,
            Action<INavigationDescriptorOptional> optionalAction = null)
            : base(route, localizer, localizerName, optionalAction)
        {
        }


        /// <summary>
        /// 创建导航描述符。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="localizerName">给定用于导航的字符串定位器名称。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        protected override AbstractNavigationDescriptor CreateDescriptor(RouteDescriptor route,
            IStringLocalizer localizer, string localizerName)
            => new NavigationDescriptor<TResource>(route, (IStringLocalizer<TResource>)localizer, localizerName);


        /// <summary>
        /// 用新路由动作以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法（可选；默认使用路由动作）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteAction(Expression<Func<TResource, string>> newActionExpression,
            Func<RouteDescriptor, string> newLocalizerNameFactory = null)
            => this.NewRouteAction(newActionExpression.AsPropertyName(), newLocalizerNameFactory);

        /// <summary>
        /// 用新路由动作以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newLocalizerNameExpression">给定的新定位器名称表达式。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteAction(Expression<Func<TResource, string>> newActionExpression,
            Expression<Func<TResource, string>> newLocalizerNameExpression)
            => this.NewRouteAction(newActionExpression.AsPropertyName(), newLocalizerNameExpression.AsPropertyName());


        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例（默认使用路由动作）。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteController(Expression<Func<TResource, string>> newActionExpression,
            string newController)
        {
            var newAction = newActionExpression.AsPropertyName();
            return this.NewRoute(route => RouteDescriptor.ByController(newAction, newController, route.Area), newAction);
        }

        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例（默认使用路由动作）。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteController(Expression<Func<TResource, string>> newActionExpression,
            string newController, string newArea)
        {
            var newAction = newActionExpression.AsPropertyName();
            return this.NewRoute(route => RouteDescriptor.ByController(newAction, newController, newArea), newAction);
        }


        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法（可选；默认使用路由动作）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteController(Expression<Func<TResource, string>> newActionExpression,
            string newController, Func<RouteDescriptor, string> newLocalizerNameFactory)
            => this.NewRoute(route => RouteDescriptor.ByController(newActionExpression.AsPropertyName(), newController, route.Area), newLocalizerNameFactory);

        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerNameFactory">给定的新定位器名称工厂方法（可选；默认使用路由动作）。</param>
        /// <param name="newArea">给定的新区域（可选）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteController(Expression<Func<TResource, string>> newActionExpression,
            string newController, Func<RouteDescriptor, string> newLocalizerNameFactory, string newArea)
            => this.NewRoute(RouteDescriptor.ByController(newActionExpression.AsPropertyName(), newController, newArea), newLocalizerNameFactory);


        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerNameExpression">给定的新定位器名称表达式。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteController(Expression<Func<TResource, string>> newActionExpression,
            string newController, Expression<Func<TResource, string>> newLocalizerNameExpression)
            => this.NewRoute(route => RouteDescriptor.ByController(newActionExpression.AsPropertyName(), newController, route.Area),
                newLocalizerNameExpression.AsPropertyName());

        /// <summary>
        /// 用新路由动作、控制器、区域（可选）以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newActionExpression">给定的新动作表达式。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <param name="newLocalizerNameExpression">给定的新定位器名称表达式。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRouteController(Expression<Func<TResource, string>> newActionExpression,
            string newController, Expression<Func<TResource, string>> newLocalizerNameExpression, string newArea)
            => this.NewRoute(route => RouteDescriptor.ByController(newActionExpression.AsPropertyName(), newController, newArea),
                newLocalizerNameExpression.AsPropertyName());


        /// <summary>
        /// 用新路由页面以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newPage">给定的新页面。</param>
        /// <param name="newLocalizerNameExpression">给定的新定位器名称表达式。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRoutePage(string newPage,
            Expression<Func<TResource, string>> newLocalizerNameExpression)
            => this.NewRoute(route => RouteDescriptor.ByPage(newPage, route.Area), newLocalizerNameExpression.AsPropertyName());

        /// <summary>
        /// 用新路由页面以及当前其他参数构造一个新实例。
        /// </summary>
        /// <param name="newPage">给定的新页面。</param>
        /// <param name="newLocalizerNameExpression">给定的新定位器名称表达式。</param>
        /// <param name="newArea">给定的新区域（如果为空，则表示清空区域）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor{TResource}"/>。</returns>
        public NavigationDescriptor<TResource> NewRoutePage(string newPage,
            Expression<Func<TResource, string>> newLocalizerNameExpression, string newArea)
            => this.NewRoute(route => RouteDescriptor.ByPage(newPage, newArea), newLocalizerNameExpression.AsPropertyName());
    }


    /// <summary>
    /// 导航描述符。
    /// </summary>
    public class NavigationDescriptor : AbstractNavigationDescriptor
    {
        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/>。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="localizerName">给定用于导航的字符串定位器名称（可选；默认使用路由视图名称）。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        public NavigationDescriptor(RouteDescriptor route,
            IStringLocalizer localizer, string localizerName = null,
            Action<INavigationDescriptorOptional> optionalAction = null)
            : base(route, localizer, localizerName, optionalAction)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="NavigationDescriptor"/>。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="text">给定用于导航的文本。</param>
        /// <param name="optionalAction">给定的 <see cref="INavigationDescriptorOptional"/> 动作（可选）。</param>
        public NavigationDescriptor(RouteDescriptor route,
            string text,
            Action<INavigationDescriptorOptional> optionalAction = null)
            : base(route, text, optionalAction)
        {
        }


        /// <summary>
        /// 创建导航描述符。
        /// </summary>
        /// <param name="route">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <param name="localizer">给定的 <see cref="IStringLocalizer"/>。</param>
        /// <param name="localizerName">给定用于导航的字符串定位器名称。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        protected override AbstractNavigationDescriptor CreateDescriptor(RouteDescriptor route,
            IStringLocalizer localizer, string localizerName)
            => new NavigationDescriptor(route, localizer, localizerName);
    }
}
