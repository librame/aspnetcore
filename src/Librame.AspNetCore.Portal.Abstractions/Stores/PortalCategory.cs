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
    /// 门户分类。
    /// </summary>
    public class PortalCategory : PortalCategory<int>
    {
    }


    /// <summary>
    /// 门户分类。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    public class PortalCategory<TId> : AbstractEntity<TId>, IParentId<TId>
        where TId : IEquatable<TId>
    {
        /// <summary>
        /// 父标识。
        /// </summary>
        public virtual TId ParentId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }
    }
}
