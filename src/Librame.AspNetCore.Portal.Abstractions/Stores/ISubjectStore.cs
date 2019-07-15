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
    /// 专题存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
    /// <typeparam name="TSubjectClaim">指定的专题声明类型。</typeparam>
    public interface ISubjectStore<out TAccessor, TSubject, TSubjectBody, TSubjectClaim> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TSubject : class
        where TSubjectBody : class
        where TSubjectClaim : class
    {

        #region Subject

        /// <summary>
        /// 异步包含指定专题。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="title">给定的标题。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        Task<bool> ContainSubjectAsync(object categoryId, string title, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取专题。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="title">给定的标题。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        Task<TSubject> GetSubjectAsync(object categoryId, string title, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找专题。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        Task<TSubject> FindSubjectAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有专题集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubject}"/> 的异步操作。</returns>
        Task<List<TSubject>> GetAllSubjectsAsync(Func<IQueryable<TSubject>, IQueryable<TSubject>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页专题集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubject}"/> 的异步操作。</returns>
        Task<IPageable<TSubject>> GetPagingSubjectsAsync(int index, int size,
            Func<IQueryable<TSubject>, IQueryable<TSubject>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建专题集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="subjects">给定的 <typeparamref name="TSubject"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSubject[] subjects);

        /// <summary>
        /// 尝试更新专题集合。
        /// </summary>
        /// <param name="subjects">给定的 <typeparamref name="TSubject"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TSubject[] subjects);

        /// <summary>
        /// 尝试删除专题集合。
        /// </summary>
        /// <param name="subjects">给定的 <typeparamref name="TSubject"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TSubject[] subjects);

        #endregion


        #region SubjectBody

        /// <summary>
        /// 异步包含指定专题主体。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="textHash">给定的文本散列。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        Task<bool> ContainSubjectBodyAsync(object subjectId, string textHash, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取指定专题主体。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="textHash">给定的文本散列。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        Task<TSubjectBody> GetSubjectBodyAsync(object subjectId, string textHash, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找指定专题主体。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        Task<TSubjectBody> FindSubjectBodyAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取专题主体集合。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectBody}"/> 的异步操作。</returns>
        Task<List<TSubjectBody>> GetSubjectBodiesAsync(object subjectId, CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建专题主体集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="subjectBodies">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSubjectBody[] subjectBodies);

        /// <summary>
        /// 尝试更新专题主体集合。
        /// </summary>
        /// <param name="subjectBodies">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TSubjectBody[] subjectBodies);

        /// <summary>
        /// 尝试删除专题主体集合。
        /// </summary>
        /// <param name="subjectBodies">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TSubjectBody[] subjectBodies);

        #endregion


        #region SubjectClaim

        /// <summary>
        /// 异步包含指定专题声明。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        Task<bool> ContainTenantAsync(object subjectId, object claimId, string assocId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取指定专题声明。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        Task<TSubjectClaim> GetSubjectClaimAsync(object subjectId, object claimId, string assocId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找指定专题声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        Task<TSubjectClaim> FindSubjectClaimAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有专题声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectClaim}"/> 的异步操作。</returns>
        Task<List<TSubjectClaim>> GetAllSubjectClaimsAsync(Func<IQueryable<TSubjectClaim>, IQueryable<TSubjectClaim>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页专题声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubjectClaim}"/> 的异步操作。</returns>
        Task<IPageable<TSubjectClaim>> GetPagingSubjectClaimsAsync(int index, int size,
            Func<IQueryable<TSubjectClaim>, IQueryable<TSubjectClaim>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建专题声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="subjectClaims">给定的 <typeparamref name="TSubjectClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSubjectClaim[] subjectClaims);

        /// <summary>
        /// 尝试更新专题声明集合。
        /// </summary>
        /// <param name="subjectClaims">给定的 <typeparamref name="TSubjectClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TSubjectClaim[] subjectClaims);

        /// <summary>
        /// 尝试删除专题声明集合。
        /// </summary>
        /// <param name="subjectClaims">给定的 <typeparamref name="TSubjectClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TSubjectClaim[] subjectClaims);

        #endregion

    }
}
