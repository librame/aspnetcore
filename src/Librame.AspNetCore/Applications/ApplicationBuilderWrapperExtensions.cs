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
    /// <summary>
    /// <see cref="IApplicationBuilderWrapper"/> 静态扩展。
    /// </summary>
    public static class ApplicationBuilderWrapperExtensions
    {
        /// <summary>
        /// 使用 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseLibrameCore(this IApplicationBuilder builder)
        {
            var loader = builder.AsWrapper();

            return loader.UseLocalization();
        }

        /// <summary>
        /// 转换为应用程序构建器包装。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper AsWrapper(this IApplicationBuilder builder)
        {
            return new InternalApplicationBuilderWrapper(builder);
        }

    }
}
