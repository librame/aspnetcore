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

namespace Librame.AspNetCore.Gallery
{
    using Extensions.Core;

    /// <summary>
    /// 内部图库构建器。
    /// </summary>
    internal class InternalGalleryBuilder : AbstractBuilder<GalleryBuilderOptions>, IGalleryBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalGalleryBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="GalleryBuilderOptions"/>。</param>
        public InternalGalleryBuilder(IBuilder builder, GalleryBuilderOptions options)
            : base(builder, options)
        {
            Services.AddSingleton<IGalleryBuilder>(this);
        }

    }
}
