#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

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
    public interface ICategoryStore<TAccessor, TCategory> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TCategory : class
    {
        /// <summary>
        /// 异步查找分类。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        Task<TCategory> FindCategoryAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取分类。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="host">给定的主机。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        Task<TCategory> GetCategoryAsync(string name, string host, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有分类集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TCategory}"/> 的异步操作。</returns>
        Task<List<TCategory>> GetAllCategorysAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页分类集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TCategory}"/> 的异步操作。</returns>
        Task<IPageable<TCategory>> GetPagingCategorysAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建分类集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TCategory[] categories);

        /// <summary>
        /// 尝试更新分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TCategory[] categories);

        /// <summary>
        /// 尝试删除分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TCategory[] categories);
    }
}
