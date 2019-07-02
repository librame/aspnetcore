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
        : PortalStore<TAccessor, TClaim, TCategory, TPane, PortalPaneClaim<int, TPaneId, TClaimId>, TTag, PortalTagClaim<int, TTagId, TClaimId>, TSource, TEditor, PortalEditorTitle<int, TEditorId>, TSubject, PortalSubjectBody<string, TSubjectId>, PortalSubjectClaim<int, TSubjectId, TClaimId>,
            TClaimId, TCategoryId, TPaneId, int, TTagId, int, TSourceId, TEditorId, int, TSubjectId, string, int, TUserId, DateTimeOffset>
        , IPortalStore<TAccessor, TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject>
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
    public class PortalStore<TAccessor, TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
        TClaimId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId, TDateTime>
        : AbstractBaseStore<TAccessor>
        , IPortalStore<TAccessor, TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim>
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
        /// 构造一个抽象门户存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStore(IAccessor accessor)
            : base(accessor)
        {
        }


        #region Claim

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
            type.NotNullOrEmpty(nameof(type));
            model.NotNullOrEmpty(nameof(model));



            return Task.Factory.StartNew(() =>
            {
                // Type and Model is unique index
                return Accessor.Claims.SingleOrDefault(p => p.Type == type && p.Model == model);
            },
            cancellationToken);
        }

        /// <summary>
        /// 异步获取所有声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>（可选）。</param>
        /// <returns>返回一个包含 <see cref="List{TClaim}"/> 的异步操作。</returns>
        public virtual Task<List<TClaim>> GetAllClaimsAsync(CancellationToken cancellationToken = default)
        {
            return Task.Factory.StartNew(() => Accessor.Claims.ToList(), cancellationToken);
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

        }


        /// <summary>
        /// 尝试异步创建声明集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回一个包含 <see cref="EntityResult"/> 的异步操作。</returns>
        public virtual Task<EntityResult> TryCreateAsync(CancellationToken cancellationToken, params TClaim[] claims)
        {

        }

        /// <summary>
        /// 尝试更新声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryUpdate(params TClaim[] claims)
        {

        }

        /// <summary>
        /// 尝试删除声明集合。
        /// </summary>
        /// <param name="claims">给定的 <typeparamref name="TClaim"/> 数组。</param>
        /// <returns>返回 <see cref="EntityResult"/>。</returns>
        public virtual EntityResult TryDelete(params TClaim[] claims)
        {

        }

        #endregion

    }
}
