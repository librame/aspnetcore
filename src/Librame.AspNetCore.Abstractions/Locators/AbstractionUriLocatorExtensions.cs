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
    public static class AbstractionUriLocatorExtensions
    {
        /// <summary>
        /// 改变主机字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newHost">给定的新主机字符串。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeHost(this IUriLocator locator, string newHost)
        {
            return locator.ChangeHost(new HostString(newHost));
        }

        /// <summary>
        /// 改变路径字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newPath">给定以 / 开始的新路径字符串。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangePath(this IUriLocator locator, string newPath)
        {
            return locator.ChangePath(new PathString(newPath));
        }

        /// <summary>
        /// 改变查询字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newQuery">给定以 ? 开始的新查询字符串。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeQuery(this IUriLocator locator, string newQuery)
        {
            return locator.ChangeQuery(new QueryString(newQuery));
        }


        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newHost">给定的新主机字符串。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewHost(this IUriLocator locator, string newHost)
        {
            return locator.NewHost(new HostString(newHost));
        }

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newPath">给定以 / 开始的新路径字符串。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewPath(this IUriLocator locator, string newPath)
        {
            return locator.NewPath(new PathString(newPath));
        }

        /// <summary>
        /// 使用指定的新查询字符串新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newQuery">给定以 ? 开始的新查询字符串。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewQuery(this IUriLocator locator, string newQuery)
        {
            return locator.NewQuery(new QueryString(newQuery));
        }


        /// <summary>
        /// 改变协议。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newSchemeFactory">给定的新协议字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeScheme(this IUriLocator locator, Func<string, string> newSchemeFactory)
        {
            newSchemeFactory.NotNull(nameof(newSchemeFactory));

            var newScheme = newSchemeFactory.Invoke(locator.Scheme);

            return locator.ChangeScheme(newScheme);
        }

        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newHostFactory">给定的新主机字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeHost(this IUriLocator locator, Func<string, string> newHostFactory)
        {
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.Host.ToString());

            return locator.ChangeHost(newHost);
        }
        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newHostFactory">给定的新主机工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeHost(this IUriLocator locator, Func<HostString, HostString> newHostFactory)
        {
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.Host);

            return locator.ChangeHost(newHost);
        }

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newPathFactory">给定以 / 开始的新路径字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangePath(this IUriLocator locator, Func<string, string> newPathFactory)
        {
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.Path.ToString());

            return locator.ChangePath(newPath);
        }
        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newPathFactory">给定的新路径工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangePath(this IUriLocator locator, Func<PathString, PathString> newPathFactory)
        {
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.Path);

            return locator.ChangePath(newPath);
        }

        /// <summary>
        /// 改变查询字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newQueryFactory">给定以 ? 开始的新查询字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeQuery(this IUriLocator locator, Func<string, string> newQueryFactory)
        {
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.Query.ToString());

            return locator.ChangeQuery(newQuery);
        }
        /// <summary>
        /// 改变查询字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newQueryFactory">给定的新查询工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeQuery(this IUriLocator locator, Func<QueryString, QueryString> newQueryFactory)
        {
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.Query);

            return locator.ChangeQuery(newQuery);
        }

        /// <summary>
        /// 改变锚点。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newAnchorFactory">给定以 # 开始的新锚点工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator ChangeAnchor(this IUriLocator locator, Func<string, string> newAnchorFactory)
        {
            newAnchorFactory.NotNull(nameof(newAnchorFactory));

            var newAnchor = newAnchorFactory.Invoke(locator.Anchor);

            return locator.ChangeAnchor(newAnchor);
        }


        /// <summary>
        /// 使用指定的新协议新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newSchemeFactory">给定的新协议字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewScheme(this IUriLocator locator, Func<string, string> newSchemeFactory)
        {
            newSchemeFactory.NotNull(nameof(newSchemeFactory));

            var newScheme = newSchemeFactory.Invoke(locator.Scheme);

            return locator.NewScheme(newScheme);
        }

        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newHostFactory">给定的新主机字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewHost(this IUriLocator locator, Func<string, string> newHostFactory)
        {
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.Host.ToString());

            return locator.NewHost(newHost);
        }
        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newHostFactory">给定的新主机工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewHost(this IUriLocator locator, Func<HostString, HostString> newHostFactory)
        {
            newHostFactory.NotNull(nameof(newHostFactory));

            var newHost = newHostFactory.Invoke(locator.Host);

            return locator.NewHost(newHost);
        }

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newPathFactory">给定以 / 开始的新路径字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewPath(this IUriLocator locator, Func<string, string> newPathFactory)
        {
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.Path.ToString());

            return locator.NewPath(newPath);
        }
        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newPathFactory">给定的新路径工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewPath(this IUriLocator locator, Func<PathString, PathString> newPathFactory)
        {
            newPathFactory.NotNull(nameof(newPathFactory));

            var newPath = newPathFactory.Invoke(locator.Path);

            return locator.NewPath(newPath);
        }

        /// <summary>
        /// 使用指定的新查询字符串新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newQueryFactory">给定以 ? 开始的新查询字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewQuery(this IUriLocator locator, Func<string, string> newQueryFactory)
        {
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.Query.ToString());

            return locator.NewQuery(newQuery);
        }
        /// <summary>
        /// 使用指定的新查询字符串新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newQueryFactory">给定的新查询工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewQuery(this IUriLocator locator, Func<QueryString, QueryString> newQueryFactory)
        {
            newQueryFactory.NotNull(nameof(newQueryFactory));

            var newQuery = newQueryFactory.Invoke(locator.Query);

            return locator.NewQuery(newQuery);
        }

        /// <summary>
        /// 使用指定的新锚点新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="locator">给定的 <see cref="IUriLocator"/>。</param>
        /// <param name="newAnchorFactory">给定以 # 开始的新锚点字符串工厂方法。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public static IUriLocator NewAnchor(this IUriLocator locator, Func<string, string> newAnchorFactory)
        {
            newAnchorFactory.NotNull(nameof(newAnchorFactory));

            var newAnchor = newAnchorFactory.Invoke(locator.Anchor);

            return locator.NewAnchor(newAnchor);
        }

    }
}
