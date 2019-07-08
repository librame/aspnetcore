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
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 门户标签声明。
    /// </summary>
    public class PortalTagClaim : PortalTagClaim<string, string, int>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalTagClaim"/> 实例。
        /// </summary>
        public PortalTagClaim()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            TagId = Id = GuIdentifier.Empty;
        }
    }


    /// <summary>
    /// 门户标签声明。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TTagId">指定的标签标识类型。</typeparam>
    /// <typeparam name="TClaimId">指定的声明标识类型。</typeparam>
    public class PortalTagClaim<TId, TTagId, TClaimId> : AbstractId<TId>
        where TId : IEquatable<TId>
        where TTagId : IEquatable<TTagId>
        where TClaimId : IEquatable<TClaimId>
    {
        /// <summary>
        /// 标签标识。
        /// </summary>
        public virtual TTagId TagId { get; set; }

        /// <summary>
        /// 声明标识。
        /// </summary>
        public virtual TClaimId ClaimId { get; set; }

        /// <summary>
        /// 引用数。
        /// </summary>
        public virtual int ReferCount { get; set; }
    }
}
