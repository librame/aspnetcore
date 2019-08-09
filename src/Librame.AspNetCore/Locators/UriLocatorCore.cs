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
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// URI 定位符核心。
    /// </summary>
    public class UriLocatorCore : UriLocator, IUriLocatorCore
    {
        /// <summary>
        /// 构造一个 <see cref="UriLocatorCore"/>。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        public UriLocatorCore(Uri uri)
            : base(uri)
        {
            HostString = new HostString(Host);
            PathString = new PathString(Path);
            QueryString = new QueryString(Query);
        }

        /// <summary>
        /// 构造一个 <see cref="UriLocatorCore"/>。
        /// </summary>
        /// <param name="scheme">给定的协议。</param>
        /// <param name="host">给定可能包含端口号的主机。</param>
        /// <param name="path">给定以 / 开始的路径（可选）。</param>
        /// <param name="query">给定以 ? 开始的查询（可选）。</param>
        /// <param name="anchor">给定以 # 开始的锚点（可选）。</param>
        protected UriLocatorCore(string scheme, HostString host,
            PathString path = default, QueryString query = default, string anchor = null)
            : base(scheme, host.ToString(), path.ToString(), query.ToString(), anchor)
        {
            HostString = host;
            PathString = path;
            QueryString = query;
        }


        /// <summary>
        /// 可能包含端口号的主机。
        /// </summary>
        public HostString HostString { get; private set; }

        /// <summary>
        /// 以 / 开始的路径。
        /// </summary>
        public PathString PathString { get; private set; }

        /// <summary>
        /// 以 ? 开始的查询。
        /// </summary>
        public QueryString QueryString { get; private set; }

        /// <summary>
        /// 查询参数集合。
        /// </summary>
        public ConcurrentDictionary<string, string> Queries
            => FromQuery(Query);


        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="newHost">给定可能包含端口号的新主机。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public IUriLocatorCore ChangeHost(HostString newHost)
        {
            ChangeHost(newHost.ToString());
            HostString = newHost;
            return this;
        }

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="newPath">给定以 / 开始的新路径。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public IUriLocatorCore ChangePath(PathString newPath)
        {
            ChangePath(newPath.ToString());
            PathString = newPath;
            return this;
        }

        /// <summary>
        /// 改变查询。
        /// </summary>
        /// <param name="newQuery">给定以 ? 开始的新查询。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public IUriLocatorCore ChangeQuery(QueryString newQuery)
        {
            ChangeQuery(newQuery.ToString());
            QueryString = newQuery;
            return this;
        }


        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newHost">给定可能包含端口号的新主机。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public virtual IUriLocatorCore NewHost(HostString newHost)
        {
            return new UriLocatorCore(Scheme, newHost, PathString, QueryString, Anchor);
        }

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newPath">给定以 / 开始的新路径。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public virtual IUriLocatorCore NewPath(PathString newPath)
        {
            return new UriLocatorCore(Scheme, HostString, newPath, QueryString, Anchor);
        }

        /// <summary>
        /// 使用指定的新查询新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newQuery">给定以 ? 开始的新查询。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        public virtual IUriLocatorCore NewQuery(QueryString newQuery)
        {
            return new UriLocatorCore(Scheme, HostString, PathString, newQuery, Anchor);
        }


        /// <summary>
        /// 比较是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="IUriLocatorCore"/>。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool Equals(IUriLocatorCore other)
        {
            return base.Equals(other);
        }


        /// <summary>
        /// 隐式转换为字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UriLocatorCore"/>。</param>
        public static implicit operator string(UriLocatorCore locator)
        {
            return locator.ToString();
        }

        /// <summary>
        /// 显式转换为 URI 定位器核心。
        /// </summary>
        /// <param name="uriString">给定的绝对 URI 字符串。</param>
        public static explicit operator UriLocatorCore(string uriString)
        {
            if (uriString.IsAbsoluteUri(out Uri result))
                return new UriLocatorCore(result);

            throw new ArgumentException($"Invalid absolute uri string: {uriString}.");
        }
        /// <summary>
        /// 显式转换为 URI 定位器核心。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        public static explicit operator UriLocatorCore(Uri uri)
        {
            return new UriLocatorCore(uri);
        }


        /// <summary>
        /// 组合 URI。
        /// </summary>
        /// <param name="scheme">给定的协议。</param>
        /// <param name="host">给定的主机。</param>
        /// <param name="path">给定的路径（可选）。</param>
        /// <param name="query">给定的查询（可选）。</param>
        /// <param name="anchor">给定以 # 开始的锚点（可选）。</param>
        /// <returns>返回 <see cref="Uri"/>。</returns>
        public static Uri CombineUri(string scheme, HostString host,
            PathString path = default, QueryString query = default, string anchor = null)
        {
            return CombineUri(scheme, host.ToString(), path.ToString(), query.ToString(), anchor);
        }

    }
}
