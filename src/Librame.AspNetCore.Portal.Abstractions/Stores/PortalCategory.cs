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
    /// <typeparam name="TIncremId">指定的标识类型。</typeparam>
    public class PortalCategory<TIncremId> : AbstractEntityWithIncremId<TIncremId>, IParentId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 父标识。
        /// </summary>
        public virtual TIncremId ParentId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }
    }
}
