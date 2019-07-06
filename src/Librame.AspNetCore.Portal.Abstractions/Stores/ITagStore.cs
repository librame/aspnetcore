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
    /// 标签存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TTagClaim">指定的标签声明类型。</typeparam>
    public interface ITagStore<TAccessor, TTag, TTagClaim> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TTag : class
        where TTagClaim : class
    {

        #region Tag

        /// <summary>
        /// 验证标签唯一性。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        Expression<Func<TTag, bool>> VerifyTagUniqueness(string name);

        /// <summary>
        /// 异步查找标签。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        Task<TTag> FindTagAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取标签。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        Task<TTag> GetTagAsync(string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TTag}"/> 的异步操作。</returns>
        Task<List<TTag>> GetAllTagsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页标签集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTag}"/> 的异步操作。</returns>
        Task<IPageable<TTag>> GetPagingTagsAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TTag[] tags);

        /// <summary>
        /// 尝试更新标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TTag[] tags);

        /// <summary>
        /// 尝试删除标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TTag[] tags);

        #endregion


        #region TagClaim

        /// <summary>
        /// 验证标签声明唯一性。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <returns>返回查询表达式。</returns>
        Expression<Func<TTagClaim, bool>> VerifyTagClaimUniqueness(object tagId, object claimId);

        /// <summary>
        /// 异步查找标签声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        Task<TTagClaim> FindTagClaimAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取标签声明。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        Task<TTagClaim> GetTagClaimAsync(object tagId, object claimId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有标签声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TTagClaim}"/> 的异步操作。</returns>
        Task<List<TTagClaim>> GetAllTagClaimsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页标签声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTagClaim}"/> 的异步操作。</returns>
        Task<IPageable<TTagClaim>> GetPagingTagClaimsAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建标签声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="tagClaims">给定的 <typeparamref name="TTagClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TTagClaim[] tagClaims);

        /// <summary>
        /// 尝试更新标签声明集合。
        /// </summary>
        /// <param name="tagClaims">给定的 <typeparamref name="TTagClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TTagClaim[] tagClaims);

        /// <summary>
        /// 尝试删除标签声明集合。
        /// </summary>
        /// <param name="tagClaims">给定的 <typeparamref name="TTagClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TTagClaim[] tagClaims);

        #endregion

    }
}
