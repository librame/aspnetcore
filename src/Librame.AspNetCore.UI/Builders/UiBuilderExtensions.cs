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
using Librame.Extensions.Core;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// UI 构建器静态扩展。
    /// </summary>
    public static class UiBuilderExtensions
    {
        /// <summary>
        /// 添加 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="builderAction">给定的选项配置动作。</param>
        /// <param name="builderFactory">给定创建 UI 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddUI(this IExtensionBuilder builder,
            Action<UiBuilderOptions> builderAction,
            Func<IExtensionBuilder, UiBuilderDependencyOptions, IUiBuilder> builderFactory = null)
        {
            builderAction.NotNull(nameof(builderAction));

            return builder.AddUI(dependency =>
            {
                dependency.Builder.Action = builderAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 UI 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddUI(this IExtensionBuilder builder,
            Action<UiBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, UiBuilderDependencyOptions, IUiBuilder> builderFactory = null)
            => builder.AddUI<UiBuilderDependencyOptions>(dependencyAction, builderFactory);

        /// <summary>
        /// 添加 UI 扩展。
        /// </summary>
        /// <typeparam name="TDependencyOptions">指定的依赖类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 UI 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IUiBuilder AddUI<TDependencyOptions>(this IExtensionBuilder builder,
            Action<TDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, TDependencyOptions, IUiBuilder> builderFactory = null)
            where TDependencyOptions : UiBuilderDependencyOptions, new()
        {
            // 如果已经添加过 UI 扩展，则直接返回，防止出现重复配置的情况
            if (builder.TryGetParentBuilder(out IUiBuilder parentUiBuilder))
                return parentUiBuilder;

            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependency();
            builder.Services.AddAllOptionsConfigurators(dependency);

            // Create Builder
            var uiBuilder = builderFactory.NotNullOrDefault(()
                => (b, d) => new UiBuilder(b, d)).Invoke(builder, dependency);

            // Configure Builder
            return uiBuilder
                .AddApplications()
                .AddDataAnnotations()
                .AddLocalizers();
        }

    }
}
