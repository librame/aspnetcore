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
        /// <param name="rawBuilder">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderWrapper"/>。</returns>
        public static IApplicationBuilderWrapper UseLibrameCore(this IApplicationBuilder rawBuilder)
        {
            var builderWrapper = new ApplicationBuilderWrapper(rawBuilder);

            return builderWrapper.UseLocalization();
        }

    }
}
