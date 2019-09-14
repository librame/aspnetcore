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
using Librame.Extensions;
using Librame.Extensions.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// ASP.NET 核心构建器服务集合静态扩展。
    /// </summary>
    public static class AspNetCoreBuilderServiceCollectionExtensions
    {
        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="loggingAction">给定的日志构建器配置动作。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<ILoggingBuilder> loggingAction,
            Func<IServiceCollection, ICoreBuilder> builderFactory = null)
        {
            loggingAction.NotNull(nameof(loggingAction));

            return services.AddLibrameCore(dependency =>
            {
                dependency.LoggingBuilderAction = loggingAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="builderAction">给定的选项配置动作。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<CoreBuilderOptions> builderAction,
            Func<IServiceCollection, ICoreBuilder> builderFactory = null)
        {
            builderAction.NotNull(nameof(builderAction));

            return services.AddLibrameCore(dependency =>
            {
                dependency.BuilderOptionsAction = builderAction;
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
            Action<CoreBuilderDependencyOptions> dependencyAction = null,
            Func<IServiceCollection, ICoreBuilder> builderFactory = null)
        {
            var builder = services.AddLibrame(dependencyAction, builderFactory);

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
