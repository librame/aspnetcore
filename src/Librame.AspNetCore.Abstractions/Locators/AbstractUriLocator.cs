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
        /// 协议。
        /// </summary>
        public string Scheme { get; private set; }

        /// <summary>
        /// 主机。
        /// </summary>
        public HostString Host { get; private set; }

        /// <summary>
        /// 路径。
        /// </summary>
        public PathString Path { get; private set; }

        /// <summary>
        /// 查询字符串。
        /// </summary>
        public QueryString Query { get; private set; }

        /// <summary>
        /// 锚点。
        /// </summary>
        public string Anchor { get; private set; }


        /// <summary>
        /// 重写源实例。
        /// </summary>
        public override Uri Source
        {
            get { return new Uri($"{Scheme}{Uri.SchemeDelimiter}{Host}{Path}{Query}{Anchor}"); }
        }


        /// <summary>
        /// 改变协议。
        /// </summary>
        /// <param name="newScheme">给定的新协议。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeScheme(string newScheme)
        {
            Scheme = newScheme;
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
        /// 改变查询字符串。
        /// </summary>
        /// <param name="newQuery">给定的新查询字符串。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeQuery(QueryString newQuery)
        {
            Query = newQuery;
            return this;
        }

        /// <summary>
        /// 改变查询参数集合
        /// </summary>
        /// <param name="queriesAction">给定的改变查询参数集合动作。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeQueries(Action<Dictionary<string, string>> queriesAction)
        {
            var queries = FromQueryString(Query.ToString());
            queriesAction.Invoke(queries);

            var queryString = ToQueryString(queries);
            return ChangeQuery(new QueryString(queryString));
        }

        /// <summary>
        /// 改变锚点。
        /// </summary>
        /// <param name="newAnchor">给定的新锚点。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        public IUriLocator ChangeAnchor(string newAnchor)
        {
            Anchor = newAnchor;
            return this;
        }


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
        /// 从查询字符串还原查询参数集合。
        /// </summary>
        /// <param name="queryString">给定的查询参数字符串。</param>
        /// <returns>返回 <see cref="Dictionary{String, String}"/>。</returns>
        public static Dictionary<string, string> FromQueryString(string queryString)
        {
            var queries = new Dictionary<string, string>();

            if (queryString.IsNotNullOrEmpty())
            {
                queryString.TrimStart('?').Split('&').ForEach(segment =>
                {
                    if (segment.IsNotNullOrEmpty())
                    {
                        var pair = segment.SplitPair(); // "="
                        queries.Add(pair.Key, pair.Value);
                    }
                });
            }

            return queries;
        }

        /// <summary>
        /// 将查询参数集合转换为查询字符串。
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
                sb.Append(pair.Value);

                if (i < count - 1)
                    sb.Append("&");
            });

            return sb.ToString();
        }

    }
}
