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
    /// 编者存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TEditorTitleId">指定的编者头衔标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public interface IEditorStore<TAccessor, TEditor, TEditorTitle, TEditorId, TEditorTitleId, TUserId> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TEditor : class
        where TEditorTitle : class
        where TEditorId : IEquatable<TEditorId>
        where TEditorTitleId : IEquatable<TEditorTitleId>
        where TUserId : IEquatable<TUserId>
    {

        #region Editor

        /// <summary>
        /// 异步通过标识查找编者（单个用户对应单个编者）。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        Task<TEditor> FindEditorByIdAsync(TUserId userId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过标识查找编者。
        /// </summary>
        /// <param name="editorId">给定的编者标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        Task<TEditor> FindEditorByIdAsync(TEditorId editorId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找编者。
        /// </summary>
        /// <param name="editorName">给定的编者名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        Task<TEditor> FindEditorByNameAsync(string editorName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取编者列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditor}"/> 的异步操作。</returns>
        Task<IPageable<TEditor>> GetPagingEditorsAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建编者。
        /// </summary>
        /// <param name="editor">给定的 <typeparamref name="TEditor"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TEditor editor, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新编者。
        /// </summary>
        /// <param name="editor">给定的 <typeparamref name="TEditor"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TEditor editor, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除编者。
        /// </summary>
        /// <param name="editor">给定的 <typeparamref name="TEditor"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TEditor editor, CancellationToken cancellationToken = default);

        #endregion


        #region EditorTitle

        /// <summary>
        /// 异步通过标识查找编者头衔。
        /// </summary>
        /// <param name="editorTitleId">给定的编者头衔标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        Task<TEditorTitle> FindEditorTitleByIdAsync(TEditorTitleId editorTitleId, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步通过名称查找编者头衔。
        /// </summary>
        /// <param name="editorTitleName">给定的编者头衔名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        Task<TEditorTitle> FindEditorTitleByNameAsync(string editorTitleName, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取编者头衔列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditorTitle}"/> 的异步操作。</returns>
        Task<IPageable<TEditorTitle>> GetPagingEditorTitlesAsync(int index, int size, CancellationToken cancellationToken = default);


        /// <summary>
        /// 异步创建编者头衔。
        /// </summary>
        /// <param name="editorTitle">给定的 <typeparamref name="TEditorTitle"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> CreateAsync(TEditorTitle editorTitle, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步更新编者头衔。
        /// </summary>
        /// <param name="editorTitle">给定的 <typeparamref name="TEditorTitle"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> UpdateAsync(TEditorTitle editorTitle, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步删除编者头衔。
        /// </summary>
        /// <param name="editorTitle">给定的 <typeparamref name="TEditorTitle"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> DeleteAsync(TEditorTitle editorTitle, CancellationToken cancellationToken = default);

        #endregion

    }
}
