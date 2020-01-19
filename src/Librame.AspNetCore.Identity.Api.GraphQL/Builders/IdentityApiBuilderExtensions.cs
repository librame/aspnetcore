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
using Librame.AspNetCore.Api.Builders;
using Librame.AspNetCore.Identity.Accessors;
using Librame.AspNetCore.Identity.Api;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Data.Stores;
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
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureOptions">给定的配置选项动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi<TAccessor, TUser>(this IExtensionBuilder parentBuilder,
            Action<ApiBuilderOptions> configureOptions = null,
            Func<IExtensionBuilder, ApiBuilderDependency, IApiBuilder> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor
            where TUser : class, IId<string>
            => parentBuilder.AddApi(configureOptions, builderFactory)
                .AddIdentityApiCore<TAccessor, TUser>();

        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi<TAccessor, TUser>(this IExtensionBuilder parentBuilder,
            Action<ApiBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, ApiBuilderDependency, IApiBuilder> builderFactory = null)
            where TAccessor : DbContext, IIdentityDbContextAccessor
            where TUser : class, IId<string>
            => parentBuilder.AddApi(configureDependency, builderFactory)
                .AddIdentityApiCore<TAccessor, TUser>();

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
