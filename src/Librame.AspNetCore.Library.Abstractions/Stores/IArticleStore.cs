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

namespace Librame.AspNetCore.Library
{
    using Extensions.Data;

    /// <summary>
    /// 文章存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectArticle">指定的专题文章类型。</typeparam>
    /// <typeparam name="TArticle">指定的文章类型。</typeparam>
    /// <typeparam name="TArticleBody">指定的文章主体类型。</typeparam>
    public interface IArticleStore<TAccessor, TSubject, TSubjectArticle, TArticle, TArticleBody> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TSubject : class
        where TSubjectArticle : class
        where TArticle : class
        where TArticleBody : class
    {

        #region Subject

        /// <summary>
        /// 异步通过标识查找专题。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        Task<TSubject> FindSubjectByIdAsync(object subjectId, CancellationToken cancellationToken = default);

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


        #region SubjectArticle

        /// <summary>
        /// Returns a flag indicating whether the specified <paramref name="subjectId"/> is a member of the given named role.
        /// </summary>
        /// <param name="subjectId">The user whose role membership should be checked.</param>
        /// <param name="articleId">The name of the role to be checked.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>
        /// The <see cref="Task"/> that represents the asynchronous operation, containing a flag indicating whether the specified <paramref name="subjectId"/> is
        /// a member of the named role.
        /// </returns>
        Task<bool> IsInSubjectAsync(object subjectId, object articleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets a list of role names the specified <paramref name="subject"/> belongs to.
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<IList<string>> GetRolesAsync(TSubject subject, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取指定专题标识的文章列表。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IList{TArticle}"/> 的异步操作。</returns>
        Task<IList<TArticle>> GetArticlesInSubjectAsync(object subjectId, CancellationToken cancellationToken = default);


        /// <summary>
        /// 添加指定的专题与文章标识。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="articleId">给定的文章标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> AddToArticleAsync(object subjectId, object articleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 移除指定的专题与文章标识。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="articleId">给定的文章标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> RemoveFromArticleAsync(object subjectId, object articleId, CancellationToken cancellationToken = default);

        #endregion

    }
}
