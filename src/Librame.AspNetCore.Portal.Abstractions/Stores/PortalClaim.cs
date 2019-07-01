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
    /// 门户声明。
    /// </summary>
    public class PortalClaim : PortalClaim<int>
    {
    }


    /// <summary>
    /// 门户声明。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public class PortalClaim<TId> : AbstractEntity<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 类型。
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 模型。
        /// </summary>
        public virtual string Model { get; set; }
    }
}
