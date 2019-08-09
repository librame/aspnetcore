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
using Librame.Extensions.Core;
using Microsoft.AspNetCore.Http;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ASP.NET 核心构建器服务集合静态扩展。
    /// </summary>
    public static class AspNetCoreBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 注册 Librame for ASP.NET Core 服务集合。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="dependencySetupAction">给定的依赖选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<CoreBuilderDependencyOptions> dependencySetupAction = null)
        {
            var builder = services.AddLibrame(dependencySetupAction);

            return builder.AddAspNetCore();
        }

        /// <summary>
        /// 注册 Librame for ASP.NET Core 服务集合。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="createFactory">给定创建核心构建器的工厂方法。</param>
        /// <param name="dependencySetupAction">给定的依赖选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Func<IServiceCollection, ICoreBuilder> createFactory,
            Action<CoreBuilderDependencyOptions> dependencySetupAction = null)
        {
            var builder = services.AddLibrame(createFactory, dependencySetupAction);

            return builder.AddAspNetCore();
        }

        private static ICoreBuilder AddAspNetCore(this ICoreBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return builder
                .AddLocalizations();
        }

    }
}
