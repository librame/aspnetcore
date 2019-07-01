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
    /// 分类存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    public interface ICategoryStore<TAccessor, TCategory, TCategoryId> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TCategory : class
        where TCategoryId : IEquatable<TCategoryId>
    {
        /// <summary>
        /// 异步通过标识查找分类。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        Task<TCategory> FindCategoryByIdAsync(TCategoryId categoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找分类。
        /// </summary>
        /// <param name="categoryName">给定的分类名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        Task<TCategory> FindCategoryByNameAsync(string categoryName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分类列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TCategory}"/> 的异步操作。</returns>
        Task<IPageable<TCategory>> GetPagingCategoriesAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建分类。
        /// </summary>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TCategory category, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新分类。
        /// </summary>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TCategory category, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除分类。
        /// </summary>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TCategory category, CancellationToken cancellationToken = default);
    }
}
