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
    public class PortalTagClaim : PortalTagClaim<string, int>
    {
    }


    /// <summary>
    /// 门户标签声明。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalTagClaim<TGenId, TIncremId> : AbstractGenId<TGenId>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户标签声明实例。
        /// </summary>
        public PortalTagClaim()
        {
            TagId = Id;
        }


        /// <summary>
        /// 标签标识。
        /// </summary>
        public virtual TGenId TagId { get; set; }

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
