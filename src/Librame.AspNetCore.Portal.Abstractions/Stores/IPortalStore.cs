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

namespace Librame.AspNetCore.Portal
{
    using Extensions.Data;

    /// <summary>
    /// 门户存储接口。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TClaim">指定的声明类型。</typeparam>
    /// <typeparam name="TClaimBody">指定的声明主体类型。</typeparam>
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
    /// <typeparam name="TClaimBodyId">指定的声明主体标识类型。</typeparam>
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
    public interface IPortalStore<TAccessor, TClaim, TClaimBody, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
        TClaimId, TClaimBodyId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId>
        : IClaimStore<TAccessor, TClaim, TClaimId>
        , ICategoryStore<TAccessor, TCategory, TCategoryId>
        , IPaneStore<TAccessor, TPane, TPaneClaim, TPaneId, TPaneClaimId, TClaimId, TCategoryId>
        , ITagStore<TAccessor, TTag, TTagClaim, TTagId, TTagClaimId, TClaimId>
        , ISourceStore<TAccessor, TSource, TSourceId, TCategoryId>
        , IEditorStore<TAccessor, TEditor, TEditorTitle, TEditorId, TEditorTitleId, TUserId>
        where TAccessor : IAccessor
        where TClaim : class
        where TClaimBody : class
        where TCategory : class
        where TPane : class
        where TPaneClaim : class
        where TTag : class
        where TTagClaim : class
        where TSource : class
        where TEditor : class
        where TEditorTitle : class
        where TSubject : class
        where TSubjectBody : class
        where TSubjectClaim : class
        where TClaimId : IEquatable<TClaimId>
        where TClaimBodyId : IEquatable<TClaimBodyId>
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
    {
    }
}
