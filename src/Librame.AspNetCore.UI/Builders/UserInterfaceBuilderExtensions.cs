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
    /// 用户界面构建器静态扩展。
    /// </summary>
    public static class UserInterfaceBuilderExtensions
    {
        /// <summary>
        /// 添加用户界面扩展。
        /// </summary>
        /// <typeparam name="TAppPostConfigureOptions">指定的应用后置配置选项类型（推荐从 <see cref="UserInterfaceApplicationPostConfigureOptionsBase"/> 派生）。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        public static IUserInterfaceBuilder AddUserInterface<TAppPostConfigureOptions>(this IExtensionBuilder builder,
            Action<UserInterfaceBuilderOptions> setupAction = null)
            where TAppPostConfigureOptions : class, IUserInterfaceApplicationPostConfigureOptions
        {
            return builder.AddUserInterface(typeof(TAppPostConfigureOptions), setupAction);
        }

        /// <summary>
        /// 添加用户界面扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="applicationPostConfigureOptionsType">给定的应用后置配置选项类型（推荐从 <see cref="UserInterfaceApplicationPostConfigureOptionsBase"/> 派生）。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        public static IUserInterfaceBuilder AddUserInterface(this IExtensionBuilder builder,
            Type applicationPostConfigureOptionsType,
            Action<UserInterfaceBuilderOptions> setupAction = null)
        {
            return builder.AddUserInterface(b =>
            {
                return new InternalUserInterfaceBuilder(applicationPostConfigureOptionsType, b);
            },
            setupAction);
        }

        /// <summary>
        /// 添加用户界面扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建用户界面构建器的工厂方法。</param>
        /// <param name="setupAction">给定的选项配置动作。</param>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        public static IUserInterfaceBuilder AddUserInterface(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IUserInterfaceBuilder> createFactory,
            Action<UserInterfaceBuilderOptions> setupAction = null)
        {
            createFactory.NotNull(nameof(createFactory));

            builder.Services.OnlyConfigure(setupAction);

            var userInterfaceBuilder = createFactory.Invoke(builder);

            userInterfaceBuilder.ApplicationPostConfigureOptionsType
                .AssignableToBase(typeof(IUserInterfaceApplicationPostConfigureOptions));

            return userInterfaceBuilder
                .AddApplications()
                .AddDataAnnotations()
                .AddLocalizations();
        }

    }
}
