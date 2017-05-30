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

namespace LibrameCore.Handlers
{
    using Utilities;
    
    /// <summary>
    /// 泛型处理程序接口。
    /// </summary>
    /// <typeparam name="TOptions">指定的选项类型。</typeparam>
    public interface IHander<TOptions>
        where TOptions : HandlerOptions
    {
        /// <summary>
        /// Librame 构建器。
        /// </summary>
        ILibrameBuilder Builder { get; }


        /// <summary>
        /// 选项。
        /// </summary>
        TOptions Options { get; }


        /// <summary>
        /// 开始处理。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        void OnHandling(IApplicationBuilder app);
    }


    /// <summary>
    /// 抽象泛型处理程序。
    /// </summary>
    /// <typeparam name="TOptions">指定的选项类型。</typeparam>
    public abstract class AbstractHander<TOptions> : IHander<TOptions>
        where TOptions : HandlerOptions
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
        /// 选项。
        /// </summary>
        public TOptions Options => Builder.GetService<TOptions>();


        /// <summary>
        /// 开始处理。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        public abstract void OnHandling(IApplicationBuilder app);
    }

}
