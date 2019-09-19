#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 身份服务器构建器静态扩展。
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawAction">给定的封装器选项配置动作。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerOptions> rawAction,
            Func<IExtensionBuilder, IIdentityServerBuilder, IIdentityServerBuilderWrapper> builderFactory = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TUser : class
            => builder.AddIdentityServer<TIdentityServerAccessor, TIdentityServerAccessor, TUser>(rawAction, builderFactory);

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, IIdentityServerBuilder, IIdentityServerBuilderWrapper> builderFactory = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TUser : class
            => builder.AddIdentityServer<TIdentityServerAccessor, TIdentityServerAccessor, TUser>(dependencyAction, builderFactory);


        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TConfigurationAccessor">指定的配置访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawAction">给定的封装器选项配置动作。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TConfigurationAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerOptions> rawAction,
            Func<IExtensionBuilder, IIdentityServerBuilder, IIdentityServerBuilderWrapper> builderFactory = null)
            where TConfigurationAccessor : DbContext, IConfigurationDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            return builder.AddIdentityServer<TConfigurationAccessor, TPersistedGrantAccessor, TUser>(dependency =>
            {
                dependency.RawAction = rawAction;
            },
            builderFactory);
        }

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TConfigurationAccessor">指定的配置访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencyAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="builderFactory">给定创建身份构建器的工厂方法（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TConfigurationAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerBuilderDependencyOptions> dependencyAction = null,
            Func<IExtensionBuilder, IIdentityServerBuilder, IIdentityServerBuilderWrapper> builderFactory = null)
            where TConfigurationAccessor : DbContext, IConfigurationDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            // Add Dependencies
            var dependency = dependencyAction.ConfigureDependencyOptions();

            var rawBuilder = builder.Services
                .AddIdentityServer(dependency.RawAction)
                .AddConfigurationStore<TConfigurationAccessor>(dependency.ConfigurationAction)
                .AddOperationalStore<TPersistedGrantAccessor>(dependency.OperationalAction)
                .AddAspNetIdentity<TUser>();

            // Add Builder
            builder.Services.OnlyConfigure(dependency.BuilderOptionsAction,
                dependency.BuilderOptionsName);

            var builderWrapper = builderFactory.NotNullOrDefault(()
                => (b, r) => new IdentityServerBuilderWrapper(typeof(TUser), b, r)).Invoke(builder, rawBuilder);

            return builderWrapper;
        }

    }
}
