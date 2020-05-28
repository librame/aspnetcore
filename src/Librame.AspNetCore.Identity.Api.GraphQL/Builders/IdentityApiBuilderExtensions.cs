#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.AspNetCore.Api;
using Librame.AspNetCore.Api.Builders;
using Librame.AspNetCore.Identity.Api;
using Librame.AspNetCore.Identity.Builders;
using Librame.AspNetCore.Identity.Stores;
using Librame.Extensions.Core.Builders;
using Librame.Extensions.Core.Identifiers;
using Librame.Extensions.Data.Stores;
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
        /// <param name="decorator">给定的 <see cref="IIdentityBuilderDecorator"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi(this IIdentityBuilderDecorator decorator,
            Action<ApiBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, ApiBuilderDependency, IApiBuilder> builderFactory = null)
            => decorator.AddIdentityApi<DefaultIdentityRole<Guid>,
                DefaultIdentityUser<Guid>>(configureDependency, builderFactory);

        /// <summary>
        /// 添加 Identity API 扩展。
        /// </summary>
        /// <typeparam name="TRole">指定的角色类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="decorator">给定的 <see cref="IIdentityBuilderDecorator"/>。</param>
        /// <param name="configureDependency">给定的配置依赖动作方法（可选）。</param>
        /// <param name="builderFactory">给定创建 API 构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IApiBuilder"/>。</returns>
        public static IApiBuilder AddIdentityApi<TRole, TUser>(this IIdentityBuilderDecorator decorator,
            Action<ApiBuilderDependency> configureDependency = null,
            Func<IExtensionBuilder, ApiBuilderDependency, IApiBuilder> builderFactory = null)
            where TRole : class, IIdentifier, ICreatedTimeTicks
            where TUser : class, IIdentifier, ICreatedTimeTicks
            => decorator.AddApi(configureDependency, builderFactory)
                .AddIdentityApiCore<TRole, TUser>();


        private static IApiBuilder AddIdentityApiCore<TRole, TUser>(this IApiBuilder builder)
            where TRole : class, IIdentifier, ICreatedTimeTicks
            where TUser : class, IIdentifier, ICreatedTimeTicks
        {
            builder.Services.TryReplace<IGraphApiQuery, IdentityGraphApiQuery<TRole, TUser>>();
            builder.Services.TryReplace<IGraphApiMutation, IdentityGraphApiMutation<TUser>>();

            return builder;
        }

    }
}
