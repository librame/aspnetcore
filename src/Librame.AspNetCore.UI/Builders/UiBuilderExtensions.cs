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
using System.Linq;
using System.Reflection;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 用户界面构建器静态扩展。
    /// </summary>
    public static class UiBuilderExtensions
    {
        /// <summary>
        /// 添加用户界面扩展。
        /// </summary>
        /// <typeparam name="TAppPostConfigureOptions">指定的应用后置配置选项类型（推荐从 <see cref="UiApplicationPostConfigureOptionsBase"/> 派生）。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddUI<TAppPostConfigureOptions>(this IExtensionBuilder builder,
            Action<UiBuilderOptions> setupAction = null)
            where TAppPostConfigureOptions : class, IUiApplicationPostConfigureOptions
        {
            return builder.AddUI(typeof(TAppPostConfigureOptions), setupAction);
        }

        /// <summary>
        /// 添加用户界面扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="applicationPostConfigureOptionsType">给定的应用后置配置选项类型（推荐从 <see cref="UiApplicationPostConfigureOptionsBase"/> 派生）。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddUI(this IExtensionBuilder builder,
            Type applicationPostConfigureOptionsType,
            Action<UiBuilderOptions> setupAction = null)
        {
            return builder.AddUI(b =>
            {
                return new UiBuilder(applicationPostConfigureOptionsType, b);
            },
            setupAction);
        }

        /// <summary>
        /// 添加用户界面扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建用户界面构建器的工厂方法。</param>
        /// <param name="setupAction">给定的选项配置动作。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddUI(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IUiBuilder> createFactory,
            Action<UiBuilderOptions> setupAction = null)
        {
            createFactory.NotNull(nameof(createFactory));

            builder.Services.OnlyConfigure(setupAction);

            var userInterfaceBuilder = createFactory.Invoke(builder);

            userInterfaceBuilder.ApplicationPostConfigureOptionsType
                .AssignableToBase(typeof(IUiApplicationPostConfigureOptions));

            return userInterfaceBuilder
                .AddApplications()
                .AddDataAnnotations()
                .AddLocalizations();
        }


        /// <summary>
        /// 添加控制器集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="razorAssembly">给定包含页面集合的程序集。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddControllers(this IUiBuilder builder,
            IMvcBuilder mvcBuilder, Assembly razorAssembly)
        {
            // Add Assemblies Pages（需引用 Microsoft.AspNetCore.Mvc 程序集才能正常被路由解析）
            var assemblies = ApplicationInfoUtility.Themepacks.Values.Select(info => info.Assembly)
                .Append(razorAssembly)
                .ToArray();

            mvcBuilder.AddRazorRelatedParts(assemblies);

            return builder.AddControllers();
        }

        /// <summary>
        /// 添加页面集合。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IUiBuilder"/>。</param>
        /// <param name="mvcBuilder">给定的 <see cref="IMvcBuilder"/>。</param>
        /// <param name="razorAssembly">给定包含页面集合的程序集。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddPages(this IUiBuilder builder,
            IMvcBuilder mvcBuilder, Assembly razorAssembly)
        {
            // Add Assemblies Pages（需引用 Microsoft.AspNetCore.Mvc 程序集才能正常被路由解析）
            var assemblies = ApplicationInfoUtility.Themepacks.Values.Select(info => info.Assembly)
                .Append(razorAssembly)
                .ToArray();

            mvcBuilder.AddRazorRelatedParts(assemblies);

            return builder;
        }

    }
}
