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
    /// 专题存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
    /// <typeparam name="TSubjectClaim">指定的专题声明类型。</typeparam>
    public interface ISubjectStore<TAccessor, TSubject, TSubjectBody, TSubjectClaim> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TSubject : class
        where TSubjectBody : class
        where TSubjectClaim : class
    {

        #region Subject

        /// <summary>
        /// 异步查找专题。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        Task<TSubject> FindSubjectAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取专题。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="host">给定的主机。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        Task<TSubject> GetSubjectAsync(string name, string host, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有专题集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubject}"/> 的异步操作。</returns>
        Task<List<TSubject>> GetAllSubjectsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页专题集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubject}"/> 的异步操作。</returns>
        Task<IPageable<TSubject>> GetPagingSubjectsAsync(int index, int size, CancellationToken cancellationToken = default);


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
        /// 异步查找专题主体。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        Task<TSubjectBody> FindSubjectBodyAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取专题主体。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="host">给定的主机。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        Task<TSubjectBody> GetSubjectBodyAsync(string name, string host, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有专题主体集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectBody}"/> 的异步操作。</returns>
        Task<List<TSubjectBody>> GetAllSubjectBodysAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页专题主体集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubjectBody}"/> 的异步操作。</returns>
        Task<IPageable<TSubjectBody>> GetPagingSubjectBodysAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建专题主体集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="subjectBodys">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSubjectBody[] subjectBodys);

        /// <summary>
        /// 尝试更新专题主体集合。
        /// </summary>
        /// <param name="subjectBodys">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TSubjectBody[] subjectBodys);

        /// <summary>
        /// 尝试删除专题主体集合。
        /// </summary>
        /// <param name="subjectBodys">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TSubjectBody[] subjectBodys);

        #endregion


        #region SubjectClaim

        /// <summary>
        /// 异步查找专题声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        Task<TSubjectClaim> FindSubjectClaimAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取专题声明。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="host">给定的主机。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        Task<TSubjectClaim> GetSubjectClaimAsync(string name, string host, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取所有专题声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectClaim}"/> 的异步操作。</returns>
        Task<List<TSubjectClaim>> GetAllSubjectClaimsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页专题声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubjectClaim}"/> 的异步操作。</returns>
        Task<IPageable<TSubjectClaim>> GetPagingSubjectClaimsAsync(int index, int size, CancellationToken cancellationToken = default);


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
