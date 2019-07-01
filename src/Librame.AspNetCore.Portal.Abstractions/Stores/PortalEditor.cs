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
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 门户编者。
    /// </summary>
    public class PortalEditor : PortalEditor<string, string>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalEditor"/> 实例。
        /// </summary>
        public PortalEditor()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            UserId = Id = GuIdentifier.Empty;
        }
    }


    /// <summary>
    /// 门户编者。
    /// </summary>
    /// <typeparam name="TId">指定的标识类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户标识类型。</typeparam>
    public class PortalEditor<TId, TUserId> : AbstractEntity<TId>
        where TId : IEquatable<TId>
        where TUserId : IEquatable<TUserId>
    {
        /// <summary>
        /// 用户标识。
        /// </summary>
        public virtual TUserId UserId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }
    }
}
