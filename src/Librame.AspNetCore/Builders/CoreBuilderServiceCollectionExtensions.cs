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
using System;

namespace Librame.Builders
{
    /// <summary>
    /// 核心构建器服务集合静态扩展。
    /// </summary>
    public static class CoreBuilderServiceCollectionExtensions
    {

        /// <summary>
        /// 注册 Librame 核心服务集合。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="configureOptions">给定的配置依赖选项（可选）。</param>
        /// <returns>返回 <see cref="IBuilder"/>。</returns>
        public static IBuilder AddLibrameCore(this IServiceCollection services, Action<DependenciesOptions> configureOptions = null)
        {
            return services.AddLibrame(configureOptions);
        }

    }
}
