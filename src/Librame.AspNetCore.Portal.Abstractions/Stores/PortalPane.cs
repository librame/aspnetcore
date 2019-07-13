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
    /// 门户窗格。
    /// </summary>
    public class PortalPane : PortalPane<int>
    {
    }


    /// <summary>
    /// 门户窗格。
    /// </summary>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    public class PortalPane<TIncremId> : AbstractEntityWithIncremId<TIncremId>
        where TIncremId : IEquatable<TIncremId>
    {
        /// <summary>
        /// 分类标识。
        /// </summary>
        public virtual TIncremId CategoryId { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 路径。
        /// </summary>
        public virtual string Path { get; set; }
    }
}
