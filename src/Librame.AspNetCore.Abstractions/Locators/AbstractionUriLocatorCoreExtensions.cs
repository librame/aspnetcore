#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// 抽象 URI 定位器静态扩展。
    /// </summary>
    public static class AbstractionUriLocatorCoreExtensions
    {
        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocatorCore"/>。</param>
        /// <param name="newHostFactory">给定的新主机工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public static IUriLocatorCore ChangeHost(this IUriLocatorCore locator, Func<HostString, HostString> newHostFactory)
        {
            locator.NotNull(nameof(locator));
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.HostString);

            return locator.ChangeHost(newHost);
        }

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocatorCore"/>。</param>
        /// <param name="newPathFactory">给定的新路径工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public static IUriLocatorCore ChangePath(this IUriLocatorCore locator, Func<PathString, PathString> newPathFactory)
        {
            locator.NotNull(nameof(locator));
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.PathString);

            return locator.ChangePath(newPath);
        }

        /// <summary>
        /// 改变查询字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocatorCore"/>。</param>
        /// <param name="newQueryFactory">给定的新查询工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public static IUriLocatorCore ChangeQuery(this IUriLocatorCore locator, Func<QueryString, QueryString> newQueryFactory)
        {
            locator.NotNull(nameof(locator));
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.QueryString);

            return locator.ChangeQuery(newQuery);
        }


        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocatorCore"/>。</param>
        /// <param name="newHostFactory">给定的新主机工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public static IUriLocatorCore NewHost(this IUriLocatorCore locator, Func<HostString, HostString> newHostFactory)
        {
            locator.NotNull(nameof(locator));
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.HostString);

            return locator.NewHost(newHost);
        }

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocatorCore"/>。</param>
        /// <param name="newPathFactory">给定的新路径工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public static IUriLocatorCore NewPath(this IUriLocatorCore locator, Func<PathString, PathString> newPathFactory)
        {
            locator.NotNull(nameof(locator));
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.PathString);

            return locator.NewPath(newPath);
        }

        /// <summary>
        /// 使用指定的新查询字符串新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocatorCore"/>。</param>
        /// <param name="newQueryFactory">给定的新查询工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public static IUriLocatorCore NewQuery(this IUriLocatorCore locator, Func<QueryString, QueryString> newQueryFactory)
        {
            locator.NotNull(nameof(locator));
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.QueryString);

            return locator.NewQuery(newQuery);
        }

    }
}
