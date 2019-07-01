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

namespace Librame.Extensions.Core
{
    using AspNetCore;

    /// <summary>
    /// 应用程序构建器静态扩展。
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 注册应用程序集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <returns>返回 <see cref="IBuilder"/>。</returns>
        public static IBuilder AddApplications(this IBuilder builder)
        {
            // Add ApplicationLocalization
            builder.Services.AddSingleton<IApplicationLocalization, InternalApplicationLocalization>();

            // Add ApplicationPrincipal
            builder.Services.AddSingleton<IApplicationPrincipal, InternalApplicationPrincipal>();

            return builder;
        }

    }
}
