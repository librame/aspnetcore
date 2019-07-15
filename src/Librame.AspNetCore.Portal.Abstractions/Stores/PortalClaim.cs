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
    using Extensions;
    using Extensions.Data;

    /// <summary>
    /// 门户声明。
    /// </summary>
    public class PortalClaim : PortalClaim<int>
    {
        /// <summary>
        /// 构造一个门户声明实例。
        /// </summary>
        public PortalClaim()
            : base()
        {
        }

        /// <summary>
        /// 构造一个门户声明实例。
        /// </summary>
        /// <param name="entityType">给定的实体类型。</param>
        /// <param name="title">给定的标题。</param>
        public PortalClaim(Type entityType, string title)
            : base(entityType, title)
        {
        }
    }


    /// <summary>
    /// 门户声明。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalClaim<TIncremId> : AbstractEntityWithIncremId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 构造一个门户声明实例。
        /// </summary>
        public PortalClaim()
        {
        }

        /// <summary>
        /// 构造一个门户声明实例。
        /// </summary>
        /// <param name="entityType">给定的实体类型。</param>
        /// <param name="title">给定的标题。</param>
        public PortalClaim(Type entityType, string title)
        {
            Type = entityType.GetBodyName();
            Model = entityType.GetFullName();
            Title = title;
        }


        /// <summary>
        /// 类型。
        /// </summary>
        public virtual string Type { get; set; }

        /// <summary>
        /// 模型。
        /// </summary>
        public virtual string Model { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public virtual string Title { get; set; }
    }
}
