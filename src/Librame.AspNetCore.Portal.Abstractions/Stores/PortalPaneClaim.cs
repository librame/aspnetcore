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
    /// 门户窗格声明。
    /// </summary>
    public class PortalPaneClaim : PortalPaneClaim<int, int, int>
    {
    }


    /// <summary>
    /// 门户窗格声明。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TPaneId">指定的窗格标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    public class PortalPaneClaim<TId, TPaneId, TClaimId> : AbstractId<TId>, IAssociation
        where TId : IEquatable<TId>
        where TPaneId : IEquatable<TPaneId>
        where TClaimId : IEquatable<TClaimId>
    {
        /// <summary>
        /// 窗格标识。
        /// </summary>
        public virtual TPaneId PaneId { get; set; }

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
