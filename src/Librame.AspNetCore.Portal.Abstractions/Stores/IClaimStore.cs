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
    /// 声明存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    public interface IClaimStore<TAccessor, TClaim> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TClaim : class
    {
        /// <summary>
        /// 验证声明唯一性。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="model">给定的模型。</param>
        /// <returns>返回查询表达式。</returns>
        Expression<Func<TClaim, bool>> VerifyClaimUniqueness(string type, string model);

        /// <summary>
        /// 异步查找声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TClaim"/> 的异步操作。</returns>
        Task<TClaim> FindClaimAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取声明。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="model">给定的模型。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TClaim"/> 的异步操作。</returns>
        Task<TClaim> GetClaimAsync(string type, string model, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TClaim}"/> 的异步操作。</returns>
        Task<List<TClaim>> GetAllClaimsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TClaim}"/> 的异步操作。</returns>
        Task<IPageable<TClaim>> GetPagingClaimsAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TClaim[] claims);

        /// <summary>
        /// 尝试更新声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TClaim[] claims);

        /// <summary>
        /// 尝试删除声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TClaim[] claims);
    }
}
