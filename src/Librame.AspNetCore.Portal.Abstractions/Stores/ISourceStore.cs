#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 来源存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    public interface ISourceStore<TAccessor, TSource, TSourceId, TCategoryId> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TSource : class
        where TSourceId : IEquatable<TSourceId>
        where TCategoryId : IEquatable<TCategoryId>
    {
        /// <summary>
        /// 异步通过标识查找来源。
        /// </summary>
        /// <param name="sourceId">给定的来源标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        Task<TSource> FindSourceByIdAsync(TSourceId sourceId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找来源。
        /// </summary>
        /// <param name="sourceName">给定的来源名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        Task<TSource> FindSourceByNameAsync(string sourceName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取来源列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="categoryId">给定的分类标识（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSource}"/> 的异步操作。</returns>
        Task<IPageable<TSource>> GetPagingSourcesAsync(int index, int size, TCategoryId categoryId = default,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建来源。
        /// </summary>
        /// <param name="source">给定的 <typeparamref name="TSource"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TSource source, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新来源。
        /// </summary>
        /// <param name="source">给定的 <typeparamref name="TSource"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TSource source, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除来源。
        /// </summary>
        /// <param name="source">给定的 <typeparamref name="TSource"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TSource source, CancellationToken cancellationToken = default);
    }
}
