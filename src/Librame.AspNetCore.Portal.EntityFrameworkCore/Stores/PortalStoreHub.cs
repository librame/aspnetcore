#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.EntityFrameworkCore;
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
    /// 门户存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class PortalStoreHub<TAccessor> : PortalStoreHub<TAccessor, PortalClaim, PortalCategory
        , PortalPane, PortalTag, PortalSource, PortalEditor, PortalSubject, string, int>
        , IPortalStoreHub<TAccessor>
        where TAccessor : PortalDbContextAccessor
    {
        /// <summary>
        /// 构造一个 <see cref="PortalStoreHub{TAccessor}"/> 实例（可用于容器构造）。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }

        /// <summary>
        /// 构造一个 <see cref="PortalStoreHub{TAccessor}"/> 实例（可用于手动构造）。
        /// </summary>
        /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
        protected PortalStoreHub(TAccessor accessor)
            : base(accessor)
        {
        }
    }


    /// <summary>
    /// 门户存储中心。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TCategory">指定的分类类型。</typeparam>
    /// <typeparam name="TPane">指定的窗格类型。</typeparam>
    /// <typeparam name="TTag">指定的标签类型。</typeparam>
    /// <typeparam name="TSource">指定的来源类型。</typeparam>
    /// <typeparam name="TEditor">指定的编者类型。</typeparam>
    /// <typeparam name="TSubject">指定的专题类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalStoreHub<TAccessor, TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject, TGenId, TIncremId>
        : PortalStoreHub<TAccessor, TClaim, TCategory, TPane, PortalPaneClaim<TIncremId>
            , TTag, PortalTagClaim<TGenId, TIncremId>, TSource, TEditor, PortalEditorTitle<TIncremId>
            , TSubject, PortalSubjectBody<TIncremId>, PortalSubjectClaim<TIncremId>, TGenId, TIncremId>
        , IPortalStoreHub<TAccessor, TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject, TGenId, TIncremId>
        where TAccessor : PortalDbContextAccessor<TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject, TGenId, TIncremId>
        where TClaim : PortalClaim<TIncremId>
        where TCategory : PortalCategory<TIncremId>
        where TPane : PortalPane<TIncremId>
        where TTag : PortalTag<TGenId>
        where TSource : PortalSource<TIncremId>
        where TEditor : PortalEditor<TIncremId, TGenId>
        where TSubject : PortalSubject<TIncremId>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户存储中心实例（可用于容器构造）。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }

        /// <summary>
        /// 构造一个门户存储中心实例（可用于手动构造）。
        /// </summary>
        /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
        protected PortalStoreHub(TAccessor accessor)
            : base(accessor)
        {
        }
    }


    /// <summary>
    /// 门户存储中心。
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
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalStoreHub<TAccessor, TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim
        , TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim, TGenId, TIncremId>
        : StoreHubBase<TAccessor>
        , IPortalStoreHub<TAccessor, TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim
            , TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim>
        where TAccessor : PortalDbContextAccessor<TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim
            , TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim, TGenId, TIncremId>
        where TClaim : PortalClaim<TIncremId>
        where TCategory : PortalCategory<TIncremId>
        where TPane : PortalPane<TIncremId>
        where TPaneClaim : PortalPaneClaim<TIncremId>
        where TTag : PortalTag<TGenId>
        where TTagClaim : PortalTagClaim<TGenId, TIncremId>
        where TSource : PortalSource<TIncremId>
        where TEditor : PortalEditor<TIncremId, TGenId>
        where TEditorTitle : PortalEditorTitle<TIncremId>
        where TSubject : PortalSubject<TIncremId>
        where TSubjectBody : PortalSubjectBody<TIncremId>
        where TSubjectClaim : PortalSubjectClaim<TIncremId>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户存储中心实例（可用于容器构造）。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }

        /// <summary>
        /// 构造一个门户存储中心实例（可用于手动构造）。
        /// </summary>
        /// <param name="accessor">给定的 <typeparamref name="TAccessor"/>。</param>
        protected PortalStoreHub(TAccessor accessor)
            : base(accessor)
        {
        }


        /// <summary>
        /// 转换为生成式标识。
        /// </summary>
        /// <param name="genId">给定的生成式标识对象。</param>
        /// <returns>返回 <typeparamref name="TGenId"/>。</returns>
        protected virtual TGenId ToGenId(object genId)
        {
            return genId.CastTo<object, TGenId>(nameof(genId));
        }

        /// <summary>
        /// 转换为增量式标识。
        /// </summary>
        /// <param name="incremId">给定的增量式标识对象。</param>
        /// <returns>返回 <typeparamref name="TIncremId"/>。</returns>
        protected virtual TIncremId ToIncremId(object incremId)
        {
            return incremId.CastTo<object, TIncremId>(nameof(incremId));
        }


        #region Claim

        /// <summary>
        /// 建立唯一声明表达式。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="model">给定的模型。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TClaim, bool>> BuildUniqueClaimExpression(string type, string model)
        {
            type.NotNullOrEmpty(nameof(type));
            model.NotNullOrEmpty(nameof(model));

            // Type and Model is unique index
            return p => p.Type == type && p.Model == model;
        }

        /// <summary>
        /// 异步包含指定声明。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="model">给定的模型。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainClaimAsync(string type, string model, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueClaimExpression(type, model);

            return Accessor.Claims.AnyAsync(predicate, cancellationToken);
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
            var predicate = BuildUniqueClaimExpression(type, model);

            return Accessor.Claims.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TClaim"/> 的异步操作。</returns>
        public virtual Task<TClaim> FindClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Claims.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TClaim>> GetAllClaimsAsync(Func<IQueryable<TClaim>, IQueryable<TClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Claims) ?? Accessor.Claims;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TClaim>> GetPagingClaimsAsync(int index, int size,
            Func<IQueryable<TClaim>, IQueryable<TClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Claims) ?? Accessor.Claims;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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
        /// 建立唯一分类表达式。
        /// </summary>
        /// <param name="parentId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TCategory, bool>> BuildUniqueCategoryExpression(object parentId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _parentId = ToIncremId(parentId);

            // ParentId and Name is unique index
            return p => p.ParentId.Equals(_parentId) && p.Name == name;
        }

        /// <summary>
        /// 异步包含指定分类。
        /// </summary>
        /// <param name="parentId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainCategoryAsync(object parentId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueCategoryExpression(parentId, name);

            return Accessor.Categories.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定分类。
        /// </summary>
        /// <param name="parentId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        public virtual Task<TCategory> GetCategoryAsync(object parentId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueCategoryExpression(parentId, name);

            return Accessor.Categories.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定分类。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TCategory"/> 的异步操作。</returns>
        public virtual Task<TCategory> FindCategoryAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Categories.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有分类集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TCategory}"/> 的异步操作。</returns>
        public virtual Task<List<TCategory>> GetAllCategoriesAsync(Func<IQueryable<TCategory>, IQueryable<TCategory>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Categories) ?? Accessor.Categories;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页分类集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TCategory}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TCategory>> GetPagingCategoriesAsync(int index, int size,
            Func<IQueryable<TCategory>, IQueryable<TCategory>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Categories) ?? Accessor.Categories;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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
        /// 建立唯一窗格表达式。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TPane, bool>> BuildUniquePaneExpression(object categoryId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _categoryId = ToIncremId(categoryId);

            // CategoryId and Name is unique index
            return p => p.CategoryId.Equals(_categoryId) && p.Name == name;
        }

        /// <summary>
        /// 异步包含指定窗格。
        /// </summary>
        /// <param name="categoryId">给定的父标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainPaneAsync(object categoryId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniquePaneExpression(categoryId, name);

            return Accessor.Panes.AnyAsync(predicate, cancellationToken);
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
            var predicate = BuildUniquePaneExpression(categoryId, name);

            return Accessor.Panes.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找窗格。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPane"/> 的异步操作。</returns>
        public virtual Task<TPane> FindPaneAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Panes.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有窗格集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TPane}"/> 的异步操作。</returns>
        public virtual Task<List<TPane>> GetAllPanesAsync(Func<IQueryable<TPane>, IQueryable<TPane>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Panes) ?? Accessor.Panes;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页窗格集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPane}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TPane>> GetPagingPanesAsync(int index, int size,
            Func<IQueryable<TPane>, IQueryable<TPane>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Panes) ?? Accessor.Panes;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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
        /// 建立唯一窗格声明表达式。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TPaneClaim, bool>> BuildUniquePaneClaimExpression(object paneId, object claimId, string assocId)
        {
            assocId.NotNullOrEmpty(nameof(assocId));

            var _paneId = ToIncremId(paneId);
            var _claimId = ToIncremId(claimId);

            // PaneId, ClaimId and AssocId is unique index
            return p => p.PaneId.Equals(_paneId) && p.ClaimId.Equals(_claimId) && p.AssocId.Equals(assocId);
        }

        /// <summary>
        /// 异步包含指定窗格。
        /// </summary>
        /// <param name="paneId">给定的窗格标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainPaneClaimAsync(object paneId, object claimId, string assocId, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniquePaneClaimExpression(paneId, claimId, assocId);

            return Accessor.PaneClaims.AnyAsync(predicate, cancellationToken);
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
            var predicate = BuildUniquePaneClaimExpression(paneId, claimId, assocId);

            return Accessor.PaneClaims.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找窗格声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TPaneClaim"/> 的异步操作。</returns>
        public virtual Task<TPaneClaim> FindPaneClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.PaneClaims.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有窗格声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TPaneClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TPaneClaim>> GetAllPaneClaimsAsync(Func<IQueryable<TPaneClaim>, IQueryable<TPaneClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.PaneClaims) ?? Accessor.PaneClaims;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页窗格声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TPaneClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TPaneClaim>> GetPagingPaneClaimsAsync(int index, int size,
            Func<IQueryable<TPaneClaim>, IQueryable<TPaneClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.PaneClaims) ?? Accessor.PaneClaims;

            return query.AsPagingByIndexAsync(q => q.OrderByDescending(k => k.Id),
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
        /// 建立唯一标签表达式。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TTag, bool>> BuildUniqueTagExpression(string name)
        {
            name.NotNullOrEmpty(nameof(name));

            // Name is unique index
            return p => p.Name == name;
        }

        /// <summary>
        /// 异步包含指定标签。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainTagAsync(string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueTagExpression(name);

            return Accessor.Tags.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定标签。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        public virtual Task<TTag> GetTagAsync(string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueTagExpression(name);

            return Accessor.Tags.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定标签。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTag"/> 的异步操作。</returns>
        public virtual Task<TTag> FindTagAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Tags.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有标签集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TTag}"/> 的异步操作。</returns>
        public virtual Task<List<TTag>> GetAllTagsAsync(Func<IQueryable<TTag>, IQueryable<TTag>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Tags) ?? Accessor.Tags;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页标签集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTag}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TTag>> GetPagingTagsAsync(int index, int size,
            Func<IQueryable<TTag>, IQueryable<TTag>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Tags) ?? Accessor.Tags;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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
        /// 建立唯一标签声明表达式。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TTagClaim, bool>> BuildUniqueTagClaimExpression(object tagId, object claimId)
        {
            var _tagId = ToGenId(tagId);
            var _claimId = ToIncremId(claimId);

            // TagId and ClaimId is unique index
            return p => p.TagId.Equals(_tagId) && p.ClaimId.Equals(_claimId);
        }

        /// <summary>
        /// 异步包含指定标签声明。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainTagClaimAsync(object tagId, object claimId, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueTagClaimExpression(tagId, claimId);

            return Accessor.TagClaims.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定标签声明。
        /// </summary>
        /// <param name="tagId">给定的标签标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        public virtual Task<TTagClaim> GetTagClaimAsync(object tagId, object claimId, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueTagClaimExpression(tagId, claimId);

            return Accessor.TagClaims.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定标签声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TTagClaim"/> 的异步操作。</returns>
        public virtual Task<TTagClaim> FindTagClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.TagClaims.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有标签声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TTagClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TTagClaim>> GetAllTagClaimsAsync(Func<IQueryable<TTagClaim>, IQueryable<TTagClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.TagClaims) ?? Accessor.TagClaims;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页标签声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TTagClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TTagClaim>> GetPagingTagClaimsAsync(int index, int size,
            Func<IQueryable<TTagClaim>, IQueryable<TTagClaim>> queryFactory = null, CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.TagClaims) ?? Accessor.TagClaims;

            return query.AsPagingByIndexAsync(ordered => ordered.OrderByDescending(k => k.Id),
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
        /// 建立唯一来源表达式。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TSource, bool>> BuildUniqueSourceExpression(object categoryId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _categoryId = ToIncremId(categoryId);

            // CategoryId and Name is unique index
            return p => p.CategoryId.Equals(_categoryId) && p.Name == name;
        }

        /// <summary>
        /// 异步包含指定来源。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainSourceAsync(object categoryId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueSourceExpression(categoryId, name);

            return Accessor.Sources.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定来源。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        public virtual Task<TSource> GetSourceAsync(object categoryId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueSourceExpression(categoryId, name);

            return Accessor.Sources.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定来源。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSource"/> 的异步操作。</returns>
        public virtual Task<TSource> FindSourceAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Sources.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有来源集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSource}"/> 的异步操作。</returns>
        public virtual Task<List<TSource>> GetAllSourcesAsync(Func<IQueryable<TSource>, IQueryable<TSource>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Sources) ?? Accessor.Sources;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页来源集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSource}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TSource>> GetPagingSourcesAsync(int index, int size,
            Func<IQueryable<TSource>, IQueryable<TSource>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Sources) ?? Accessor.Sources;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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
        /// 建立唯一编者表达式。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TEditor, bool>> BuildUniqueEditorExpression(object userId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _userId = ToGenId(userId);

            // UserId and Name is unique index
            return p => p.UserId.Equals(_userId) && p.Name == name;
        }

        /// <summary>
        /// 异步包含指定编者。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainEditorAsync(object userId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueEditorExpression(userId, name);

            return Accessor.Editors.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定编者。
        /// </summary>
        /// <param name="userId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        public virtual Task<TEditor> GetEditorAsync(object userId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueEditorExpression(userId, name);

            return Accessor.Editors.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定编者。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditor"/> 的异步操作。</returns>
        public virtual Task<TEditor> FindEditorAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Editors.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有编者集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TEditor}"/> 的异步操作。</returns>
        public virtual Task<List<TEditor>> GetAllEditorsAsync(Func<IQueryable<TEditor>, IQueryable<TEditor>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Editors) ?? Accessor.Editors;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页编者集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditor}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TEditor>> GetPagingEditorsAsync(int index, int size,
            Func<IQueryable<TEditor>, IQueryable<TEditor>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Editors) ?? Accessor.Editors;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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
        /// 建立唯一编者头衔表达式。
        /// </summary>
        /// <param name="editorId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TEditorTitle, bool>> BuildUniqueEditorTitleExpression(object editorId, string name)
        {
            name.NotNullOrEmpty(nameof(name));

            var _editorId = ToIncremId(editorId);

            // EditorId and Name is unique index
            return p => p.EditorId.Equals(_editorId) && p.Name == name;
        }

        /// <summary>
        /// 异步包含指定编者头衔。
        /// </summary>
        /// <param name="editorId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainEditorTitleAsync(object editorId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueEditorTitleExpression(editorId, name);

            return Accessor.EditorTitles.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定编者头衔。
        /// </summary>
        /// <param name="editorId">给定的用户标识。</param>
        /// <param name="name">给定的名称。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        public virtual Task<TEditorTitle> GetEditorTitleAsync(object editorId, string name, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueEditorTitleExpression(editorId, name);

            return Accessor.EditorTitles.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定编者头衔。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TEditorTitle"/> 的异步操作。</returns>
        public virtual Task<TEditorTitle> FindEditorTitleAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.EditorTitles.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有编者头衔集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TEditorTitle}"/> 的异步操作。</returns>
        public virtual Task<List<TEditorTitle>> GetAllEditorTitlesAsync(Func<IQueryable<TEditorTitle>, IQueryable<TEditorTitle>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.EditorTitles) ?? Accessor.EditorTitles;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页编者头衔集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TEditorTitle}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TEditorTitle>> GetPagingEditorTitlesAsync(int index, int size,
            Func<IQueryable<TEditorTitle>, IQueryable<TEditorTitle>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.EditorTitles) ?? Accessor.EditorTitles;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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
        /// 建立唯一专题表达式。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="title">给定的标题。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TSubject, bool>> BuildUniqueSubjectExpression(object categoryId, string title)
        {
            title.NotNullOrEmpty(nameof(title));

            var _categoryId = ToIncremId(categoryId);

            // CategoryId and Name is unique index
            return p => p.CategoryId.Equals(_categoryId) && p.Title == title;
        }

        /// <summary>
        /// 异步包含指定专题。
        /// </summary>
        /// <param name="categoryId">给定的分类标识。</param>
        /// <param name="title">给定的标题。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainSubjectAsync(object categoryId, string title, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueSubjectExpression(categoryId, title);

            return Accessor.Subjects.AnyAsync(predicate, cancellationToken);
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
            var predicate = BuildUniqueSubjectExpression(categoryId, title);

            return Accessor.Subjects.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找专题。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubject"/> 的异步操作。</returns>
        public virtual Task<TSubject> FindSubjectAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.Subjects.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有专题集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubject}"/> 的异步操作。</returns>
        public virtual Task<List<TSubject>> GetAllSubjectsAsync(Func<IQueryable<TSubject>, IQueryable<TSubject>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Subjects) ?? Accessor.Subjects;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页专题集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubject}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TSubject>> GetPagingSubjectsAsync(int index, int size,
            Func<IQueryable<TSubject>, IQueryable<TSubject>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.Subjects) ?? Accessor.Subjects;

            return query.AsDescendingPagingByIndexAsync(index, size, cancellationToken);
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

        /// <summary>
        /// 建立唯一专题主体表达式。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="textHash">给定的文本散列。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TSubjectBody, bool>> BuildUniqueSubjectBodyExpression(object subjectId, string textHash)
        {
            textHash.NotNullOrEmpty(nameof(textHash));

            var _subjectId = ToIncremId(subjectId);

            // SubjectId and BodyHash is unique index
            return p => p.SubjectId.Equals(_subjectId) && p.TextHash == textHash;
        }

        /// <summary>
        /// 异步包含指定专题主体。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="textHash">给定的文本散列。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainSubjectBodyAsync(object subjectId, string textHash, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueSubjectBodyExpression(subjectId, textHash);

            return Accessor.SubjectBodies.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定专题主体。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="bodyHash">给定的主体散列。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        public virtual Task<TSubjectBody> GetSubjectBodyAsync(object subjectId, string bodyHash, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueSubjectBodyExpression(subjectId, bodyHash);

            return Accessor.SubjectBodies.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定专题主体。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectBody"/> 的异步操作。</returns>
        public virtual Task<TSubjectBody> FindSubjectBodyAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.SubjectBodies.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取专题主体集合。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectBody}"/> 的异步操作。</returns>
        public virtual Task<List<TSubjectBody>> GetSubjectBodiesAsync(object subjectId, CancellationToken cancellationToken = default)
        {
            var _subjectId = ToIncremId(subjectId);

            return Accessor.SubjectBodies.Where(p => p.SubjectId.Equals(_subjectId)).ToListAsync(cancellationToken);
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
        /// 建立唯一专题声明表达式。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<TSubjectClaim, bool>> BuildUniqueSubjectClaimExpression(object subjectId, object claimId, string assocId)
        {
            assocId.NotNullOrEmpty(nameof(assocId));

            var _subjectId = ToIncremId(subjectId);
            var _claimId = ToIncremId(claimId);

            // SubjectId, ClaimId and AssocId is unique index
            return p => p.SubjectId.Equals(_subjectId) && p.ClaimId.Equals(_claimId) && p.AssocId.Equals(assocId);
        }

        /// <summary>
        /// 异步包含指定专题声明。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含布尔值的异步操作。</returns>
        public virtual Task<bool> ContainTenantAsync(object subjectId, object claimId, string assocId, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueSubjectClaimExpression(subjectId, claimId, assocId);

            return Accessor.SubjectClaims.AnyAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步获取指定专题声明。
        /// </summary>
        /// <param name="subjectId">给定的专题标识。</param>
        /// <param name="claimId">给定的声明标识。</param>
        /// <param name="assocId">给定的关联标识。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        public virtual Task<TSubjectClaim> GetSubjectClaimAsync(object subjectId, object claimId, string assocId, CancellationToken cancellationToken = default)
        {
            var predicate = BuildUniqueSubjectClaimExpression(subjectId, claimId, assocId);

            return Accessor.SubjectClaims.SingleOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// 异步查找指定专题声明。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="keyValues">给定的键值对数组或标识。</param>
        /// <returns>返回一个包含 <typeparamref name="TSubjectClaim"/> 的异步操作。</returns>
        public virtual Task<TSubjectClaim> FindSubjectClaimAsync(CancellationToken cancellationToken, params object[] keyValues)
        {
            return Accessor.SubjectClaims.FindAsync(keyValues, cancellationToken);
        }

        /// <summary>
        /// 异步获取所有专题声明集合。
        /// </summary>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TSubjectClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TSubjectClaim>> GetAllSubjectClaimsAsync(Func<IQueryable<TSubjectClaim>, IQueryable<TSubjectClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.SubjectClaims) ?? Accessor.SubjectClaims;

            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 异步获取分页专题声明集合。
        /// </summary>
        /// <param name="index">给定的页索引。</param>
        /// <param name="size">给定的页大小。</param>
        /// <param name="queryFactory">给定的查询工厂方法（可选）。</param>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPageable{TSubjectClaim}"/> 的异步操作。</returns>
        public virtual Task<IPageable<TSubjectClaim>> GetPagingSubjectClaimsAsync(int index, int size,
            Func<IQueryable<TSubjectClaim>, IQueryable<TSubjectClaim>> queryFactory = null,
            CancellationToken cancellationToken = default)
        {
            var query = queryFactory?.Invoke(Accessor.SubjectClaims) ?? Accessor.SubjectClaims;

            return query.AsPagingByIndexAsync(ordered => ordered.OrderByDescending(k => k.Id),
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
