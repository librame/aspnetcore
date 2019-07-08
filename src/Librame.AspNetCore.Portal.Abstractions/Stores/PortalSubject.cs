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
    public class PortalSubject : PortalSubject<int, int, DateTimeOffset>
    {
    }


    /// <summary>
    /// 门户专题。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TCategoryId">指定的分类标识类型。</typeparam>
    /// <typeparam name="TDateTime">指定的日期与时间类型（提供对 DateTime 或 DateTimeOffset 的支持）。</typeparam>
    public class PortalSubject<TId, TCategoryId, TDateTime> : AbstractEntity<TId>, IPublishing<TDateTime>
        where TId : IEquatable<TId>
        where TCategoryId : IEquatable<TCategoryId>
        where TDateTime : struct
    {
        /// <summary>
        /// 分类标识。
        /// </summary>
        public virtual TCategoryId CategoryId { get; set; }

        /// <summary>
        /// 发布时间。
        /// </summary>
        public virtual TDateTime PublishTime { get; set; }

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
