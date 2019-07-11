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
    /// 编者存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    public interface IEditorStore<out TAccessor, TEditor, TEditorTitle> : IStore<TAccessor>
        where TAccessor : IAccessor
        where TEditor : class
        where TEditorTitle : class
    {

        #region Editor

        /// <summary>
        /// 异步包含指定编者。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        Task<bool> ContainEditorAsync(object userId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取指定编者。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        Task<TEditor> GetEditorAsync(object userId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找指定编者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        Task<TEditor> FindEditorAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有编者集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TEditor}"/> 的异步操作。</returns>
        Task<List<TEditor>> GetAllEditorsAsync(Func<IQueryable<TEditor>, IQueryable<TEditor>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页编者集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditor}"/> 的异步操作。</returns>
        Task<IPageable<TEditor>> GetPagingEditorsAsync(int index, int size,
            Func<IQueryable<TEditor>, IQueryable<TEditor>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建编者集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TEditor[] editors);

        /// <summary>
        /// 尝试更新编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TEditor[] editors);

        /// <summary>
        /// 尝试删除编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TEditor[] editors);

        #endregion


        #region EditorTitle

        /// <summary>
        /// 异步包含指定编者头衔。
        /// </summary>
        /// <param name="editorId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        Task<bool> ContainEditorTitleAsync(object editorId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取指定编者头衔。
        /// </summary>
        /// <param name="editorId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        Task<TEditorTitle> GetEditorTitleAsync(object editorId, string name, CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步查找指定编者头衔。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        Task<TEditorTitle> FindEditorTitleAsync(CancellationToken cancellationToken, params object[] keyValues);

        /// <summary>
        /// 异步获取所有编者头衔集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TEditorTitle}"/> 的异步操作。</returns>
        Task<List<TEditorTitle>> GetAllEditorTitlesAsync(Func<IQueryable<TEditorTitle>, IQueryable<TEditorTitle>> queryFactory = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步获取分页编者头衔集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditorTitle}"/> 的异步操作。</returns>
        Task<IPageable<TEditorTitle>> GetPagingEditorTitlesAsync(int index, int size,
            Func<IQueryable<TEditorTitle>, IQueryable<TEditorTitle>> queryFactory = null,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// 尝试异步创建编者头衔集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="editorTitles">给定的 <typeparamref name="TEditorTitle"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TEditorTitle[] editorTitles);

        /// <summary>
        /// 尝试更新编者头衔集合。
        /// </summary>
        /// <param name="editorTitles">给定的 <typeparamref name="TEditorTitle"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryUpdate(params TEditorTitle[] editorTitles);

        /// <summary>
        /// 尝试删除编者头衔集合。
        /// </summary>
        /// <param name="editorTitles">给定的 <typeparamref name="TEditorTitle"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        EntityResult TryDelete(params TEditorTitle[] editorTitles);

        #endregion

    }
}
