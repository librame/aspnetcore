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
    /// 门户来源。
    /// </summary>
    public class PortalSource : PortalSource<int>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalSource{TIncremId}"/> 实例。
        /// </summary>
        public PortalSource()
            : base()
        {
        }

        /// <summary>
        /// 构造一个 <see cref="PortalSource{TIncremId}"/> 实例。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="link">给定的链接。</param>
        public PortalSource(string name, string link)
            : base(name, link)
        {
        }
    }


    /// <summary>
    /// 门户来源。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalSource<TIncremId> : AbstractEntityWithIncremId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalSource{TIncremId}"/> 实例。
        /// </summary>
        public PortalSource()
        {
        }

        /// <summary>
        /// 构造一个 <see cref="PortalSource{TIncremId}"/> 实例。
        /// </summary>
        /// <param name="name">给定的名称。</param>
        /// <param name="link">给定的链接。</param>
        public PortalSource(string name, string link)
        {
            Name = name;
            Link = link;
        }


        /// <summary>
        /// 分类标识。
        /// </summary>
        public virtual TIncremId CategoryId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 标志。
        /// </summary>
        public virtual string Logo { get; set; }

        /// <summary>
        /// 链接。
        /// </summary>
        public virtual string Link { get; set; }

        /// <summary>
        /// 备注。
        /// </summary>
        public virtual string Descr { get; set; }
    }
}
