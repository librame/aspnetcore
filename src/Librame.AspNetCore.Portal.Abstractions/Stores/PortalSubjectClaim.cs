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
    /// 门户专题声明。
    /// </summary>
    public class PortalSubjectClaim : PortalSubjectClaim<int, int, int>
    {
    }


    /// <summary>
    /// 门户专题声明。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    public class PortalSubjectClaim<TId, TSubjectId, TClaimId> : AbstractId<TId>, IAssociation
        where TId : IEquatable<TId>
        where TSubjectId : IEquatable<TSubjectId>
        where TClaimId : IEquatable<TClaimId>
    {
        /// <summary>
        /// 专题标识。
        /// </summary>
        public virtual TSubjectId SubjectId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        public virtual TClaimId ClaimId { get; set; }

        /// <summary>
        /// 关联标识。
        /// </summary>
        public virtual string AssocId { get; set; }
    }
}
