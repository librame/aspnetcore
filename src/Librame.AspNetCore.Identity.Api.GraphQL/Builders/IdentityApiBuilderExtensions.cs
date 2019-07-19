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
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi(this IExtensionBuilder builder,
            Action<ApiBuilderOptions> setupAction = null)
        {
            return builder.AddApi(setupAction)
                .AddIdentityApiCore();
        }

        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建 API 构建器的工厂方法。</param>
        /// <param name="setupAction">给定的选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IApiBuilder> createFactory,
            Action<ApiBuilderOptions> setupAction = null)
        {
            return builder.AddApi(createFactory, setupAction)
                .AddIdentityApiCore();
        }

        private static IApiBuilder AddIdentityApiCore(this IApiBuilder builder)
        {
            builder.Services.TryReplace<IGraphApiMutation, InternalIdentityGraphApiMutation>();
            builder.Services.TryReplace<IGraphApiQuery, InternalIdentityGraphApiQuery>();

            return builder;
        }

    }
}
