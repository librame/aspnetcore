#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

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
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependencySetupAction">给定的封装器选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerBuilderDependencyOptions> dependencySetupAction = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            return builder.AddIdentityServer<TIdentityServerAccessor, TPersistedGrantAccessor, TUser>((b, r) =>
            {
                return new IdentityServerBuilderWrapper(typeof(TUser), b, r);
            },
            dependencySetupAction);
        }

        /// <summary>
        /// 添加身份服务器扩展。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建身份构建器的工厂方法。</param>
        /// <param name="dependencySetupAction">给定的封装器选项配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IIdentityServerBuilder, IIdentityServerBuilderWrapper> createFactory,
            Action<IdentityServerBuilderDependencyOptions> dependencySetupAction = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            createFactory.NotNull(nameof(createFactory));

            // Add Dependencies
            var dependencyOptions = dependencySetupAction.ConfigureDependencyOptions();

            var rawBuilder = builder.Services
                .AddIdentityServer(dependencyOptions.BaseSetupAction)
                .AddConfigurationStore<TIdentityServerAccessor>(dependencyOptions.ConfigurationSetupAction)
                .AddOperationalStore<TPersistedGrantAccessor>(dependencyOptions.OperationalSetupAction)
                .AddAspNetIdentity<TUser>();

            // Add Builder
            builder.Services.OnlyConfigure(dependencySetupAction);

            var builderWrapper = createFactory.Invoke(builder, rawBuilder);

            return builderWrapper;
        }

    }
}
