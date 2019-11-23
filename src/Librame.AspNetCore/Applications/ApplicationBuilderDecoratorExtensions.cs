#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// <see cref="IApplicationBuilder"/> 静态扩展。
    /// </summary>
    public static class ApplicationBuilderDecoratorExtensions
    {
        /// <summary>
        /// 使用 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IApplicationBuilder"/>。</param>
        /// <returns>返回 <see cref="IApplicationBuilderDecorator"/>。</returns>
        public static IApplicationBuilderDecorator UseLibrameCore(this IApplicationBuilder builder)
        {
            var decorator = new ApplicationBuilderDecorator(builder);

            return decorator.UseLocalization();
        }

    }
}
