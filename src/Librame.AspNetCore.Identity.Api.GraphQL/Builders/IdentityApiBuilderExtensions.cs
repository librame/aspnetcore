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

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;
    using Extensions.Core;

    /// <summary>
    /// 身份 API 构建器静态扩展。
    /// </summary>
    public static class IdentityApiBuilderExtensions
    {
        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="builderAction">给定的选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi(this IExtensionBuilder builder,
            Action<ApiBuilderOptions> builderAction = null,
            Func<IExtensionBuilder, ApiBuilderDependencyOptions, IApiBuilder> builderFactory = null)
        {
            return builder.AddApi(builderAction, builderFactory)
                .AddIdentityApiCore();
        }

        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi(this IExtensionBuilder builder,
            Action<ApiBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, ApiBuilderDependencyOptions, IApiBuilder> builderFactory = null)
        {
            return builder.AddApi(dependencyAction, builderFactory)
                .AddIdentityApiCore();
        }

        private static IApiBuilder AddIdentityApiCore(this IApiBuilder builder)
        {
            builder.Services.TryReplace<IGraphApiMutation, IdentityGraphApiMutation>();
            builder.Services.TryReplace<IGraphApiQuery, IdentityGraphApiQuery>();

            return builder;
        }

    }
}
