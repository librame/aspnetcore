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
        /// <summary>
        /// 构造一个门户分类实例。
        /// </summary>
        public PortalCategory()
            : base()
        {
        }

        /// <summary>
        /// 构造一个门户分类。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="descr">给定的说明。</param>
        public PortalCategory(string name, string descr)
            : base(name, descr)
        {
        }
    }


    /// <summary>
    /// 门户分类。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalCategory<TIncremId> : AbstractEntityWithIncremId<TIncremId>, IParentId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户分类实例。
        /// </summary>
        public PortalCategory()
        {
        }

        /// <summary>
        /// 构造一个门户分类实例。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="descr">给定的说明。</param>
        public PortalCategory(string name, string descr)
        {
            Name = name;
            Descr = descr;
        }


        /// <summary>
        /// 父标识。
        /// </summary>
        public virtual TIncremId ParentId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 说明。
        /// </summary>
        public virtual string Descr { get; set; }
    }
}
