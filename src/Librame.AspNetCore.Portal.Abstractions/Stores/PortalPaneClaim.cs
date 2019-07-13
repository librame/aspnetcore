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
    public class PortalPaneClaim : PortalPaneClaim<int>
    {
    }


    /// <summary>
    /// 门户窗格声明。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalPaneClaim<TIncremId> : AbstractIncremId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 窗格标识。
        /// </summary>
        public virtual TIncremId PaneId { get; set; }

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
