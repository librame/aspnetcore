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
    using Extensions.Core;

    /// <summary>
    /// URL 定位符核心。
    /// </summary>
    public class UrlLocatorCore : UrlLocator
    {
        /// <summary>
        /// 构造一个 <see cref="UrlLocatorCore"/>。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        public UrlLocatorCore(Uri uri)
            : base(uri)
        {
            HostString = new HostString(Host);
            PathString = new PathString(Path);
            QueryString = new QueryString(Query);
        }

        /// <summary>
        /// 构造一个 <see cref="UrlLocatorCore"/>。
        /// </summary>
        /// <param name="scheme">给定的协议。</param>
        /// <param name="host">给定可能包含端口号的主机。</param>
        /// <param name="path">给定以 / 开始的路径（可选）。</param>
        /// <param name="query">给定以 ? 开始的查询（可选）。</param>
        /// <param name="anchor">给定以 # 开始的锚点（可选）。</param>
        protected UrlLocatorCore(string scheme, HostString host,
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
        /// 改变主机。
        /// </summary>
        /// <param name="newHost">给定可能包含端口号的新主机。</param>
        /// <returns>返回 <see cref="UrlLocatorCore"/>。</returns>
        public UrlLocatorCore ChangeHost(HostString newHost)
        {
            ChangeHost(newHost.ToString());
            HostString = newHost;
            return this;
        }

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="newPath">给定以 / 开始的新路径。</param>
        /// <returns>返回 <see cref="UrlLocatorCore"/>。</returns>
        public UrlLocatorCore ChangePath(PathString newPath)
        {
            base.ChangePath(newPath);
            PathString = newPath;
            return this;
        }

        /// <summary>
        /// 改变查询。
        /// </summary>
        /// <param name="newQuery">给定以 ? 开始的新查询。</param>
        /// <returns>返回 <see cref="UrlLocatorCore"/>。</returns>
        public UrlLocatorCore ChangeQuery(QueryString newQuery)
        {
            ChangeQuery(newQuery.ToString());
            QueryString = newQuery;
            return this;
        }


        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="UrlLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newHost">给定可能包含端口号的新主机。</param>
        /// <returns>返回 <see cref="UrlLocatorCore"/>。</returns>
        public virtual UrlLocatorCore NewHost(HostString newHost)
            => new UrlLocatorCore(Scheme, newHost, PathString, QueryString, Anchor);

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="UrlLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newPath">给定以 / 开始的新路径。</param>
        /// <returns>返回 <see cref="UrlLocatorCore"/>。</returns>
        public virtual UrlLocatorCore NewPath(PathString newPath)
            => new UrlLocatorCore(Scheme, HostString, newPath, QueryString, Anchor);

        /// <summary>
        /// 使用指定的新查询新建一个 <see cref="UrlLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newQuery">给定以 ? 开始的新查询。</param>
        /// <returns>返回 <see cref="UrlLocatorCore"/>。</returns>
        public virtual UrlLocatorCore NewQuery(QueryString newQuery)
            => new UrlLocatorCore(Scheme, HostString, PathString, newQuery, Anchor);


        /// <summary>
        /// 比较是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="UrlLocatorCore"/>。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool Equals(UrlLocatorCore other)
            => base.Equals(other);


        /// <summary>
        /// 隐式转换为字符串。
        /// </summary>
        /// <param name="locator">给定的 <see cref="UrlLocatorCore"/>。</param>
        public static implicit operator string(UrlLocatorCore locator)
            => locator.ToString();

        /// <summary>
        /// 显式转换为 URI 定位器核心。
        /// </summary>
        /// <param name="uriString">给定的绝对 URI 字符串。</param>
        public static explicit operator UrlLocatorCore(string uriString)
        {
            if (uriString.IsAbsoluteUri(out Uri result))
                return new UrlLocatorCore(result);

            throw new ArgumentException($"Invalid absolute uri string: {uriString}.");
        }
        /// <summary>
        /// 显式转换为 URI 定位器核心。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        public static explicit operator UrlLocatorCore(Uri uri)
            => new UrlLocatorCore(uri);


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
            => CombineUri(scheme, host.ToString(), path, query.ToString(), anchor);
    }
}
