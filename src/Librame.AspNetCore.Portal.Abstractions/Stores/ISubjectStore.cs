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
    /// 专题存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
    /// <typeparam name="TSubjectClaim">指定的专题声明类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    /// <typeparam name="TSubjectBodyId">指定的专题主体标识类型。</typeparam>
    /// <typeparam name="TSubjectClaimId">指定的专题声明标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    public interface ISubjectStore<TAccessor, TSubject, TSubjectBody, TSubjectClaim, TSubjectId, TSubjectBodyId, TSubjectClaimId, TClaimId> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TSubject : class
        where TSubjectBody : class
        where TSubjectClaim : class
        where TSubjectId : IEquatable<TSubjectId>
        where TClaimId : IEquatable<TClaimId>
    {

        #region Subject

        /// <summary>
        /// 异步通过标识查找专题。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        Task<TSubject> FindSubjectByIdAsync(TSubjectId subjectId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找专题。
        /// </summary>
        /// <param name="subjectName">给定的专题名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        Task<TSubject> FindSubjectByNameAsync(string subjectName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取专题列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubject}"/> 的异步操作。</returns>
        Task<IPageable<TSubject>> GetPagingSubjectsAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建专题。
        /// </summary>
        /// <param name="subject">给定的 <typeparamref name="TSubject"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TSubject subject, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新专题。
        /// </summary>
        /// <param name="subject">给定的 <typeparamref name="TSubject"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TSubject subject, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除专题。
        /// </summary>
        /// <param name="subject">给定的 <typeparamref name="TSubject"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TSubject subject, CancellationToken cancellationToken = default);

        #endregion


        #region SubjectBody

        /// <summary>
        /// 异步通过标识查找专题主体。
        /// </summary>
        /// <param name="subjectBodyId">给定的专题主体标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        Task<TSubjectBody> FindSubjectBodyByIdAsync(TSubjectBodyId subjectBodyId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找专题主体。
        /// </summary>
        /// <param name="subjectBodyName">给定的专题主体名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        Task<TSubjectBody> FindSubjectBodyByNameAsync(string subjectBodyName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取专题主体列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="subjectId">给定的专题标识（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubjectBody}"/> 的异步操作。</returns>
        Task<IPageable<TSubjectBody>> GetPagingSubjectBodysAsync(int index, int size, TSubjectId subjectId = default,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建专题主体。
        /// </summary>
        /// <param name="subjectBody">给定的 <typeparamref name="TSubjectBody"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TSubjectBody subjectBody, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新专题主体。
        /// </summary>
        /// <param name="subjectBody">给定的 <typeparamref name="TSubjectBody"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TSubjectBody subjectBody, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除专题主体。
        /// </summary>
        /// <param name="subjectBody">给定的 <typeparamref name="TSubjectBody"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TSubjectBody subjectBody, CancellationToken cancellationToken = default);

        #endregion


        #region SubjectClaim

        /// <summary>
        /// 异步通过标识查找专题声明。
        /// </summary>
        /// <param name="subjectClaimId">给定的专题声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        Task<TSubjectClaim> FindSubjectClaimByIdAsync(TSubjectClaimId subjectClaimId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找专题声明。
        /// </summary>
        /// <param name="subjectClaimName">给定的专题声明名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        Task<TSubjectClaim> FindSubjectClaimByNameAsync(string subjectClaimName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取专题声明列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="subjectId">给定的专题标识（可选）。</param>
        /// <param name="claimId">给定的声明标识（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubjectClaim}"/> 的异步操作。</returns>
        Task<IPageable<TSubjectClaim>> GetPagingSubjectClaimsAsync(int index, int size, TSubjectId subjectId = default,
            TClaimId claimId = default, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建专题声明。
        /// </summary>
        /// <param name="subjectClaim">给定的 <typeparamref name="TSubjectClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TSubjectClaim subjectClaim, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新专题声明。
        /// </summary>
        /// <param name="subjectClaim">给定的 <typeparamref name="TSubjectClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TSubjectClaim subjectClaim, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除专题声明。
        /// </summary>
        /// <param name="subjectClaim">给定的 <typeparamref name="TSubjectClaim"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TSubjectClaim subjectClaim, CancellationToken cancellationToken = default);

        #endregion

    }
}
