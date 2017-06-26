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

namespace LibrameStandard.Handlers
{
    using Utilities;

    /// <summary>
    /// 抽象泛型处理程序。
    /// </summary>
    /// <typeparam name="THandlerSettings">指定的处理程序设置类型。</typeparam>
    public abstract class AbstractHander<THandlerSettings> : IHander<THandlerSettings>
        where THandlerSettings : HandlerSettings
    {
        /// <summary>
        /// 构建一个默认处理程序实例。
        /// </summary>
        /// <param name="builder">给定的 Librame 构建器接口。</param>
        public AbstractHander(ILibrameBuilder builder)
        {
            Builder = builder.NotNull(nameof(builder));
        }


        /// <summary>
        /// Librame 构建器。
        /// </summary>
        public ILibrameBuilder Builder { get; }
        
        /// <summary>
        /// 处理程序设置。
        /// </summary>
        public THandlerSettings Settings => Builder.GetService<THandlerSettings>();


        /// <summary>
        /// 开始处理。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        public abstract void OnHandling(IApplicationBuilder app);
    }
}
