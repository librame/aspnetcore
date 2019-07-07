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
using System.Linq.Expressions;
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
    public class PortalStore<TAccessor>
        : PortalStore<TAccessor, PortalClaim, PortalCategory, PortalPane, PortalTag, PortalSource, PortalEditor, PortalSubject,
            int, int, int, int, int, string, string, string>
        , IPortalStore<TAccessor>
        where TAccessor : PortalDbContextAccessor
    {
        /// <summary>
        /// 构造一个抽象门户存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStore(IAccessor accessor)
            : base(accessor)
        {
        }
    }


    /// <summary>
    /// 门户存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public class PortalStore<TAccessor, TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject,
        TClaimId, TCategoryId, TPaneId, TTagId, TSourceId, TEditorId, TSubjectId, TUserId>
        : PortalFullStore<TAccessor, TClaim, TCategory, TPane, PortalPaneClaim<int, TPaneId, TClaimId>, TTag, PortalTagClaim<int, TTagId, TClaimId>, TSource, TEditor, PortalEditorTitle<int, TEditorId>, TSubject, PortalSubjectBody<string, TSubjectId>, PortalSubjectClaim<int, TSubjectId, TClaimId>,
            TClaimId, TCategoryId, TPaneId, int, TTagId, int, TSourceId, TEditorId, int, TSubjectId, string, int, TUserId, DateTimeOffset>
        , IPortalStore<TAccessor, TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject,
            TClaimId, TPaneId, TTagId, TEditorId, TSubjectId>
        where TAccessor : PortalDbContextAccessor<TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject,
        TClaimId, TCategoryId, TPaneId, TTagId, TSourceId, TEditorId, TSubjectId, TUserId>
        where TClaim : PortalClaim<TClaimId>
        where TCategory : PortalCategory<TCategoryId>
        where TPane : PortalPane<TPaneId, TCategoryId>
        where TTag : PortalTag<TTagId>
        where TSource : PortalSource<TSourceId, TCategoryId>
        where TEditor : PortalEditor<TEditorId, TUserId>
        where TSubject : PortalSubject<TSubjectId, TCategoryId, DateTimeOffset>
        where TClaimId : IEquatable<TClaimId>
        where TCategoryId : IEquatable<TCategoryId>
        where TPaneId : IEquatable<TPaneId>
        where TTagId : IEquatable<TTagId>
        where TSourceId : IEquatable<TSourceId>
        where TEditorId : IEquatable<TEditorId>
        where TSubjectId : IEquatable<TSubjectId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 构造一个门户存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStore(IAccessor accessor)
            : base(accessor)
        {
        }
    }


    /// <summary>
    /// 门户完整存储。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TPaneClaim">指定的窗格声明类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TTagClaim">指定的标签声明类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TEditorTitle">指定的编者头衔类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TSubjectBody">指定的专题主体类型。</typeparam>
    /// <typeparam name="TSubjectClaim">指定的专题声明类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TPaneClaimId">指定的窗格声明标识类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    /// <typeparam name="TTagClaimId">指定的标签声明标识类型。</typeparam>
    /// <typeparam name="TSourceId">指定的来源标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TEditorTitleId">指定的编者头衔标识类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    /// <typeparam name="TSubjectBodyId">指定的专题主体标识类型。</typeparam>
    /// <typeparam name="TSubjectClaimId">指定的专题声明标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    /// <typeparam name="TDateTime">指定的日期与时间类型（提供对 DateTime 或 DateTimeOffset 的支持）。</typeparam>
    public class PortalFullStore<TAccessor, TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
        TClaimId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId, TDateTime>
        : AbstractBaseStore<TAccessor>
        , IPortalFullStore<TAccessor, TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim>
        where TAccessor : PortalDbContextAccessor<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
            TClaimId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId, TDateTime>
        where TClaim : PortalClaim<TClaimId>
        where TCategory : PortalCategory<TCategoryId>
        where TPane : PortalPane<TPaneId, TCategoryId>
        where TPaneClaim : PortalPaneClaim<TPaneClaimId, TPaneId, TClaimId>
        where TTag : PortalTag<TTagId>
        where TTagClaim : PortalTagClaim<TTagClaimId, TTagId, TClaimId>
        where TSource : PortalSource<TSourceId, TCategoryId>
        where TEditor : PortalEditor<TEditorId, TUserId>
        where TEditorTitle : PortalEditorTitle<TEditorTitleId, TEditorId>
        where TSubject : PortalSubject<TSubjectId, TCategoryId, TDateTime>
        where TSubjectBody : PortalSubjectBody<TSubjectBodyId, TSubjectId>
        where TSubjectClaim : PortalSubjectClaim<TSubjectClaimId, TSubjectId, TClaimId>
        where TClaimId : IEquatable<TClaimId>
        where TCategoryId : IEquatable<TCategoryId>
        where TPaneId : IEquatable<TPaneId>
        where TPaneClaimId : IEquatable<TPaneClaimId>
        where TTagId : IEquatable<TTagId>
        where TTagClaimId : IEquatable<TTagClaimId>
        where TSourceId : IEquatable<TSourceId>
        where TEditorId : IEquatable<TEditorId>
        where TEditorTitleId : IEquatable<TEditorTitleId>
        where TSubjectId : IEquatable<TSubjectId>
        where TSubjectBodyId : IEquatable<TSubjectBodyId>
        where TSubjectClaimId : IEquatable<TSubjectClaimId>
        where TUserId : IEquatable<TUserId>
        where TDateTime : struct
    {
        /// <summary>
        /// 构造一个完整门户存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalFullStore(IAccessor accessor)
            : base(accessor)
        {
        }


        /// <summary>
        /// 转换为声明标识。
        /// </summary>
        /// <param name="claimId">给定的声明标识对象。</param>
        /// <returns>返回 <typeparamref name="TClaimId"/>。</returns>
        protected virtual TClaimId ToClaimId(object claimId)
        {
            return claimId.IsValue<object, TClaimId>(nameof(claimId));
        }

        /// <summary>
        /// 转换为分类标识。
        /// </summary>
        /// <param name="categoryId">给定的分类标识对象。</param>
        /// <returns>返回 <typeparamref name="TCategoryId"/>。</returns>
        protected virtual TCategoryId ToCategoryId(object categoryId)
        {
            return categoryId.IsValue<object, TCategoryId>(nameof(categoryId));
        }

        /// <summary>
        /// 转换为窗格标识。
        /// </summary>
        /// <param name="paneId">给定的窗格标识对象。</param>
        /// <returns>返回 <typeparamref name="TPaneId"/>。</returns>
        protected virtual TPaneId ToPaneId(object paneId)
        {
            return paneId.IsValue<object, TPaneId>(nameof(paneId));
        }

        /// <summary>
        /// 转换为标签标识。
        /// </summary>
        /// <param name="tagId">给定的标签标识对象。</param>
        /// <returns>返回 <typeparamref name="TTagId"/>。</returns>
        protected virtual TTagId ToTagId(object tagId)
        {
            return tagId.IsValue<object, TTagId>(nameof(tagId));
        }

        /// <summary>
        /// 转换为编者标识。
        /// </summary>
        /// <param name="editorId">给定的编者标识对象。</param>
        /// <returns>返回 <typeparamref name="TEditorId"/>。</returns>
        protected virtual TEditorId ToEditorId(object editorId)
        {
            return editorId.IsValue<object, TEditorId>(nameof(editorId));
        }

        /// <summary>
        /// 转换为专题标识。
        /// </summary>
        /// <param name="subjectId">给定的专题标识对象。</param>
        /// <returns>返回 <typeparamref name="TSubjectId"/>。</returns>
        protected virtual TSubjectId ToSubjectId(object subjectId)
        {
            return subjectId.IsValue<object, TSubjectId>(nameof(subjectId));
        }

        /// <summary>
        /// 转换为用户标识。
        /// </summary>
        /// <param name="userId">给定的用户标识对象。</param>
        /// <returns>返回 <typeparamref name="TUserId"/>。</returns>
        protected virtual TUserId ToUserId(object userId)
        {
            return userId.IsValue<object, TUserId>(nameof(userId));
        }


        #region Claim

        /// <summary>
        /// 验证声明唯一性。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="model">给定的模型。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TClaim, bool>> VerifyClaimUniqueness(string type, string model)
        {
            type.NotNullOrEmpty(nameof(type));
            model.NotNullOrEmpty(nameof(model));

            // Type and Model is unique index
            return p => p.Type == type && p.Model == model;
        }

        /// <summary>
        /// 异步查找声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TClaim"/> 的异步操作。</returns>
        public virtual Task<TClaim> FindClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Claims.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取声明。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="model">给定的模型。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TClaim"/> 的异步操作。</returns>
        public virtual Task<TClaim> GetClaimAsync(string type, string model, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyClaimUniqueness(type, model);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Claims.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取所有声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TClaim>> GetAllClaimsAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Claims.ToList());
        }

        /// <summary>
        /// 异步获取分页声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TClaim>> GetPagingClaimsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.Claims.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TClaim[] claims)
        {
            return Accessor.Claims.TryCreateAsync(cancellationToken, claims);
        }

        /// <summary>
        /// 尝试更新声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TClaim[] claims)
        {
            return Accessor.Claims.TryUpdate(claims);
        }

        /// <summary>
        /// 尝试删除声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TClaim[] claims)
        {
            return Accessor.Claims.TryLogicDelete(claims);
        }

        #endregion


        #region Category

        /// <summary>
        /// 验证分类唯一性。
        /// </summary>
        /// <param name="parentId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TCategory, bool>> VerifyCategoryUniqueness(object parentId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _parentId = ToCategoryId(parentId);

            // ParentId and Name is unique index
            return p => p.ParentId.Equals(_parentId) && p.Name == name;
        }

        /// <summary>
        /// 异步查找分类。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        public virtual Task<TCategory> FindCategoryAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Categories.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取分类。
        /// </summary>
        /// <param name="parentId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        public virtual Task<TCategory> GetCategoryAsync(object parentId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyCategoryUniqueness(parentId, name);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Categories.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取所有分类集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TCategory}"/> 的异步操作。</returns>
        public virtual Task<List<TCategory>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Categories.ToList());
        }

        /// <summary>
        /// 异步获取分页分类集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TCategory}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TCategory>> GetPagingCategoriesAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.Categories.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建分类集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TCategory[] categories)
        {
            return Accessor.Categories.TryCreateAsync(cancellationToken, categories);
        }

        /// <summary>
        /// 尝试更新分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TCategory[] categories)
        {
            return Accessor.Categories.TryUpdate(categories);
        }

        /// <summary>
        /// 尝试删除分类集合。
        /// </summary>
        /// <param name="categories">给定的 <typeparamref name="TCategory"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TCategory[] categories)
        {
            return Accessor.Categories.TryLogicDelete(categories);
        }

        #endregion


        #region Pane

        /// <summary>
        /// 验证窗格唯一性。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TPane, bool>> VerifyPaneUniqueness(object categoryId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _categoryId = ToCategoryId(categoryId);

            // CategoryId and Name is unique index
            return p => p.CategoryId.Equals(_categoryId) && p.Name == name;
        }

        /// <summary>
        /// 异步查找窗格。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        public virtual Task<TPane> FindPaneAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Panes.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取窗格。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        public virtual Task<TPane> GetPaneAsync(object categoryId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyPaneUniqueness(categoryId, name);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Panes.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取所有窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TPane}"/> 的异步操作。</returns>
        public virtual Task<List<TPane>> GetAllPanesAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Panes.ToList());
        }

        /// <summary>
        /// 异步获取分页窗格集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPane}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TPane>> GetPagingPanesAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.Panes.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建窗格集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TPane[] panes)
        {
            return Accessor.Panes.TryCreateAsync(cancellationToken, panes);
        }

        /// <summary>
        /// 尝试更新窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TPane[] panes)
        {
            return Accessor.Panes.TryUpdate(panes);
        }

        /// <summary>
        /// 尝试删除窗格集合。
        /// </summary>
        /// <param name="panes">给定的 <typeparamref name="TPane"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TPane[] panes)
        {
            return Accessor.Panes.TryLogicDelete(panes);
        }

        #endregion


        #region PaneClaim

        /// <summary>
        /// 验证窗格声明唯一性。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TPaneClaim, bool>> VerifyPaneClaimUniqueness(object paneId, object claimId, string assocId)
        {
            assocId.NotNullOrEmpty(nameof(assocId));

            var _paneId = ToPaneId(paneId);
            var _claimId = ToClaimId(claimId);

            // PaneId, ClaimId and AssocId is unique index
            return p => p.PaneId.Equals(_paneId) && p.ClaimId.Equals(_claimId) && p.AssocId.Equals(assocId);
        }

        /// <summary>
        /// 异步查找窗格声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneClaim"/> 的异步操作。</returns>
        public virtual Task<TPaneClaim> FindPaneClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.PaneClaims.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取窗格声明。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneClaim"/> 的异步操作。</returns>
        public virtual Task<TPaneClaim> GetPaneClaimAsync(object paneId, object claimId, string assocId, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyPaneClaimUniqueness(paneId, claimId, assocId);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.PaneClaims.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取窗格声明集合。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TPaneClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TPaneClaim>> GetPaneClaimsAsync(object paneId, object claimId, CancellationToken cancellationToken = default)
        {
            var _paneId = ToPaneId(paneId);
            var _claimId = ToClaimId(claimId);

            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                return Accessor.PaneClaims.Where(p => p.PaneId.Equals(_paneId) && p.ClaimId.Equals(_claimId)).ToList();
            });
        }

        /// <summary>
        /// 异步获取所有窗格声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TPaneClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TPaneClaim>> GetAllPaneClaimsAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.PaneClaims.ToList());
        }

        /// <summary>
        /// 异步获取分页窗格声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPaneClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TPaneClaim>> GetPagingPaneClaimsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.PaneClaims.AsPagingByIndexAsync(ordered => ordered.OrderByDescending(k => k.Id),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建窗格声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="paneClaims">给定的 <typeparamref name="TPaneClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TPaneClaim[] paneClaims)
        {
            return Accessor.PaneClaims.TryCreateAsync(cancellationToken, paneClaims);
        }

        /// <summary>
        /// 尝试更新窗格声明集合。
        /// </summary>
        /// <param name="paneClaims">给定的 <typeparamref name="TPaneClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TPaneClaim[] paneClaims)
        {
            return Accessor.PaneClaims.TryUpdate(paneClaims);
        }

        /// <summary>
        /// 尝试删除窗格声明集合。
        /// </summary>
        /// <param name="paneClaims">给定的 <typeparamref name="TPaneClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TPaneClaim[] paneClaims)
        {
            return Accessor.PaneClaims.TryDelete(paneClaims);
        }

        #endregion


        #region Tag

        /// <summary>
        /// 验证标签唯一性。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TTag, bool>> VerifyTagUniqueness(string name)
        {
            name.NotNullOrEmpty(nameof(name));

            // Name is unique index
            return p => p.Name == name;
        }

        /// <summary>
        /// 异步查找标签。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        public virtual Task<TTag> FindTagAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Tags.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取标签。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        public virtual Task<TTag> GetTagAsync(string name, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyTagUniqueness(name);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Tags.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取所有标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TTag}"/> 的异步操作。</returns>
        public virtual Task<List<TTag>> GetAllTagsAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Tags.ToList());
        }

        /// <summary>
        /// 异步获取分页标签集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTag}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TTag>> GetPagingTagsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.Tags.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建标签集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TTag[] tags)
        {
            return Accessor.Tags.TryCreateAsync(cancellationToken, tags);
        }

        /// <summary>
        /// 尝试更新标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TTag[] tags)
        {
            return Accessor.Tags.TryUpdate(tags);
        }

        /// <summary>
        /// 尝试删除标签集合。
        /// </summary>
        /// <param name="tags">给定的 <typeparamref name="TTag"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TTag[] tags)
        {
            return Accessor.Tags.TryLogicDelete(tags);
        }

        #endregion


        #region TagClaim

        /// <summary>
        /// 验证标签声明唯一性。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TTagClaim, bool>> VerifyTagClaimUniqueness(object tagId, object claimId)
        {
            var _tagId = ToTagId(tagId);
            var _claimId = ToClaimId(claimId);

            // TagId and ClaimId is unique index
            return p => p.TagId.Equals(_tagId) && p.ClaimId.Equals(_claimId);
        }

        /// <summary>
        /// 异步查找标签声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        public virtual Task<TTagClaim> FindTagClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.TagClaims.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取标签声明。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        public virtual Task<TTagClaim> GetTagClaimAsync(object tagId, object claimId, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyTagClaimUniqueness(tagId, claimId);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.TagClaims.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取标签声明集合。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TTagClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TTagClaim>> GetTagClaimsAsync(object tagId, object claimId, CancellationToken cancellationToken = default)
        {
            var _tagId = ToTagId(tagId);
            var _claimId = ToClaimId(claimId);

            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                return Accessor.TagClaims.Where(p => p.TagId.Equals(_tagId) && p.ClaimId.Equals(_claimId)).ToList();
            });
        }

        /// <summary>
        /// 异步获取所有标签声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TTagClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TTagClaim>> GetAllTagClaimsAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.TagClaims.ToList());
        }

        /// <summary>
        /// 异步获取分页标签声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTagClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TTagClaim>> GetPagingTagClaimsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.TagClaims.AsPagingByIndexAsync(ordered => ordered.OrderByDescending(k => k.Id),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建标签声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="tagClaims">给定的 <typeparamref name="TTagClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TTagClaim[] tagClaims)
        {
            return Accessor.TagClaims.TryCreateAsync(cancellationToken, tagClaims);
        }

        /// <summary>
        /// 尝试更新标签声明集合。
        /// </summary>
        /// <param name="tagClaims">给定的 <typeparamref name="TTagClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TTagClaim[] tagClaims)
        {
            return Accessor.TagClaims.TryUpdate(tagClaims);
        }

        /// <summary>
        /// 尝试删除标签声明集合。
        /// </summary>
        /// <param name="tagClaims">给定的 <typeparamref name="TTagClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TTagClaim[] tagClaims)
        {
            return Accessor.TagClaims.TryDelete(tagClaims);
        }

        #endregion


        #region Source

        /// <summary>
        /// 验证来源唯一性。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TSource, bool>> VerifySourceUniqueness(object categoryId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _categoryId = ToCategoryId(categoryId);

            // CategoryId and Name is unique index
            return p => p.CategoryId.Equals(_categoryId) && p.Name == name;
        }

        /// <summary>
        /// 异步查找来源。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        public virtual Task<TSource> FindSourceAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Sources.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取来源。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        public virtual Task<TSource> GetSourceAsync(object categoryId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = VerifySourceUniqueness(categoryId, name);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Sources.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取来源集合。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSource}"/> 的异步操作。</returns>
        public virtual Task<List<TSource>> GetSourcesAsync(object categoryId, CancellationToken cancellationToken = default)
        {
            var _categoryId = ToCategoryId(categoryId);

            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                return Accessor.Sources.Where(p => p.CategoryId.Equals(categoryId)).ToList();
            });
        }

        /// <summary>
        /// 异步获取所有来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSource}"/> 的异步操作。</returns>
        public virtual Task<List<TSource>> GetAllSourcesAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Sources.ToList());
        }

        /// <summary>
        /// 异步获取分页来源集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSource}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TSource>> GetPagingSourcesAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.Sources.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建来源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSource[] sources)
        {
            return Accessor.Sources.TryCreateAsync(cancellationToken, sources);
        }

        /// <summary>
        /// 尝试更新来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TSource[] sources)
        {
            return Accessor.Sources.TryUpdate(sources);
        }

        /// <summary>
        /// 尝试删除来源集合。
        /// </summary>
        /// <param name="sources">给定的 <typeparamref name="TSource"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TSource[] sources)
        {
            return Accessor.Sources.TryLogicDelete(sources);
        }

        #endregion


        #region Editor

        /// <summary>
        /// 验证编者唯一性。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TEditor, bool>> VerifyEditorUniqueness(object userId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _userId = ToUserId(userId);

            // UserId and Name is unique index
            return p => p.UserId.Equals(_userId) && p.Name == name;
        }

        /// <summary>
        /// 异步查找编者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        public virtual Task<TEditor> FindEditorAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Editors.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取编者。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        public virtual Task<TEditor> GetEditorAsync(object userId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyEditorUniqueness(userId, name);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Editors.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取所有编者集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TEditor}"/> 的异步操作。</returns>
        public virtual Task<List<TEditor>> GetAllEditorsAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Editors.ToList());
        }

        /// <summary>
        /// 异步获取分页编者集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditor}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TEditor>> GetPagingEditorsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.Editors.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建编者集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TEditor[] editors)
        {
            return Accessor.Editors.TryCreateAsync(cancellationToken, editors);
        }

        /// <summary>
        /// 尝试更新编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TEditor[] editors)
        {
            return Accessor.Editors.TryUpdate(editors);
        }

        /// <summary>
        /// 尝试删除编者集合。
        /// </summary>
        /// <param name="editors">给定的 <typeparamref name="TEditor"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TEditor[] editors)
        {
            return Accessor.Editors.TryLogicDelete(editors);
        }

        #endregion


        #region EditorTitle

        /// <summary>
        /// 验证编者头衔唯一性。
        /// </summary>
        /// <param name="editorId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TEditorTitle, bool>> VerifyEditorTitleUniqueness(object editorId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _editorId = ToEditorId(editorId);

            // EditorId and Name is unique index
            return p => p.EditorId.Equals(_editorId) && p.Name == name;
        }

        /// <summary>
        /// 异步查找编者头衔。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        public virtual Task<TEditorTitle> FindEditorTitleAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.EditorTitles.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取编者头衔。
        /// </summary>
        /// <param name="editorId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        public virtual Task<TEditorTitle> GetEditorTitleAsync(object editorId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = VerifyEditorTitleUniqueness(editorId, name);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.EditorTitles.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取所有编者头衔集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TEditorTitle}"/> 的异步操作。</returns>
        public virtual Task<List<TEditorTitle>> GetAllEditorTitlesAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.EditorTitles.ToList());
        }

        /// <summary>
        /// 异步获取分页编者头衔集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditorTitle}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TEditorTitle>> GetPagingEditorTitlesAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.EditorTitles.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建编者头衔集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="editorTitles">给定的 <typeparamref name="TEditorTitle"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TEditorTitle[] editorTitles)
        {
            return Accessor.EditorTitles.TryCreateAsync(cancellationToken, editorTitles);
        }

        /// <summary>
        /// 尝试更新编者头衔集合。
        /// </summary>
        /// <param name="editorTitles">给定的 <typeparamref name="TEditorTitle"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TEditorTitle[] editorTitles)
        {
            return Accessor.EditorTitles.TryUpdate(editorTitles);
        }

        /// <summary>
        /// 尝试删除编者头衔集合。
        /// </summary>
        /// <param name="editorTitles">给定的 <typeparamref name="TEditorTitle"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TEditorTitle[] editorTitles)
        {
            return Accessor.EditorTitles.TryLogicDelete(editorTitles);
        }

        #endregion


        #region Subject

        /// <summary>
        /// 验证专题唯一性。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="title">给定的标题。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TSubject, bool>> VerifySubjectUniqueness(object categoryId, string title)
        {
            title.NotNullOrEmpty(nameof(title));

            var _categoryId = ToCategoryId(categoryId);

            // CategoryId and Name is unique index
            return p => p.CategoryId.Equals(_categoryId) && p.Title == title;
        }

        /// <summary>
        /// 异步查找专题。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        public virtual Task<TSubject> FindSubjectAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Subjects.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取专题。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="title">给定的标题。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        public virtual Task<TSubject> GetSubjectAsync(object categoryId, string title, CancellationToken cancellationToken = default)
        {
            var predicate = VerifySubjectUniqueness(categoryId, title);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Subjects.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取所有专题集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubject}"/> 的异步操作。</returns>
        public virtual Task<List<TSubject>> GetAllSubjectsAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.Subjects.ToList());
        }

        /// <summary>
        /// 异步获取分页专题集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubject}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TSubject>> GetPagingSubjectsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.Subjects.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建专题集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="subjects">给定的 <typeparamref name="TSubject"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSubject[] subjects)
        {
            return Accessor.Subjects.TryCreateAsync(cancellationToken, subjects);
        }

        /// <summary>
        /// 尝试更新专题集合。
        /// </summary>
        /// <param name="subjects">给定的 <typeparamref name="TSubject"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TSubject[] subjects)
        {
            return Accessor.Subjects.TryUpdate(subjects);
        }

        /// <summary>
        /// 尝试删除专题集合。
        /// </summary>
        /// <param name="subjects">给定的 <typeparamref name="TSubject"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TSubject[] subjects)
        {
            return Accessor.Subjects.TryLogicDelete(subjects);
        }

        #endregion


        #region SubjectBody

        ///// <summary>
        ///// 验证专题主体唯一性。
        ///// </summary>
        ///// <param name="subjectId">给定的专题标识。</param>
        ///// <param name="body">给定的主体。</param>
        ///// <returns>返回查询表达式。</returns>
        //public virtual Expression<Func<TSubjectBody, bool>> VerifySubjectBodyUniqueness(object subjectId, string body)
        //{
        //    body.NotNullOrEmpty(nameof(body));

        //    var _subjectId = ToSubjectId(subjectId);

        //    // SubjectId and Body is unique index
        //    return p => p.SubjectId.Equals(_subjectId) && p.Body == body;
        //}

        /// <summary>
        /// 异步查找专题主体。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        public virtual Task<TSubjectBody> FindSubjectBodyAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.SubjectBodies.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取专题主体集合。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectBody}"/> 的异步操作。</returns>
        public virtual Task<List<TSubjectBody>> GetSubjectBodiesAsync(object subjectId, CancellationToken cancellationToken = default)
        {
            var _subjectId = ToSubjectId(subjectId);

            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                return Accessor.SubjectBodies.Where(p => p.SubjectId.Equals(_subjectId)).ToList();
            });
        }


        /// <summary>
        /// 尝试异步创建专题主体集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="subjectBodies">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSubjectBody[] subjectBodies)
        {
            return Accessor.SubjectBodies.TryCreateAsync(cancellationToken, subjectBodies);
        }

        /// <summary>
        /// 尝试更新专题主体集合。
        /// </summary>
        /// <param name="subjectBodies">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TSubjectBody[] subjectBodies)
        {
            return Accessor.SubjectBodies.TryUpdate(subjectBodies);
        }

        /// <summary>
        /// 尝试删除专题主体集合。
        /// </summary>
        /// <param name="subjectBodies">给定的 <typeparamref name="TSubjectBody"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TSubjectBody[] subjectBodies)
        {
            return Accessor.SubjectBodies.TryDelete(subjectBodies);
        }

        #endregion


        #region SubjectClaim

        /// <summary>
        /// 验证专题声明唯一性。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <returns>返回查询表达式。</returns>
        public virtual Expression<Func<TSubjectClaim, bool>> VerifySubjectClaimUniqueness(object subjectId, object claimId, string assocId)
        {
            assocId.NotNullOrEmpty(nameof(assocId));

            var _subjectId = ToSubjectId(subjectId);
            var _claimId = ToClaimId(claimId);

            // SubjectId, ClaimId and AssocId is unique index
            return p => p.SubjectId.Equals(_subjectId) && p.ClaimId.Equals(_claimId) && p.AssocId.Equals(assocId);
        }

        /// <summary>
        /// 异步查找专题声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        public virtual Task<TSubjectClaim> FindSubjectClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.SubjectClaims.FindAsync(cancellationToken, keyValues);
        }

        /// <summary>
        /// 异步获取专题声明。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        public virtual Task<TSubjectClaim> GetSubjectClaimAsync(object subjectId, object claimId, string assocId, CancellationToken cancellationToken = default)
        {
            var predicate = VerifySubjectClaimUniqueness(subjectId, claimId, assocId);

            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.SubjectClaims.SingleOrDefault(predicate));
        }

        /// <summary>
        /// 异步获取专题声明集合。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TSubjectClaim>> GetSubjectClaimsAsync(object subjectId, object claimId, CancellationToken cancellationToken = default)
        {
            var _subjectId = ToSubjectId(subjectId);
            var _claimId = ToClaimId(claimId);

            return cancellationToken.RunFactoryOrCancellationAsync(() =>
            {
                return Accessor.SubjectClaims.Where(p => p.SubjectId.Equals(_subjectId) && p.ClaimId.Equals(_claimId)).ToList();
            });
        }

        /// <summary>
        /// 异步获取所有专题声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TSubjectClaim>> GetAllSubjectClaimsAsync(CancellationToken cancellationToken = default)
        {
            return cancellationToken.RunFactoryOrCancellationAsync(() => Accessor.SubjectClaims.ToList());
        }

        /// <summary>
        /// 异步获取分页专题声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubjectClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TSubjectClaim>> GetPagingSubjectClaimsAsync(int index, int size, CancellationToken cancellationToken = default)
        {
            return Accessor.SubjectClaims.AsPagingByIndexAsync(ordered => ordered.OrderByDescending(k => k.Id),
                index, size, cancellationToken);
        }


        /// <summary>
        /// 尝试异步创建专题声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="subjectClaims">给定的 <typeparamref name="TSubjectClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TSubjectClaim[] subjectClaims)
        {
            return Accessor.SubjectClaims.TryCreateAsync(cancellationToken, subjectClaims);
        }

        /// <summary>
        /// 尝试更新专题声明集合。
        /// </summary>
        /// <param name="subjectClaims">给定的 <typeparamref name="TSubjectClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TSubjectClaim[] subjectClaims)
        {
            return Accessor.SubjectClaims.TryUpdate(subjectClaims);
        }

        /// <summary>
        /// 尝试删除专题声明集合。
        /// </summary>
        /// <param name="subjectClaims">给定的 <typeparamref name="TSubjectClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TSubjectClaim[] subjectClaims)
        {
            return Accessor.SubjectClaims.TryDelete(subjectClaims);
        }

        #endregion

    }
}
