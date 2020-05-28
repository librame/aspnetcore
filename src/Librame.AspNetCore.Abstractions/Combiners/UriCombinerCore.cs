#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Librame.Extensions.Core.Combiners
{
    /// <summary>
    /// <see cref="UriCombiner"/> for ASP.NET Core。
    /// </summary>
    public class UriCombinerCore : UriCombiner
    {
        /// <summary>
        /// 构造一个 <see cref="UriCombinerCore"/>。
        /// </summary>
        /// <param name="uri">给定的 <see cref="Uri"/>。</param>
        public UriCombinerCore(Uri uri)
            : base(uri)
        {
            HostString = new HostString(Host);
            PathString = new PathString(Path);
            QueryString = new QueryString(Query);
        }

        /// <summary>
        /// 构造一个 <see cref="UriCombinerCore"/>。
        /// </summary>
        /// <param name="scheme">给定的协议。</param>
        /// <param name="host">给定可能包含端口号的主机。</param>
        /// <param name="path">给定以 / 开始的路径（可选）。</param>
        /// <param name="query">给定以 ? 开始的查询（可选）。</param>
        /// <param name="anchor">给定以 # 开始的锚点（可选）。</param>
        protected UriCombinerCore(string scheme, HostString host,
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
        /// <param name="newHostString">给定的新 <see cref="Microsoft.AspNetCore.Http.HostString"/>。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public UriCombinerCore ChangeHost(HostString newHostString)
        {
            ChangeHost(newHostString.ToString());
            HostString = newHostString;
            return this;
        }

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="newPathString">给定的新 <see cref="Microsoft.AspNetCore.Http.PathString"/>。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public UriCombinerCore ChangePath(PathString newPathString)
        {
            base.ChangePath(newPathString.ToString());
            PathString = newPathString;
            return this;
        }

        /// <summary>
        /// 改变查询。
        /// </summary>
        /// <param name="newQueryString">给定的新 <see cref="Microsoft.AspNetCore.Http.QueryString"/>。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public UriCombinerCore ChangeQuery(QueryString newQueryString)
        {
            ChangeQuery(newQueryString.ToString());
            QueryString = newQueryString;
            return this;
        }


        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="UriCombinerCore"/>。
        /// </summary>
        /// <param name="newHostString">给定的新 <see cref="Microsoft.AspNetCore.Http.HostString"/>。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public UriCombinerCore WithHost(HostString newHostString)
            => new UriCombinerCore(Scheme, newHostString, PathString, QueryString, Anchor);

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="UriCombinerCore"/>。
        /// </summary>
        /// <param name="newPathString">给定的新 <see cref="Microsoft.AspNetCore.Http.PathString"/>。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public UriCombinerCore WithPath(PathString newPathString)
            => new UriCombinerCore(Scheme, HostString, newPathString, QueryString, Anchor);

        /// <summary>
        /// 使用指定的新查询新建一个 <see cref="UriCombinerCore"/>。
        /// </summary>
        /// <param name="newQueryString">给定的新 <see cref="Microsoft.AspNetCore.Http.QueryString"/>。</param>
        /// <returns>返回 <see cref="UriCombinerCore"/>。</returns>
        public UriCombinerCore WithQuery(QueryString newQueryString)
            => new UriCombinerCore(Scheme, HostString, PathString, newQueryString, Anchor);


        /// <summary>
        /// 是指定主机（忽略大小写）。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public bool IsHost(HostString hostString)
            => IsHost(hostString.ToString());


        /// <summary>
        /// 比较是否相等。
        /// </summary>
        /// <param name="other">给定的 <see cref="UriCombinerCore"/>。</param>
        /// <returns>返回布尔值。</returns>
        public virtual bool Equals(UriCombinerCore other)
            => base.Equals(other);


        /// <summary>
        /// 将查询参数集合转换为查询字符串（内部支持对参数值的特殊字符进行转码处理）。
        /// </summary>
        /// <param name="queries">给定的查询参数集合。</param>
        /// <param name="addStartsWithDelimiter">增加以界定符“?”开始的字符串（可选；默认增加）。</param>
        /// <returns>返回查询字符串。</returns>
        public static string ToQuery(IEnumerable<KeyValuePair<string, StringValues>> queries,
            bool addStartsWithDelimiter = true)
        {
            var sb = new StringBuilder();

            if (addStartsWithDelimiter)
                sb.Append(QueryStringDelimiter);

            var count = queries.Count();
            queries.ForEach((pair, i) =>
            {
                sb.Append(pair.Key);
                sb.Append(QueryStringKeyValuePairConnector);

                if (pair.Value.IsNotEmpty())
                    sb.Append(Uri.EscapeDataString(pair.Value));

                if (i < count - 1)
                    sb.Append(QueryStringSeparator);
            });

            return Uri.EscapeUriString(sb.ToString());
        }

    }
}
