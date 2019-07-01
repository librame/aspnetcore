#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.Portal
{
    using Extensions.Core;

    /// <summary>
    /// 内部门户构建器。
    /// </summary>
    internal class InternalPortalBuilder : AbstractBuilder<PortalBuilderOptions>, IPortalBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalPortalBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="PortalBuilderOptions"/>。</param>
        public InternalPortalBuilder(IBuilder builder, PortalBuilderOptions options)
            : base(builder, options)
        {
            Services.AddSingleton<IPortalBuilder>(this);
        }

    }
}
