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
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 门户存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TEditorTitleId">指定的编者头衔标识类型。</typeparam>
    public class PortalStore<TAccessor, TCategory, TCategoryId, TEditor, TEditorId, TUserId, TEditorTitle, TEditorTitleId>
        : AbstractBaseStore<TAccessor>, IPortalStore<TAccessor, TCategory, TEditor, TEditorTitle>
        where TAccessor : IAccessor
        where TCategory : PortalCategory<TCategoryId>
        where TEditor : PortalEditor<TEditorId, TUserId>
        where TEditorTitle : PortalEditor<TEditorTitleId, TUserId>
        where TCategoryId : IEquatable<TCategoryId>
        where TEditorId : IEquatable<TEditorId>
        where TUserId : IEquatable<TUserId>
        where TEditorTitleId : IEquatable<TEditorTitleId>
    {
        /// <summary>
        /// 构造一个抽象门户存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStore(IAccessor accessor)
            : base(accessor)
        {
        }


        #region Category

        /// <summary>
        /// 异步通过标识查找分类。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        public virtual Task<TCategory> FindCategoryByIdAsync(object categoryId, CancellationToken cancellationToken = default)
        {
            var realId = categoryId.IsValue<object, TCategoryId>(nameof(categoryId));

            return Accessor.QueryResultAsync<TCategory>(query => query.FirstOrDefault(p => p.Id.Equals(realId)), cancellationToken);
        }

        /// <summary>
        /// 异步通过名称查找分类。
        /// </summary>
        /// <param name="categoryName">给定的分类名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        public virtual Task<TCategory> FindCategoryByNameAsync(string categoryName, CancellationToken cancellationToken = default)
            => Accessor.QueryResultAsync<TCategory>(query => query.FirstOrDefault(p => p.Name == categoryName), cancellationToken);

        /// <summary>
        /// 异步获取分类列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TCategory}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TCategory>> GetPagingCategoriesAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.QueryPagingAsync<TCategory>(query => query.OrderByDescending(ks => ks.Id),
                descr => descr.ComputeByIndex(index, size), cancellationToken);
        }

        /// <summary>
        /// 异步创建分类。
        /// </summary>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> CreateAsync(TCategory category, CancellationToken cancellationToken = default)
            => Accessor.CreateAsync(cancellationToken, category);

        /// <summary>
        /// 异步更新分类。
        /// </summary>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> UpdateAsync(TCategory category, CancellationToken cancellationToken = default)
            => Accessor.UpdateAsync(cancellationToken, category);

        /// <summary>
        /// 异步删除分类。
        /// </summary>
        /// <param name="category">给定的 <typeparamref name="TCategory"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> DeleteAsync(TCategory category, CancellationToken cancellationToken = default)
            => Accessor.DeleteAsync(cancellationToken, category);

        #endregion


        #region Editor

        /// <summary>
        /// 异步通过标识查找编者。
        /// </summary>
        /// <param name="editorId">给定的编者标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        public virtual Task<TEditor> FindEditorByIdAsync(object editorId, CancellationToken cancellationToken = default)
        {
            var realId = editorId.IsValue<object, TEditorId>(nameof(editorId));

            return Accessor.QueryResultAsync<TEditor>(query => query.FirstOrDefault(p => p.Id.Equals(realId)), cancellationToken);
        }

        /// <summary>
        /// 异步通过名称查找编者。
        /// </summary>
        /// <param name="editorName">给定的编者名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        public virtual Task<TEditor> FindEditorByNameAsync(string editorName, CancellationToken cancellationToken = default)
            => Accessor.QueryResultAsync<TEditor>(query => query.FirstOrDefault(p => p.Name == editorName), cancellationToken);

        /// <summary>
        /// 异步获取编者列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditor}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TEditor>> GetPagingEditorsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.QueryPagingAsync<TEditor>(query => query.OrderByDescending(ks => ks.Id),
                descr => descr.ComputeByIndex(index, size), cancellationToken);
        }

        /// <summary>
        /// 异步创建编者。
        /// </summary>
        /// <param name="editor">给定的 <typeparamref name="TEditor"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> CreateAsync(TEditor editor, CancellationToken cancellationToken = default)
            => Accessor.CreateAsync(cancellationToken, editor);

        /// <summary>
        /// 异步更新编者。
        /// </summary>
        /// <param name="editor">给定的 <typeparamref name="TEditor"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> UpdateAsync(TEditor editor, CancellationToken cancellationToken = default)
            => Accessor.UpdateAsync(cancellationToken, editor);

        /// <summary>
        /// 异步删除编者。
        /// </summary>
        /// <param name="editor">给定的 <typeparamref name="TEditor"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> DeleteAsync(TEditor editor, CancellationToken cancellationToken = default)
            => Accessor.DeleteAsync(cancellationToken, editor);

        #endregion


        #region EditorTitle

        /// <summary>
        /// 异步通过标识查找编者头衔。
        /// </summary>
        /// <param name="editorTitleId">给定的编者头衔标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        public virtual Task<TEditorTitle> FindEditorTitleByIdAsync(object editorTitleId, CancellationToken cancellationToken = default)
        {
            var realId = editorTitleId.IsValue<object, TEditorTitleId>(nameof(editorTitleId));

            return Accessor.QueryResultAsync<TEditorTitle>(query => query.FirstOrDefault(p => p.Id.Equals(realId)), cancellationToken);
        }

        /// <summary>
        /// 异步通过名称查找编者头衔。
        /// </summary>
        /// <param name="editorTitleName">给定的编者头衔名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        public virtual Task<TEditorTitle> FindEditorTitleByNameAsync(string editorTitleName, CancellationToken cancellationToken = default)
            => Accessor.QueryResultAsync<TEditorTitle>(query => query.FirstOrDefault(p => p.Name == editorTitleName), cancellationToken);

        /// <summary>
        /// 异步获取编者头衔列表。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditorTitle}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TEditorTitle>> GetPagingEditorTitlesAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.QueryPagingAsync<TEditorTitle>(query => query.OrderByDescending(ks => ks.Id),
                descr => descr.ComputeByIndex(index, size), cancellationToken);
        }


        /// <summary>
        /// 异步创建编者头衔。
        /// </summary>
        /// <param name="editorTitle">给定的 <typeparamref name="TEditorTitle"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> CreateAsync(TEditorTitle editorTitle, CancellationToken cancellationToken = default)
            => Accessor.CreateAsync(cancellationToken, editorTitle);

        /// <summary>
        /// 异步更新编者头衔。
        /// </summary>
        /// <param name="editorTitle">给定的 <typeparamref name="TEditorTitle"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> UpdateAsync(TEditorTitle editorTitle, CancellationToken cancellationToken = default)
            => Accessor.UpdateAsync(cancellationToken, editorTitle);

        /// <summary>
        /// 异步删除编者头衔。
        /// </summary>
        /// <param name="editorTitle">给定的 <typeparamref name="TEditorTitle"/>。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> DeleteAsync(TEditorTitle editorTitle, CancellationToken cancellationToken = default)
            => Accessor.LogicDeleteAsync(cancellationToken, editorTitle);

        #endregion

    }
}
