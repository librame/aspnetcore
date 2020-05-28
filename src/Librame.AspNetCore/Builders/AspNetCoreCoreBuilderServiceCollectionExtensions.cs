#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Builders;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// <see cref="CoreBuilder"/> for ASP.NET Core 服务集合静态扩展。
    /// </summary>
    public static class AspNetCoreCoreBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="configureLoggingBuilder">给定的配置日志构建器动作方法。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<ILoggingBuilder> configureLoggingBuilder,
            Func<IServiceCollection, AspNetCoreCoreBuilderDependency, ICoreBuilder> builderFactory = null)
        {
            configureLoggingBuilder.NotNull(nameof(configureLoggingBuilder));

            return services.AddLibrameCore(dependency =>
            {
                dependency.ConfigureLoggingBuilder = configureLoggingBuilder;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<AspNetCoreCoreBuilderDependency> dependencyAction = null,
            Func<IServiceCollection, AspNetCoreCoreBuilderDependency, ICoreBuilder> builderFactory = null)
            => services.AddLibrameCore<AspNetCoreCoreBuilderDependency>(dependencyAction, builderFactory);

        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <typeparam name="TDependency">指定的依赖类型。</typeparam>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore<TDependency>(this IServiceCollection services,
            Action<TDependency> configureDependency = null,
            Func<IServiceCollection, TDependency, ICoreBuilder> builderFactory = null)
            where TDependency : AspNetCoreCoreBuilderDependency, new()
        {
            AddAspNetCoreServiceCharacteristics();

            return services
                .AddLibrame(configureDependency, builderFactory)
                .AddAspNetCoreServices();
        }


        private static ICoreBuilder AddAspNetCoreServices(this ICoreBuilder builder)
        {
            builder.AddService<IHttpContextAccessor, HttpContextAccessor>();

            return builder;
        }

        private static void AddAspNetCoreServiceCharacteristics()
        {
            CoreBuilderServiceCharacteristicsRegistration.Register
                .TryAdd<IHttpContextAccessor>(ServiceCharacteristics.Singleton());
        }

    }
}
