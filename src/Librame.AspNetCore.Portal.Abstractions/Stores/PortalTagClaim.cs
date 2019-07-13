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
    /// 门户标签声明。
    /// </summary>
    public class PortalTagClaim : PortalTagClaim<int>
    {
    }


    /// <summary>
    /// 门户标签声明。
    /// </summary>
    /// <typeparam name="TIncremId">指定的标识类型。</typeparam>
    public class PortalTagClaim<TIncremId> : AbstractIncremId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 标签标识。
        /// </summary>
        public virtual TIncremId TagId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        public virtual TIncremId ClaimId { get; set; }

        /// <summary>
        /// 引用数。
        /// </summary>
        public virtual int ReferCount { get; set; }
    }
}
