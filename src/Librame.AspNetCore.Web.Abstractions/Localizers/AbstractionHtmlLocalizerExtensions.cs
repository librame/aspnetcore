#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Web.Descriptors;
using Librame.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Microsoft.AspNetCore.Mvc.Localization
{
    /// <summary>
    /// HTML 定位器静态扩展。
    /// </summary>
    public static class AbstractionHtmlLocalizerExtensions
    {
        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static LocalizedHtmlString GetString<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> propertyExpression)
            where TResource : class
        {
            localizer.NotNull(nameof(localizer));
            return localizer[propertyExpression.AsPropertyName()];
        }

        /// <summary>
        /// 获取字符串。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="propertyExpression">给定的属性表达式。</param>
        /// <param name="arguments">给定的参数数组。</param>
        /// <returns>返回 <see cref="LocalizedHtmlString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static LocalizedHtmlString GetString<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> propertyExpression, params object[] arguments)
            where TResource : class
        {
            localizer.NotNull(nameof(localizer));
            return localizer[propertyExpression.AsPropertyName(), arguments];
        }


        #region GetNavigation

        /// <summary>
        /// 获取带有属性表达式动作的路由导航。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{TResource}"/>。</param>
        /// <param name="localizerProperty">给定的属性表达式。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public static NavigationDescriptor GetNavigationWithRouteAction<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> localizerProperty, ActionRouteDescriptor route)
            where TResource : class
        {
            var localizerName = localizerProperty.AsPropertyName();
            return new NavigationDescriptor(localizer, route?.WithAction(localizerName), localizerName);
        }

        /// <summary>
        /// 获取带有属性表达式动作和新控制器的路由导航。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{TResource}"/>。</param>
        /// <param name="localizerProperty">给定的属性表达式。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <param name="newController">给定的新控制器。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public static NavigationDescriptor GetNavigationWithRouteActionAndController<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> localizerProperty, ActionRouteDescriptor route, string newController)
            where TResource : class
        {
            var localizerName = localizerProperty.AsPropertyName();
            return new NavigationDescriptor(localizer, route?.WithActionAndController(localizerName, newController), localizerName);
        }

        /// <summary>
        /// 获取带有属性表达式页面名称的路由导航，如果页面有路径部分则保持不变（如将页面路由 Page 的值 '/Path/PageName' 改变为 '/Path/NewPageName'）。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{TResource}"/>。</param>
        /// <param name="localizerProperty">给定的属性表达式。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public static NavigationDescriptor GetNavigationWithRoutePageName<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> localizerProperty, PageRouteDescriptor route)
            where TResource : class
        {
            var localizerName = localizerProperty.AsPropertyName();
            return new NavigationDescriptor(localizer, route?.WithPageName(localizerName), localizerName);
        }


        /// <summary>
        /// 获取路由导航。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{TResource}"/>。</param>
        /// <param name="localizerProperty">给定的属性表达式。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static NavigationDescriptor GetNavigation<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> localizerProperty, AbstractRouteDescriptor route)
            where TResource : class
            => new NavigationDescriptor(localizer, route, localizerProperty.AsPropertyName());

        /// <summary>
        /// 获取路由导航。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="route">给定的 <see cref="AbstractRouteDescriptor"/>。</param>
        /// <param name="localizerName">给定的本地化名称（可选；默认使用路由视图名称）。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static NavigationDescriptor GetNavigation(this IHtmlLocalizer localizer,
            AbstractRouteDescriptor route, string localizerName = null)
            => new NavigationDescriptor(localizer, route, localizerName);


        /// <summary>
        /// 获取初始化链接导航。
        /// </summary>
        /// <typeparam name="TResource">指定的资源类型。</typeparam>
        /// <typeparam name="TProperty">指定的属性类型。</typeparam>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer{TResource}"/>。</param>
        /// <param name="localizerProperty">给定的本地化属性表达式。</param>
        /// <param name="initialLink">给定的初始化链接。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public static NavigationDescriptor GetNavigation<TResource, TProperty>(this IHtmlLocalizer<TResource> localizer,
            Expression<Func<TResource, TProperty>> localizerProperty, string initialLink)
            where TResource : class
            => localizer.GetNavigation(localizerProperty.AsPropertyName(), initialLink);

        /// <summary>
        /// 获取初始化链接导航。
        /// </summary>
        /// <param name="localizer">给定的 <see cref="IHtmlLocalizer"/>。</param>
        /// <param name="localizerName">给定的本地化名称。</param>
        /// <param name="initialLink">给定的初始化链接。</param>
        /// <returns>返回 <see cref="NavigationDescriptor"/>。</returns>
        public static NavigationDescriptor GetNavigation(this IHtmlLocalizer localizer,
            string localizerName, string initialLink)
            => new NavigationDescriptor(localizer, localizerName, initialLink);

        #endregion

    }
}
