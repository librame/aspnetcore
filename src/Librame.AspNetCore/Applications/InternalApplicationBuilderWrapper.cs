#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    /// <summary>
    /// 内部 <see cref="IApplicationBuilder"/> 封装器。
    /// </summary>
    internal class InternalApplicationBuilderWrapper : AbstractBuilderWrapper<IApplicationBuilder>, IApplicationBuilderWrapper
    {
        /// <summary>
        /// 构造一个 <see cref="InternalApplicationBuilderWrapper"/> 实例。
        /// </summary>
        /// <param name="rawBuilder">给定的 <see cref="IApplicationBuilder"/>。</param>
        public InternalApplicationBuilderWrapper(IApplicationBuilder rawBuilder)
            : base(rawBuilder)
        {
        }

    }
}
