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
    public class PortalEditor : PortalEditor<int, string>
    {
        /// <summary>
        /// 构造一个 <see cref="PortalEditor"/> 实例。
        /// </summary>
        public PortalEditor()
            : base()
        {
            // 默认使用空标识符，新增推荐使用服务注入
            UserId = UniqueIdentifier.Empty;
        }
    }


    /// <summary>
    /// 门户编者。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class PortalEditor<TIncremId, TGenId> : AbstractEntityWithIncremId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 用户标识。
        /// </summary>
        public virtual TGenId UserId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }
    }
}
