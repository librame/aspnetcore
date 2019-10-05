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
using Microsoft.AspNetCore.Builder;
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
        /// <param name="requestLocalizationAction">给定的请求本地化配置动作。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<RequestLocalizationOptions> requestLocalizationAction,
            Func<IServiceCollection, AspNetCoreCoreBuilderDependencyOptions, ICoreBuilder> builderFactory = null)
        {
            requestLocalizationAction.NotNull(nameof(requestLocalizationAction));

            return services.AddLibrameCore(dependency =>
            {
                dependency.RequestLocalization.Action = requestLocalizationAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="loggingAction">给定的日志构建器配置动作。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore(this IServiceCollection services,
            Action<ILoggingBuilder> loggingAction,
            Func<IServiceCollection, AspNetCoreCoreBuilderDependencyOptions, ICoreBuilder> builderFactory = null)
        {
            loggingAction.NotNull(nameof(loggingAction));

            return services.AddLibrameCore(dependency =>
            {
                dependency.LoggingAction = loggingAction;
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
            Func<IServiceCollection, AspNetCoreCoreBuilderDependencyOptions, ICoreBuilder> builderFactory = null)
        {
            builderAction.NotNull(nameof(builderAction));

            return services.AddLibrameCore(dependency =>
            {
                dependency.Builder.Action = builderAction;
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
            Action<AspNetCoreCoreBuilderDependencyOptions> dependencyAction = null,
            Func<IServiceCollection, AspNetCoreCoreBuilderDependencyOptions, ICoreBuilder> builderFactory = null)
            => services.AddLibrameCore<AspNetCoreCoreBuilderDependencyOptions>(dependencyAction, builderFactory);

        /// <summary>
        /// 添加 Librame for ASP.NET Core。
        /// </summary>
        /// <typeparam name="TDependencyOptions">指定的依赖类型。</typeparam>
        /// <param name="services">给定的 <see cref="IServiceCollection"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建核心构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="ICoreBuilder"/>。</returns>
        public static ICoreBuilder AddLibrameCore<TDependencyOptions>(this IServiceCollection services,
            Action<TDependencyOptions> dependencyAction = null,
            Func<IServiceCollection, TDependencyOptions, ICoreBuilder> builderFactory = null)
            where TDependencyOptions : AspNetCoreCoreBuilderDependencyOptions, new()
        {
            return services
                .AddLibrame(dependencyAction, builderFactory)
                .AddAspNetCore();
        }

        private static ICoreBuilder AddAspNetCore(this ICoreBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return builder
                .AddLocalizers();
        }

    }
}
