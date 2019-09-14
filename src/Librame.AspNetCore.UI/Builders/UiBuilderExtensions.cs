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

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

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
            Func<IExtensionBuilder, IUiBuilder> builderFactory = null)
        {
            builderAction.NotNull(nameof(builderAction));

            return builder.AddUI(dependency =>
            {
                dependency.BuilderOptionsAction = builderAction;
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
            Func<IExtensionBuilder, IUiBuilder> builderFactory = null)
        {
            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependencyOptions();

            // Add Builder
            builder.Services.OnlyConfigure(dependency.BuilderOptionsAction,
                dependency.BuilderOptionsName);

            var uiBuilder = builderFactory.NotNullOrDefault(()
                => b => new UiBuilder(b)).Invoke(builder);

            return uiBuilder
                .AddApplications()
                .AddDataAnnotations()
                .AddLocalizations();
        }

    }
}
