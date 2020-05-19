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
    }
}
