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
    using Extensions.Core;

    /// <summary>
    /// URI 定位符接口。
    /// </summary>
    public interface IUriLocator : ILocator<Uri>, IEquatable<IUriLocator>
    {
        /// <summary>
        /// 协议。
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// 主机。
        /// </summary>
        HostString Host { get; }

        /// <summary>
        /// 主机字符串。
        /// </summary>
        string HostString { get; }

        /// <summary>
        /// 以 / 开始的路径。
        /// </summary>
        PathString Path { get; }

        /// <summary>
        /// 以 / 开始的路径字符串。
        /// </summary>
        string PathString { get; }

        /// <summary>
        /// 以 ? 开始的查询。
        /// </summary>
        QueryString Query { get; }

        /// <summary>
        /// 以 ? 开始的查询字符串。
        /// </summary>
        string QueryString { get; }

        /// <summary>
        /// 以 # 开始的锚点。
        /// </summary>
        string Anchor { get; }


        /// <summary>
        /// 改变协议。
        /// </summary>
        /// <param name="newScheme">给定的新协议。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator ChangeScheme(string newScheme);

        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="newHost">给定的新主机。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator ChangeHost(HostString newHost);

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="newPath">给定的新路径。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator ChangePath(PathString newPath);

        /// <summary>
        /// 改变查询。
        /// </summary>
        /// <param name="newQuery">给定的新查询。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator ChangeQuery(QueryString newQuery);

        /// <summary>
        /// 改变查询参数集合。
        /// </summary>
        /// <param name="queriesAction">给定的改变查询参数集合动作（内部支持对参数值的特殊字符进行转码处理）。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator ChangeQueries(Action<ConcurrentDictionary<string, string>> queriesAction);

        /// <summary>
        /// 改变锚点。
        /// </summary>
        /// <param name="newAnchor">给定以 # 开始的新锚点。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator ChangeAnchor(string newAnchor);


        /// <summary>
        /// 使用指定的新协议新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newScheme">给定的新协议。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator NewScheme(string newScheme);

        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newHost">给定的新主机。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator NewHost(HostString newHost);

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newPath">给定的新路径。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator NewPath(PathString newPath);

        /// <summary>
        /// 使用指定的新查询新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newQuery">给定的新查询。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator NewQuery(QueryString newQuery);

        /// <summary>
        /// 使用指定的查询参数数组新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="queriesAction">给定的改变查询参数集合动作（内部支持对参数值的特殊字符进行转码处理）。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator NewQueries(Action<ConcurrentDictionary<string, string>> queriesAction);

        /// <summary>
        /// 使用指定的新锚点新建一个 <see cref="IUriLocator"/> 实例。
        /// </summary>
        /// <param name="newAnchor">给定以 # 开始的新锚点。</param>
        /// <returns>返回 <see cref="IUriLocator"/>。</returns>
        IUriLocator NewAnchor(string newAnchor);
    }
}
