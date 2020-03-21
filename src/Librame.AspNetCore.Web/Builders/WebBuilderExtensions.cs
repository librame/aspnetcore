#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Web.Builders;
using Librame.Extensions;
using Librame.Extensions.Core.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Web 构建器静态扩展。
    /// </summary>
    public static class WebBuilderExtensions
    {
        /// <summary>
        /// 添加 Web 扩展。
        /// </summary>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 Web 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        public static IWebBuilder AddWeb(this IExtensionBuilder parentBuilder,
            Action<WebBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, WebBuilderDependency, IWebBuilder> builderFactory = null)
            => parentBuilder.AddWeb<WebBuilderDependency>(configureDependency, builderFactory);

        /// <summary>
        /// 添加 Web 扩展。
        /// </summary>
        /// <typeparam name="TDependencyOptions">指定的依赖类型。</typeparam>
        /// <param name="parentBuilder">给定的父级 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 Web 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IWebBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "parentBuilder")]
        public static IWebBuilder AddWeb<TDependencyOptions>(this IExtensionBuilder parentBuilder,
            Action<TDependencyOptions> configureDependency = null,
            Func<IExtensionBuilder, TDependencyOptions, IWebBuilder> builderFactory = null)
            where TDependencyOptions : WebBuilderDependency, new()
        {
            // 如果已经添加过 Web 扩展，则直接返回，防止出现重复配置的情况
            if (parentBuilder.TryGetParentBuilder(out IWebBuilder webBuilder))
                return webBuilder;

            // Configure Dependencies
            var dependency = configureDependency.ConfigureDependency(parentBuilder);

            // Create Builder
            webBuilder = builderFactory.NotNullOrDefault(()
                => (b, d) => new WebBuilder(b, d)).Invoke(parentBuilder, dependency);

            // Configure Builder
            return webBuilder
                .AddApplications()
                .AddDataAnnotations()
                .AddLocalizers()
                .AddProjects()
                .AddServices()
                .AddThemepacks();
        }

    }
}
