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
using System.Linq.Expressions;
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
    public interface ISourceStore<out TAccessor, TSource> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TSource : class
    {
        /// <summary>
        /// 验证来源唯一性。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        Expression<Func<TSource, bool>> VerifySourceUniqueness(object categoryId, string name);

        /// <summary>
        /// 异步获取来源。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        Task<TSource> GetSourceAsync(object categoryId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找来源。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        Task<TSource> FindSourceAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取来源集合。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSource}"/> 的异步操作。</returns>
        Task<List<TSource>> GetSourcesAsync(object categoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSource}"/> 的异步操作。</returns>
        Task<List<TSource>> GetAllSourcesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页来源集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSource}"/> 的异步操作。</returns>
        Task<IPageable<TSource>> GetPagingSourcesAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSource[] sources);

        /// <summary>
        /// 尝试更新来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TSource[] sources);

        /// <summary>
        /// 尝试删除来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TSource[] sources);
    }
}
