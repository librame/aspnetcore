#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Api;
using Librame.AspNetCore.Identity;
using Librame.AspNetCore.Identity.Api;
using Librame.Extensions.Core;
using Librame.Extensions.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 身份 API 构建器静态扩展。
    /// </summary>
    public static class IdentityApiBuilderExtensions
    {
        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="builderAction">给定的选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi<TAccessor, TUser>(this IExtensionBuilder builder,
            Action<ApiBuilderOptions> builderAction = null,
            Func<IExtensionBuilder, ApiBuilderDependencyOptions, IApiBuilder> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor
            where TUser : class, IId<string>
        {
            return builder.AddApi(builderAction, builderFactory)
                .AddIdentityApiCore<TAccessor, TUser>();
        }

        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的依赖选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi<TAccessor, TUser>(this IExtensionBuilder builder,
            Action<ApiBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, ApiBuilderDependencyOptions, IApiBuilder> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor
            where TUser : class, IId<string>
        {
            return builder.AddApi(dependencyAction, builderFactory)
                .AddIdentityApiCore<TAccessor, TUser>();
        }

        private static IApiBuilder AddIdentityApiCore<TAccessor, TUser>(this IApiBuilder builder)
            where TAccessor : DbContext, IIdentityDbContextAccessor
            where TUser : class, IId<string>
        {
            builder.Services.TryReplace<IGraphApiQuery, IdentityGraphApiQuery<TAccessor>>();
            builder.Services.TryReplace<IGraphApiMutation, IdentityGraphApiMutation<TUser>>();

            return builder;
        }

    }
}
