#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.UI;
using Librame.Extensions;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 应用程序后置配置选项静态扩展。
    /// </summary>
    public static class ApplicationPostConfigureOptionsExtensions
    {

        /// <summary>
        /// 后置配置应用程序选项。
        /// </summary>
        /// <typeparam name="TConfigureOptions">指定的配置选项类型。</typeparam>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection PostConfigureApplicationOptions<TConfigureOptions>(this IServiceCollection services)
            where TConfigureOptions : class, IApplicationPostConfigureOptions
        {
            services.ConfigureOptions<TConfigureOptions>();

            return services;
        }

        /// <summary>
        /// 后置配置应用程序选项。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="configureOptionsType">给定的配置选项类型。</param>
        /// <returns>返回 <see cref="IServiceCollection"/>。</returns>
        public static IServiceCollection PostConfigureApplicationOptions(this IServiceCollection services, Type configureOptionsType)
        {
            typeof(IApplicationPostConfigureOptions).IsAssignableFromTargetType(configureOptionsType);

            services.ConfigureOptions(configureOptionsType);

            return services;
        }

    }
}
