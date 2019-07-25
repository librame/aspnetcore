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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Librame.AspNetCore
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 抽象 URI 定位符。
    /// </summary>
    public abstract class AbstractUriLocator : AbstractLocator<Uri>, IUriLocator
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractUriLocator"/>。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        protected AbstractUriLocator(Uri uri)
            : base(uri)
        {
            Scheme = uri.Scheme;
            Host = new HostString(uri.Authority);
            Path = new PathString(uri.AbsolutePath); // uri.LocalPath
            Query = new QueryString(uri.Query);
            Anchor = uri.Fragment;
        }

        /// <summary>
        /// 构造一个 <see cref="AbstractUriLocator"/>。
        /// </summary>
        /// <param name="scheme">给定的协议。</param>
        /// <param name="host">给定的主机。</param>
        /// <param name="path">给定的路径（可选）。</param>
        /// <param name="query">给定的查询（可选）。</param>
        /// <param name="anchor">给定的锚点（可选）。</param>
        protected AbstractUriLocator(string scheme, HostString host,
            PathString path = default, QueryString query = default, string anchor = null)
            : base(CombineUri(scheme, host, path, query, anchor))
        {
            Scheme = scheme;
            Host = host;
            Path = path;
            Query = query;
            Anchor = anchor;
        }


        /// <summary>
        /// 协议。
        /// </summary>
        public string Scheme { get; private set; }

        /// <summary>
        /// 主机。
        /// </summary>
        public HostString Host { get; private set; }

        /// <summary>
        /// 主机字符串。
        /// </summary>
        public string HostString => Host.ToString();

        /// <summary>
        /// 以 / 开始的路径。
        /// </summary>
        public PathString Path { get; private set; }

        /// <summary>
        /// 以 / 开始的路径字符串。
        /// </summary>
        public string PathString => Path.ToString();

        /// <summary>
        /// 以 ? 开始的查询。
        /// </summary>
        public QueryString Query { get; private set; }

        /// <summary>
        /// 以 ? 开始的查询字符串。
        /// </summary>
        public string QueryString => Query.ToString();

        /// <summary>
        /// 以 # 开始的锚点。
        /// </summary>
        public string Anchor { get; private set; }


        /// <summary>
        /// 重写源实例。
        /// </summary>
        public override Uri Source
        {
            get { return CombineUri(Scheme, Host, Path, Query, Anchor); }
        }


        /// <summary>
        /// 改变协议。
        /// </summary>
        /// <param name="newScheme">给定的新协议。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeScheme(string newScheme)
        {
            Scheme = newScheme.NotNullOrEmpty(newScheme);
            return this;
        }

        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="newHost">给定的新主机。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeHost(HostString newHost)
        {
            Host = newHost;
            return this;
        }

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="newPath">给定的新路径。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangePath(PathString newPath)
        {
            Path = newPath;
            return this;
        }

        /// <summary>
        /// 改变查询。
        /// </summary>
        /// <param name="newQuery">给定的新查询。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeQuery(QueryString newQuery)
        {
            Query = newQuery;
            return this;
        }

        /// <summary>
        /// 改变查询参数集合。
        /// </summary>
        /// <param name="queriesAction">给定的改变查询参数集合动作（内部支持对参数值的特殊字符进行转码处理）。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeQueries(Action<ConcurrentDictionary<string, string>> queriesAction)
        {
            var queries = FromQueryString(Query.ToString());
            queriesAction.Invoke(queries);

            var queryString = ToQueryString(queries);
            return ChangeQuery(new QueryString(queryString));
        }

        /// <summary>
        /// 改变锚点。
        /// </summary>
        /// <param name="newAnchor">给定以 # 开始的新锚点。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeAnchor(string newAnchor)
        {
            Anchor = newAnchor.NotNullOrEmpty(newAnchor);
            return this;
        }


        /// <summary>
        /// 使用指定的新协议新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newScheme">给定的新协议。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public abstract IUriLocator NewScheme(string newScheme);

        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newHost">给定的新主机。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public abstract IUriLocator NewHost(HostString newHost);

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newPath">给定的新路径。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public abstract IUriLocator NewPath(PathString newPath);

        /// <summary>
        /// 使用指定的新查询新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newQuery">给定的新查询。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public abstract IUriLocator NewQuery(QueryString newQuery);

        /// <summary>
        /// 使用指定的查询参数数组新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="queriesAction">给定的改变查询参数集合动作（内部支持对参数值的特殊字符进行转码处理）。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public abstract IUriLocator NewQueries(Action<ConcurrentDictionary<string, string>> queriesAction);

        /// <summary>
        /// 使用指定的新锚点新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newAnchor">给定以 # 开始的新锚点。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public abstract IUriLocator NewAnchor(string newAnchor);


        /// <summary>
        /// 比较是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="IUriLocator"/>。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool Equals(IUriLocator other)
        {
            return Source == other.Source;
        }


        /// <summary>
        /// 转换为文件。
        /// </summary>
        /// <returns>返回字符串。</returns>
        public override string ToString()
        {
            return Source.ToString();
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
            scheme.NotNullOrEmpty(nameof(scheme));

            return new Uri($"{scheme}{Uri.SchemeDelimiter}{host}{path}{query}{anchor}");
        }


        /// <summary>
        /// 从查询字符串还原查询参数集合（内部支持对参数值的特殊字符进行解码处理）。
        /// </summary>
        /// <param name="queryString">给定的查询参数字符串。</param>
        /// <returns>返回 <see cref="ConcurrentDictionary{String, String}"/>。</returns>
        public static ConcurrentDictionary<string, string> FromQueryString(string queryString)
        {
            var queries = new ConcurrentDictionary<string, string>();

            if (queryString.IsNotNullOrEmpty())
            {
                queryString.TrimStart('?').Split('&').ForEach(segment =>
                {
                    if (segment.IsNotNullOrEmpty())
                    {
                        var pair = segment.SplitPair(); // "="
                        var valueString = pair.Value;

                        if (valueString.IsNotNullOrEmpty())
                            valueString = Uri.UnescapeDataString(valueString);

                        queries.AddOrUpdate(pair.Key, valueString, (key, value) => valueString);
                    }
                });
            }

            return queries;
        }

        /// <summary>
        /// 将查询参数集合转换为查询字符串（内部支持对参数值的特殊字符进行转码处理）。
        /// </summary>
        /// <param name="queries">给定的查询参数集合。</param>
        /// <returns>返回查询字符串。</returns>
        public static string ToQueryString(IEnumerable<KeyValuePair<string, string>> queries)
        {
            var sb = new StringBuilder("?");

            var count = queries.Count();
            queries.ForEach((pair, i) =>
            {
                sb.Append(pair.Key);
                sb.Append("=");

                if (pair.Value.IsNotNullOrEmpty())
                    sb.Append(Uri.EscapeDataString(pair.Value));

                if (i < count - 1)
                    sb.Append("&");
            });

            return Uri.EscapeUriString(sb.ToString());
        }

    }
}
