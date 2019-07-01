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

namespace Librame.AspNetCore.Library
{
    using Extensions.Core;

    /// <summary>
    /// 内部文库构建器。
    /// </summary>
    internal class InternalLibraryBuilder : AbstractBuilder<LibraryBuilderOptions>, ILibraryBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalLibraryBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="LibraryBuilderOptions"/>。</param>
        public InternalLibraryBuilder(IBuilder builder, LibraryBuilderOptions options)
            : base(builder, options)
        {
            Services.AddSingleton<ILibraryBuilder>(this);
        }

    }
}
