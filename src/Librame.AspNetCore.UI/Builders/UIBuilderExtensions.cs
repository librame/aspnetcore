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
    public static class UIBuilderExtensions
    {
        /// <summary>
        /// 添加 UI 扩展。
        /// </summary>
        /// <typeparam name="TAppContext">指定的应用程序上下文类型（推荐从 <see cref="AbstractApplicationContext"/> 派生）。</typeparam>
        /// <typeparam name="TAppPostConfigureOptions">指定的应用程序后置配置选项类型（推荐从 <see cref="ApplicationPostConfigureOptionsBase"/> 派生）。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="themepack">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddUI<TAppContext, TAppPostConfigureOptions>(this IExtensionBuilder builder,
            IThemepackInfo themepack, Action<UIBuilderOptions> setupAction = null)
            where TAppContext : class, IApplicationContext
            where TAppPostConfigureOptions : class, IApplicationPostConfigureOptionsBase
        {
            return builder.AddUI(typeof(TAppContext), typeof(TAppPostConfigureOptions),
                themepack, setupAction);
        }

        /// <summary>
        /// 添加 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="applicationContextType">给定的应用程序上下文类型（推荐从 <see cref="AbstractApplicationContext"/> 派生）。</param>
        /// <param name="applicationPostConfigureOptionsType">给定的应用程序后置配置选项类型（推荐从 <see cref="ApplicationPostConfigureOptionsBase"/> 派生）。</param>
        /// <param name="themepack">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddUI(this IExtensionBuilder builder,
            Type applicationContextType, Type applicationPostConfigureOptionsType,
            IThemepackInfo themepack, Action<UIBuilderOptions> setupAction = null)
        {
            return builder.AddUI(b =>
            {
                return new InternalUIBuilder(applicationContextType,
                    applicationPostConfigureOptionsType, themepack, b);
            },
            setupAction);
        }

        /// <summary>
        /// 添加 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建 UI 构建器的工厂方法。</param>
        /// <param name="setupAction">给定的选项配置动作。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddUI(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IUIBuilder> createFactory,
            Action<UIBuilderOptions> setupAction = null)
        {
            createFactory.NotNull(nameof(createFactory));

            builder.Services.OnlyConfigure(setupAction);

            var uiBuilder = createFactory.Invoke(builder);

            uiBuilder.ApplicationContextType.AssignableToBase(typeof(IApplicationContext));
            uiBuilder.ApplicationPostConfigureOptionsType
                .AssignableToBase(typeof(IApplicationPostConfigureOptionsBase));
            uiBuilder.Themepack.NotNull(nameof(uiBuilder.Themepack));

            return uiBuilder
                .AddApplications()
                .AddDataAnnotations()
                .AddLocalizations()
                .AddThemepacks();
        }

    }
}
