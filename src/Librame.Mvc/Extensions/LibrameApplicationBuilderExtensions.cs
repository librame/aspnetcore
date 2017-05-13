#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Utility;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Librame 应用构建器静态扩展。
    /// </summary>
    public static class LibrameApplicationBuilderExtensions
    {
        /// <summary>
        /// 使用 Librame 应用。
        /// </summary>
        /// <param name="app">给定的应用构建器接口。</param>
        /// <returns>返回应用构建器接口。</returns>
        public static IApplicationBuilder UseLibrame(this IApplicationBuilder app)
        {
            return app.NotNull(nameof(app));
        }

    }
}
