#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Concurrent;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// URI 定位器静态扩展。
    /// </summary>
    public static class UriLocatorExtensions
    {
        /// <summary>
        /// 转换为 URI 定位器。
        /// </summary>
        /// <param name="uriString">给定的 URI 字符串。</param>
        /// <param name="newScheme">给定的新协议（可选）。</param>
        /// <param name="newHost">给定的新主机（可选）。</param>
        /// <param name="newPath">给定的新路径（可选）。</param>
        /// <param name="newQuery">给定的新查询（可选）。</param>
        /// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator AsUriLocator(this string uriString, string newScheme = null,
            HostString newHost = default, PathString newPath = default, QueryString newQuery = default,
            string newAnchor = null)
        {
            var locator = (UriLocator)uriString;

            return locator.ChangeParameters(newScheme, newHost, newPath, newQuery, newAnchor);
        }

        ///// <summary>
        ///// 转换为 URI 定位器。
        ///// </summary>
        ///// <param name="uriString">给定的 URI 字符串。</param>
        ///// <param name="newScheme">给定的新协议（可选）。</param>
        ///// <param name="newHost">给定的新主机（可选）。</param>
        ///// <param name="newPath">给定的新路径（可选）。</param>
        ///// <param name="queriesAction">给定的改变查询参数集合动作。</param>
        ///// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        ///// <returns>返回 <see cref="IUriLocator"/>。</returns>
        //public static IUriLocator AsUriLocator(this string uriString, string newScheme = null,
        //    HostString newHost = default, PathString newPath = default,
        //    Action<ConcurrentDictionary<string, string>> queriesAction = null, string newAnchor = null)
        //{
        //    var locator = (UriLocator)uriString;

        //    return locator.ChangeParameters(newScheme, newHost, newPath, queriesAction, newAnchor);
        //}


        /// <summary>
        /// 转换为 URI 定位器。
        /// </summary>
        /// <param name="uri">给定的 URI 字符串。</param>
        /// <param name="newScheme">给定的新协议（可选）。</param>
        /// <param name="newHost">给定的新主机（可选）。</param>
        /// <param name="newPath">给定的新路径（可选）。</param>
        /// <param name="newQuery">给定的新查询（可选）。</param>
        /// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator AsUriLocator(this Uri uri, string newScheme = null,
            HostString newHost = default, PathString newPath = default, QueryString newQuery = default,
            string newAnchor = null)
        {
            var locator = (UriLocator)uri;

            return locator.ChangeParameters(newScheme, newHost, newPath, newQuery, newAnchor);
        }

        ///// <summary>
        ///// 转换为 URI 定位器。
        ///// </summary>
        ///// <param name="uri">给定的 URI 字符串。</param>
        ///// <param name="newScheme">给定的新协议（可选）。</param>
        ///// <param name="newHost">给定的新主机（可选）。</param>
        ///// <param name="newPath">给定的新路径（可选）。</param>
        ///// <param name="queriesAction">给定的改变查询参数集合动作。</param>
        ///// <param name="newAnchor">给定以 # 开始的新锚点（可选）。</param>
        ///// <returns>返回 <see cref="IUriLocator"/>。</returns>
        //public static IUriLocator AsUriLocator(this Uri uri, string newScheme = null,
        //    HostString newHost = default, PathString newPath = default,
        //    Action<ConcurrentDictionary<string, string>> queriesAction = null, string newAnchor = null)
        //{
        //    var locator = (UriLocator)uri;

        //    return locator.ChangeParameters(newScheme, newHost, newPath, queriesAction, newAnchor);
        //}


        private static IUriLocator ChangeParameters(this IUriLocator locator, string newScheme = null,
            HostString newHost = default, PathString newPath = default, QueryString newQuery = default,
            string newAnchor = null)
        {
            if (!newScheme.IsNullOrEmpty())
                locator.ChangeScheme(newScheme);

            if (newHost.HasValue)
                locator.ChangeHost(newHost);

            if (newPath.HasValue)
                locator.ChangePath(newPath);

            if (newQuery.HasValue)
                locator.ChangeQuery(newQuery);

            if (!newAnchor.IsNullOrEmpty())
                locator.ChangeAnchor(newAnchor);

            return locator;
        }

        private static IUriLocator ChangeParameters(this IUriLocator locator, string newScheme = null,
            HostString newHost = default, PathString newPath = default,
            Action<ConcurrentDictionary<string, string>> queriesAction = null, string newAnchor = null)
        {
            if (!newScheme.IsNullOrEmpty())
                locator.ChangeScheme(newScheme);

            if (newHost.HasValue)
                locator.ChangeHost(newHost);

            if (newPath.HasValue)
                locator.ChangePath(newPath);

            if (queriesAction.IsNotNull())
                locator.ChangeQueries(queriesAction);

            if (!newAnchor.IsNullOrEmpty())
                locator.ChangeAnchor(newAnchor);

            return locator;
        }

    }
}
