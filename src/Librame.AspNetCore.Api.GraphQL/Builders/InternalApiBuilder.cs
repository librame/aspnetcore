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

namespace Librame.AspNetCore.Api
{
    using Extensions.Core;

    /// <summary>
    /// 内部身份构建器。
    /// </summary>
    internal class InternalApiBuilder : AbstractBuilder<ApiBuilderOptions>, IApiBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalApiBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="ApiBuilderOptions"/>。</param>
        public InternalApiBuilder(IBuilder builder, ApiBuilderOptions options)
            : base(builder, options)
        {
            Services.AddSingleton<IApiBuilder>(this);
        }

    }
}
