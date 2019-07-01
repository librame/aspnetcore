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
    /// 内部应用程序构建器包装。
    /// </summary>
    internal class InternalApplicationBuilderWrapper : IApplicationBuilderWrapper
    {
        /// <summary>
        /// 构造一个 <see cref="InternalApplicationBuilderWrapper"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        public InternalApplicationBuilderWrapper(IApplicationBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// 应用构建器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IApplicationBuilder"/>。
        /// </value>
        public IApplicationBuilder Builder { get; }
    }
}
