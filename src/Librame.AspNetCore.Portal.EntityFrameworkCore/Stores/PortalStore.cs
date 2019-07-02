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
    public class PortalStore<TAccessor, TClaim, TClaimBody, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim,
        TClaimId, TClaimBodyId, TCategoryId, TPaneId, TPaneClaimId, TTagId, TTagClaimId, TSourceId, TEditorId, TEditorTitleId, TSubjectId, TSubjectBodyId, TSubjectClaimId, TUserId>
        : AbstractBaseStore<TAccessor>
        , IPortalStore<TAccessor, TClaim, TClaimBody, TCategory, TPane, TPaneClaim, TTag, TTagClaim, TSource, TEditor, TEditorTitle, TSubject, TSubjectBody, TSubjectClaim>
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
        /// <summary>
        /// 构造一个抽象门户存储实例。
        /// </summary>
        /// <param name="accessor">给定的 <see cref="IAccessor"/>。</param>
        public PortalStore(IAccessor accessor)
            : base(accessor)
        {
        }




    }
}
