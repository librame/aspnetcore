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
    /// 窗格存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TPaneClaim">指定的窗格声明类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TPaneClaimId">指定的窗格声明标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    public interface IPaneStore<TAccessor, TPane, TPaneClaim, TPaneId, TPaneClaimId, TClaimId, TCategoryId> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TPane : class
        where TPaneClaim : class
        where TPaneId : IEquatable<TPaneId>
        where TClaimId : IEquatable<TClaimId>
        where TCategoryId : IEquatable<TCategoryId>
    {

        #region Pane

        /// <summary>
        /// 异步通过标识查找窗格。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        Task<TPane> FindPaneByIdAsync(TPaneId paneId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找窗格。
        /// </summary>
        /// <param name="paneName">给定的窗格名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        Task<TPane> FindPaneByNameAsync(string paneName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取窗格列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="categoryId">给定的分类标识（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPane}"/> 的异步操作。</returns>
        Task<IPageable<TPane>> GetPagingPanesAsync(int index, int size, TCategoryId categoryId = default,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建窗格。
        /// </summary>
        /// <param name="pane">给定的 <typeparamref name="TPane"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TPane pane, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新窗格。
        /// </summary>
        /// <param name="pane">给定的 <typeparamref name="TPane"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TPane pane, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除窗格。
        /// </summary>
        /// <param name="pane">给定的 <typeparamref name="TPane"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TPane pane, CancellationToken cancellationToken = default);

        #endregion


        #region PaneClaim

        /// <summary>
        /// 异步通过标识查找窗格声明。
        /// </summary>
        /// <param name="paneClaimId">给定的窗格声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneClaim"/> 的异步操作。</returns>
        Task<TPaneClaim> FindPaneClaimByIdAsync(TPaneClaimId paneClaimId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找窗格声明。
        /// </summary>
        /// <param name="paneClaimName">给定的窗格声明名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneClaim"/> 的异步操作。</returns>
        Task<TPaneClaim> FindPaneClaimByNameAsync(string paneClaimName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取窗格声明列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="paneId">给定的窗格标识（可选）。</param>
        /// <param name="claimId">给定的声明标识（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPaneClaim}"/> 的异步操作。</returns>
        Task<IPageable<TPaneClaim>> GetPagingPaneClaimsAsync(int index, int size, TPaneId paneId = default,
            TClaimId claimId = default, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建窗格声明。
        /// </summary>
        /// <param name="paneClaim">给定的 <typeparamref name="TPaneClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TPaneClaim paneClaim, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新窗格声明。
        /// </summary>
        /// <param name="paneClaim">给定的 <typeparamref name="TPaneClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TPaneClaim paneClaim, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除窗格声明。
        /// </summary>
        /// <param name="paneClaim">给定的 <typeparamref name="TPaneClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TPaneClaim paneClaim, CancellationToken cancellationToken = default);

        #endregion

    }
}
