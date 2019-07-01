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
    /// 标签存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TTagClaim">指定的标签声明类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    /// <typeparam name="TTagClaimId">指定的标签声明标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    public interface ITagStore<TAccessor, TTag, TTagClaim, TTagId, TTagClaimId, TClaimId> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TTag : class
        where TTagClaim : class
        where TTagId : IEquatable<TTagId>
        where TClaimId : IEquatable<TClaimId>
    {

        #region Tag

        /// <summary>
        /// 异步通过标识查找标签。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        Task<TTag> FindTagByIdAsync(TTagId tagId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找标签。
        /// </summary>
        /// <param name="tagName">给定的标签名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        Task<TTag> FindTagByNameAsync(string tagName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取标签列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTag}"/> 的异步操作。</returns>
        Task<IPageable<TTag>> GetPagingTagsAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建标签。
        /// </summary>
        /// <param name="tag">给定的 <typeparamref name="TTag"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TTag tag, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新标签。
        /// </summary>
        /// <param name="tag">给定的 <typeparamref name="TTag"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TTag tag, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除标签。
        /// </summary>
        /// <param name="tag">给定的 <typeparamref name="TTag"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TTag tag, CancellationToken cancellationToken = default);

        #endregion


        #region TagClaim

        /// <summary>
        /// 异步通过标识查找标签声明。
        /// </summary>
        /// <param name="tagClaimId">给定的标签声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        Task<TTagClaim> FindTagClaimByIdAsync(TTagClaimId tagClaimId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找标签声明。
        /// </summary>
        /// <param name="tagClaimName">给定的标签声明名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        Task<TTagClaim> FindTagClaimByNameAsync(string tagClaimName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取标签声明列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="tagId">给定的标签标识（可选）。</param>
        /// <param name="claimId">给定的声明标识（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTagClaim}"/> 的异步操作。</returns>
        Task<IPageable<TTagClaim>> GetPagingTagClaimsAsync(int index, int size, TTagId tagId = default,
            TClaimId claimId = default, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建标签声明。
        /// </summary>
        /// <param name="tagClaim">给定的 <typeparamref name="TTagClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TTagClaim tagClaim, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新标签声明。
        /// </summary>
        /// <param name="tagClaim">给定的 <typeparamref name="TTagClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TTagClaim tagClaim, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除标签声明。
        /// </summary>
        /// <param name="tagClaim">给定的 <typeparamref name="TTagClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TTagClaim tagClaim, CancellationToken cancellationToken = default);

        #endregion

    }
}
