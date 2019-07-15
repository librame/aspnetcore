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
    /// 门户专题主体。
    /// </summary>
    public class PortalSubjectBody : PortalSubjectBody<int>
    {
    }


    /// <summary>
    /// 门户专题主体。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalSubjectBody<TIncremId> : AbstractIncremId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 专题标识。
        /// </summary>
        public virtual TIncremId SubjectId { get; set; }

        /// <summary>
        /// 文本散列。
        /// </summary>
        public virtual string TextHash { get; set; }

        /// <summary>
        /// 文本。
        /// </summary>
        public virtual string Text { get; set; }
    }
}
