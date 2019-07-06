﻿#region License

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
    public interface IPortalStore<TAccessor>
        : IPortalStore<TAccessor, PortalClaim, PortalCategory, PortalPane, PortalTag, PortalSource, PortalEditor, PortalSubject,
            int, int, int, string, string>
        , IClaimStore<TAccessor, PortalClaim>
        , ICategoryStore<TAccessor, PortalCategory>
        , IPaneStore<TAccessor, PortalPane, PortalPaneClaim>
        , ITagStore<TAccessor, PortalTag, PortalTagClaim>
        , ISourceStore<TAccessor, PortalSource>
        , IEditorStore<TAccessor, PortalEditor, PortalEditorTitle>
        , ISubjectStore<TAccessor, PortalSubject, PortalSubjectBody, PortalSubjectClaim>
        where TAccessor : IAccessor
    {
    }

    /// <summary>
    /// 门户存储接口。
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
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    /// <typeparam name="TEditorId">指定的编者标识类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    public interface IPortalStore<TAccessor, TClaim, TCategory, TPane, TTag, TSource, TEditor, TSubject,
        TClaimId, TPaneId, TTagId, TEditorId, TSubjectId>
        : IFullPortalStore<TAccessor, TClaim, TCategory, TPane, PortalPaneClaim<int, TPaneId, TClaimId>, TTag, PortalTagClaim<int, TTagId, TClaimId>, TSource, TEditor, PortalEditorTitle<int, TEditorId>, TSubject, PortalSubjectBody<string, TSubjectId>, PortalSubjectClaim<int, TSubjectId, TClaimId>>
        , IClaimStore<TAccessor, TClaim>
        , ICategoryStore<TAccessor, TCategory>
        , IPaneStore<TAccessor, TPane, PortalPaneClaim<int, TPaneId, TClaimId>>
        , ITagStore<TAccessor, TTag, PortalTagClaim<int, TTagId, TClaimId>>
        , ISourceStore<TAccessor, TSource>
        , IEditorStore<TAccessor, TEditor, PortalEditorTitle<int, TEditorId>>
        , ISubjectStore<TAccessor, TSubject, PortalSubjectBody<string, TSubjectId>, PortalSubjectClaim<int, TSubjectId, TClaimId>>
        where TAccessor : IAccessor
        where TClaim : class
        where TCategory : class
        where TPane : class
        where TTag : class
        where TSource : class
        where TEditor : class
        where TSubject : class
        where TClaimId : IEquatable<TClaimId>
        where TPaneId : IEquatable<TPaneId>
        where TTagId : IEquatable<TTagId>
        where TEditorId : IEquatable<TEditorId>
        where TSubjectId : IEquatable<TSubjectId>
    {
    }


    /// <summary>
    /// 完整门户存储接口。
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
    public interface IFullPortalStore<TAccessor, TClaim, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim>
        : IClaimStore<TAccessor, TClaim>
        , ICategoryStore<TAccessor, TCategory>
        , IPaneStore<TAccessor, TPane, TPaneClaim>
        , ITagStore<TAccessor, TTag, TTagClaim>
        , ISourceStore<TAccessor, TSource>
        , IEditorStore<TAccessor, TEditor, TEditorTitle>
        , ISubjectStore<TAccessor, TSubject, TSubjectBody, TSubjectClaim>
        where TAccessor : IAccessor
        where TClaim : class
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
    {
    }
}