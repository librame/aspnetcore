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
    /// 门户专题。
    /// </summary>
    public class PortalSubject : PortalSubject<int>
    {
    }


    /// <summary>
    /// 门户专题。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalSubject<TIncremId> : AbstractEntityWithIncremId<TIncremId>, IPublishing<DateTimeOffset>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 分类标识。
        /// </summary>
        public virtual TIncremId CategoryId { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public virtual DateTimeOffset PublishTime { get; set; }

        /// <summary>
        /// 发布链接。
        /// </summary>
        public virtual string PublishLink { get; set; }

        /// <summary>
        /// 标题。
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// 副标题。
        /// </summary>
        public virtual string Subtitle { get; set; }

        /// <summary>
        /// 标签集合。
        /// </summary>
        public virtual string Tags { get; set; }
    }
}
