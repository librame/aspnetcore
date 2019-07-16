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
    /// 门户标签。
    /// </summary>
    public class PortalTag : PortalTag<string>
    {
    }


    /// <summary>
    /// 门户标签。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class PortalTag<TGenId> : AbstractEntityWithGenId<TGenId>
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        public virtual string Name { get; set; }
    }
}
