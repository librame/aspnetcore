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
using System.Collections.Concurrent;

namespace Librame.AspNetCore
{
    /// <summary>
    /// URI 定位符。
    /// </summary>
    public class UriLocator : AbstractUriLocator
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractUriLocator"/>。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        public UriLocator(Uri uri)
            : base(uri)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="UriLocator"/>。
        /// </summary>
        /// <param name="scheme">给定的协议。</param>
        /// <param name="host">给定的主机。</param>
        /// <param name="path">给定的路径（可选）。</param>
        /// <param name="query">给定的查询（可选）。</param>
        /// <param name="anchor">给定的锚点（可选）。</param>
        protected UriLocator(string scheme, HostString host,
            PathString path = default, QueryString query = default, string anchor = null)
            : base(scheme, host, path, query, anchor)
        {
        }


        /// <summary>
        /// 使用指定的新协议新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newScheme">给定的新协议。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public override IUriLocator NewScheme(string newScheme)
        {
            return new UriLocator(newScheme, Host, Path, Query, Anchor);
        }

        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newHost">给定的新主机。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public override IUriLocator NewHost(HostString newHost)
        {
            return new UriLocator(Scheme, newHost, Path, Query, Anchor);
        }

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newPath">给定的新路径。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public override IUriLocator NewPath(PathString newPath)
        {
            return new UriLocator(Scheme, Host, newPath, Query, Anchor);
        }

        /// <summary>
        /// 使用指定的新查询新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newQuery">给定的新查询。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public override IUriLocator NewQuery(QueryString newQuery)
        {
            return new UriLocator(Scheme, Host, Path, newQuery, Anchor);
        }

        /// <summary>
        /// 使用指定的查询参数数组新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="queriesAction">给定的改变查询参数集合动作（内部支持对参数值的特殊字符进行转码处理）。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public override IUriLocator NewQueries(Action<ConcurrentDictionary<string, string>> queriesAction)
        {
            var queries = FromQueryString(Query.ToString());
            queriesAction.Invoke(queries);

            var queryString = ToQueryString(queries);
            return new UriLocator(Scheme, Host, Path, new QueryString(queryString), Anchor);
        }

        /// <summary>
        /// 使用指定的新锚点新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newAnchor">给定以 # 开始的新锚点。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public override IUriLocator NewAnchor(string newAnchor)
        {
            return new UriLocator(Scheme, Host, Path, Query, newAnchor);
        }


        /// <summary>
        /// 隐式转换为字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriLocator"/>。</param>
        public static implicit operator string(UriLocator locator)
        {
            return locator.ToString();
        }

        /// <summary>
        /// 显式转换为 URI 定位器。
        /// </summary>
        /// <param name="uriString">给定的绝对 URI 字符串。</param>
        public static explicit operator UriLocator(string uriString)
        {
            if (Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
                return new UriLocator(uri);

            throw new ArgumentException($"Invalid uri string: {uriString}.");
        }
        /// <summary>
        /// 显式转换为 URI 定位器。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        public static explicit operator UriLocator(Uri uri)
        {
            return new UriLocator(uri);
        }

    }
}
