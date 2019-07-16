#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Configuration;
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
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{UIBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddUI<TAppContext, TAppPostConfigureOptions>(this IBuilder builder,
            Action<UIBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
            where TAppContext : class, IApplicationContext
            where TAppPostConfigureOptions : class, IApplicationPostConfigureOptionsBase
        {
            return builder.AddUICore(options =>
            {
                return new InternalUIBuilder(typeof(TAppContext),
                typeof(TAppPostConfigureOptions), builder, options);
            },
            configureOptions, configuration, configureBinderOptions);
        }

        /// <summary>
        /// 添加 UI 扩展（需确保构建器选项中的 <see cref="UIBuilderOptions.ApplicationContextType"/> 与 <see cref="UIBuilderOptions.ApplicationPostConfigureOptionsType"/> 已配置）。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="configureOptions">给定的 <see cref="Action{UIBuilderOptions}"/>（可选；高优先级）。</param>
        /// <param name="configuration">给定的 <see cref="IConfiguration"/>（可选；次优先级）。</param>
        /// <param name="configureBinderOptions">给定的配置绑定器选项动作（可选）。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public static IUIBuilder AddUI(this IBuilder builder,
            Action<UIBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
        {
            return builder.AddUICore(options =>
            {
                var applicationContextType = options.ApplicationContextType;
                var applicationPostConfigureOptionsType = options.ApplicationPostConfigureOptionsType;

                applicationContextType.AssignableToBase(typeof(IApplicationContext));
                applicationPostConfigureOptionsType.AssignableToBase(typeof(IApplicationPostConfigureOptionsBase));

                return new InternalUIBuilder(applicationContextType,
                    applicationPostConfigureOptionsType, builder, options);
            },
            configureOptions, configuration, configureBinderOptions);
        }

        private static IUIBuilder AddUICore(this IBuilder builder,
            Func<UIBuilderOptions, IUIBuilder> builderFactory,
            Action<UIBuilderOptions> configureOptions = null,
            IConfiguration configuration = null,
            Action<BinderOptions> configureBinderOptions = null)
        {
            var options = builder.Configure(configureOptions,
                configuration, configureBinderOptions);

            var applicationContextType = options.ApplicationContextType;
            var applicationPostConfigureOptionsType = options.ApplicationPostConfigureOptionsType;

            applicationContextType.AssignableToBase(typeof(IApplicationContext));
            applicationPostConfigureOptionsType.AssignableToBase(typeof(IApplicationPostConfigureOptionsBase));

            var uiBuilder = builderFactory.Invoke(options);

            return uiBuilder
                .AddApplications()
                .AddLocalizations()
                .AddThemepacks(options.Themepacks.DefaultInfo)
                .ResetDataAnnotations();
        }

    }
}
