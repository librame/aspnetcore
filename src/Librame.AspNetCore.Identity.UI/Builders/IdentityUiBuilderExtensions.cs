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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity
{
    using AspNetCore.UI;
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 身份 UI 构建器静态扩展。
    /// </summary>
    public static class IdentityUiBuilderExtensions
    {
        /// <summary>
        /// 添加身份 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilderDecorator"/>。</param>
        /// <param name="builderAction">给定的选项配置动作。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IUiBuilder AddIdentityUI(this IIdentityBuilderDecorator builder,
            Action<UiBuilderOptions> builderAction,
            Func<IExtensionBuilder, UiBuilderDependencyOptions, IUiBuilder> builderFactory = null)
        {
            builder.NotNull(nameof(builder));

            return builder.AddUI(builderAction, builderFactory)
                .AddUser(builder.Source.UserType);
        }

        /// <summary>
        /// 添加身份 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilderDecorator"/>。</param>
        /// <param name="dependencyAction">给定的选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "builder")]
        public static IUiBuilder AddIdentityUI(this IIdentityBuilderDecorator builder,
            Action<UiBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, UiBuilderDependencyOptions, IUiBuilder> builderFactory = null)
        {
            builder.NotNull(nameof(builder));

            return builder.AddUI(dependencyAction, builderFactory)
                .AddUser(builder.Source.UserType);
        }

    }
}
