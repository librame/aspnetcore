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
    public class PortalSubjectClaim : PortalSubjectClaim<int>
    {
    }


    /// <summary>
    /// 门户专题声明。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalSubjectClaim<TIncremId> : AbstractIncremId<TIncremId>, IAssociation
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 专题标识。
        /// </summary>
        public virtual TIncremId SubjectId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        public virtual TIncremId ClaimId { get; set; }

        /// <summary>
        /// 关联标识。
        /// </summary>
        public virtual string AssocId { get; set; }
    }
}
