#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Descriptors
{
    using Extensions;

    /// <summary>
    /// <see cref="AbstractRouteDescriptor"/> 静态扩展。
    /// </summary>
    public static class AbstractionRouteDescriptorExtensions
    {
        /// <summary>
        /// 转换为路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="AbstractRouteDescriptor"/>。</returns>
        public static AbstractRouteDescriptor AsRouteDescriptor(this IDictionary<string, string> routeValues)
            => AbstractRouteDescriptor.GeneralParse(routeValues);

        /// <summary>
        /// 转换为路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="AbstractRouteDescriptor"/>。</returns>
        public static AbstractRouteDescriptor AsRouteDescriptor(this RouteValueDictionary routeValues)
            => AbstractRouteDescriptor.GeneralParse(routeValues);


        /// <summary>
        /// 转换为动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public static ActionRouteDescriptor AsActionRouteDescriptor(this IDictionary<string, string> routeValues)
            => ActionRouteDescriptor.Parse(routeValues);

        /// <summary>
        /// 转换为动作路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="ActionRouteDescriptor"/>。</returns>
        public static ActionRouteDescriptor AsActionRouteDescriptor(this RouteValueDictionary routeValues)
            => ActionRouteDescriptor.Parse(routeValues);


        /// <summary>
        /// 转换为页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        public static PageRouteDescriptor AsPageRouteDescriptor(this IDictionary<string, string> routeValues)
            => PageRouteDescriptor.Parse(routeValues);

        /// <summary>
        /// 转换为页面路由描述符。
        /// </summary>
        /// <param name="routeValues">给定的路由值集合。</param>
        /// <returns>返回 <see cref="PageRouteDescriptor"/>。</returns>
        public static PageRouteDescriptor AsPageRouteDescriptor(this RouteValueDictionary routeValues)
            => PageRouteDescriptor.Parse(routeValues);


        /// <summary>
        /// 带有区域。
        /// </summary>
        /// <typeparam name="TDescriptor">指定的路由描述符类型。</typeparam>
        /// <param name="descriptor">给定的 <typeparamref name="TDescriptor"/>。</param>
        /// <param name="newArea">给定的新区域。</param>
        /// <returns>返回 <typeparamref name="TDescriptor"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static TDescriptor WithArea<TDescriptor>(this TDescriptor descriptor, string newArea)
            where TDescriptor : AbstractRouteDescriptor
        {
            descriptor.NotNull(nameof(descriptor));

            descriptor.Area = newArea;

            return descriptor;
        }

        /// <summary>
        /// 带有参数。
        /// </summary>
        /// <typeparam name="TDescriptor">指定的路由描述符类型。</typeparam>
        /// <param name="descriptor">给定的 <typeparamref name="TDescriptor"/>。</param>
        /// <param name="newId">给定的新参数。</param>
        /// <returns>返回 <typeparamref name="TDescriptor"/>。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        public static TDescriptor WithId<TDescriptor>(this TDescriptor descriptor, string newId)
            where TDescriptor : AbstractRouteDescriptor
        {
            descriptor.NotNull(nameof(descriptor));

            descriptor.Id = newId;

            return descriptor;
        }

    }
}
