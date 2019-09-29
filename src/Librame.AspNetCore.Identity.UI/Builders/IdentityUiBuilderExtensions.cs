#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions.Core;

    /// <summary>
    /// 身份 UI 构建器静态扩展。
    /// </summary>
    public static class IdentityUiBuilderExtensions
    {
        /// <summary>
        /// 添加身份 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilderWrapper"/>。</param>
        /// <param name="builderAction">给定的选项配置动作。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddIdentityUI(this IIdentityBuilderWrapper builder,
            Action<UiBuilderOptions> builderAction,
            Func<IExtensionBuilder, UiBuilderDependencyOptions, IUiBuilder> builderFactory = null)
        {
            return builder.AddUI(builderAction, builderFactory)
                .AddUser(builder.RawBuilder.UserType);
        }

        /// <summary>
        /// 添加身份 UI 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IIdentityBuilderWrapper"/>。</param>
        /// <param name="dependencyAction">给定的选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建数据构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IUiBuilder"/>。</returns>
        public static IUiBuilder AddIdentityUI(this IIdentityBuilderWrapper builder,
            Action<UiBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, UiBuilderDependencyOptions, IUiBuilder> builderFactory = null)
        {
            return builder.AddUI(dependencyAction, builderFactory)
                .AddUser(builder.RawBuilder.UserType);
        }

    }
}
