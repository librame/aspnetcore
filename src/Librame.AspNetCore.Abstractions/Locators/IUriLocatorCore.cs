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
    /// URI 定位器核心接口。
    /// </summary>
    public interface IUriLocatorCore : IUriLocator, IEquatable<IUriLocatorCore>
    {
        /// <summary>
        /// 可能包含端口号的主机。
        /// </summary>
        HostString HostString { get; }

        /// <summary>
        /// 以 / 开始的路径。
        /// </summary>
        PathString PathString { get; }

        /// <summary>
        /// 以 ? 开始的查询。
        /// </summary>
        QueryString QueryString { get; }

        /// <summary>
        /// 查询参数集合。
        /// </summary>
        ConcurrentDictionary<string, string> Queries { get; }


        /// <summary>
        /// 改变主机。
        /// </summary>
        /// <param name="newHost">给定可能包含端口号的新主机。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        IUriLocatorCore ChangeHost(HostString newHost);

        /// <summary>
        /// 改变路径。
        /// </summary>
        /// <param name="newPath">给定以 / 开始的新路径。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        IUriLocatorCore ChangePath(PathString newPath);

        /// <summary>
        /// 改变查询。
        /// </summary>
        /// <param name="newQuery">给定以 ? 开始的新查询。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        IUriLocatorCore ChangeQuery(QueryString newQuery);


        /// <summary>
        /// 使用指定的新主机新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newHost">给定可能包含端口号的新主机。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        IUriLocatorCore NewHost(HostString newHost);

        /// <summary>
        /// 使用指定的新路径新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newPath">给定以 / 开始的新路径。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        IUriLocatorCore NewPath(PathString newPath);

        /// <summary>
        /// 使用指定的新查询新建一个 <see cref="IUriLocatorCore"/> 实例。
        /// </summary>
        /// <param name="newQuery">给定以 ? 开始的新查询。</param>
        /// <returns>返回 <see cref="IUriLocatorCore"/>。</returns>
        IUriLocatorCore NewQuery(QueryString newQuery);
    }
}
