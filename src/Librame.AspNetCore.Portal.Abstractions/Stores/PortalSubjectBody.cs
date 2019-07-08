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
    public class PortalSubjectBody : PortalSubjectBody<int, int>
    {
    }


    /// <summary>
    /// 门户专题主体。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TSubjectId">指定的专题标识类型。</typeparam>
    public class PortalSubjectBody<TId, TSubjectId> : AbstractId<TId>
        where TId : IEquatable<TId>
        where TSubjectId : IEquatable<TSubjectId>
    {
        /// <summary>
        /// 专题标识。
        /// </summary>
        public virtual TSubjectId SubjectId { get; set; }

        /// <summary>
        /// 主体散列。
        /// </summary>
        public virtual string BodyHash { get; set; }

        /// <summary>
        /// 正文。
        /// </summary>
        public virtual string Body { get; set; }
    }
}
