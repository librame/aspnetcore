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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 窗格存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TPaneClaim">指定的窗格声明类型。</typeparam>
    public interface IPaneStore<out TAccessor, TPane, TPaneClaim> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TPane : class
        where TPaneClaim : class
    {

        #region Pane

        /// <summary>
        /// 异步包含指定窗格。
        /// </summary>
        /// <param name="categoryId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        Task<bool> ContainPaneAsync(object categoryId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取窗格。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        Task<TPane> GetPaneAsync(object categoryId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找窗格。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        Task<TPane> FindPaneAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有窗格集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TPane}"/> 的异步操作。</returns>
        Task<List<TPane>> GetAllPanesAsync(Func<IQueryable<TPane>, IQueryable<TPane>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页窗格集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPane}"/> 的异步操作。</returns>
        Task<IPageable<TPane>> GetPagingPanesAsync(int index, int size,
            Func<IQueryable<TPane>, IQueryable<TPane>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TPane[] panes);

        /// <summary>
        /// 尝试更新窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TPane[] panes);

        /// <summary>
        /// 尝试删除窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TPane[] panes);

        #endregion


        #region PaneClaim

        /// <summary>
        /// 异步包含指定窗格。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        Task<bool> ContainPaneClaimAsync(object paneId, object claimId, string assocId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取窗格声明。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneClaim"/> 的异步操作。</returns>
        Task<TPaneClaim> GetPaneClaimAsync(object paneId, object claimId, string assocId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找窗格声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneClaim"/> 的异步操作。</returns>
        Task<TPaneClaim> FindPaneClaimAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有窗格声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TPaneClaim}"/> 的异步操作。</returns>
        Task<List<TPaneClaim>> GetAllPaneClaimsAsync(Func<IQueryable<TPaneClaim>, IQueryable<TPaneClaim>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页窗格声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPaneClaim}"/> 的异步操作。</returns>
        Task<IPageable<TPaneClaim>> GetPagingPaneClaimsAsync(int index, int size,
            Func<IQueryable<TPaneClaim>, IQueryable<TPaneClaim>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建窗格声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="paneClaims">给定的 <typeparamref name="TPaneClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TPaneClaim[] paneClaims);

        /// <summary>
        /// 尝试更新窗格声明集合。
        /// </summary>
        /// <param name="paneClaims">给定的 <typeparamref name="TPaneClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TPaneClaim[] paneClaims);

        /// <summary>
        /// 尝试删除窗格声明集合。
        /// </summary>
        /// <param name="paneClaims">给定的 <typeparamref name="TPaneClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TPaneClaim[] paneClaims);

        #endregion

    }
}
