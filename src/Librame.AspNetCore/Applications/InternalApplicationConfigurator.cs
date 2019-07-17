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
    using Extensions;

    /// <summary>
    /// 内部应用程序配置器。
    /// </summary>
    internal class InternalApplicationConfigurator : IApplicationConfigurator
    {
        /// <summary>
        /// 构造一个 <see cref="InternalApplicationConfigurator"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        public InternalApplicationConfigurator(IApplicationBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// 应用程序构建器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationBuilder"/>。
        /// </value>
        public IApplicationBuilder Builder { get; }
    }
}
