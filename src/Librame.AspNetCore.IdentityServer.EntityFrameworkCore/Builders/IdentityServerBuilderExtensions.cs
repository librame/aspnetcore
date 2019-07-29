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
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 身份服务器构建器静态扩展。
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="setupAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="rawBuilderAction">给定的身份核心选项配置动作（可选）。</param>
        /// <param name="identityServerStoresAction">给定的身份服务器存储配置动作（可选）。</param>
        /// <param name="persistedGrantStoresAction">给定的持久化授予存储配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Action<IdentityServerBuilderOptions> setupAction = null,
            Action<IdentityServerOptions> rawBuilderAction = null,
            Action<ConfigurationStoreOptions> identityServerStoresAction = null,
            Action<OperationalStoreOptions> persistedGrantStoresAction = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            return builder.AddIdentityServer<TIdentityServerAccessor, TPersistedGrantAccessor, TUser>((b, r) =>
            {
                return new InternalIdentityServerBuilderWrapper(b, r);
            },
            setupAction, rawBuilderAction, identityServerStoresAction, persistedGrantStoresAction);
        }

        /// <summary>
        /// 添加身份扩展。
        /// </summary>
        /// <typeparam name="TIdentityServerAccessor">指定的身份服务器访问器类型。</typeparam>
        /// <typeparam name="TPersistedGrantAccessor">指定的持久化授予访问器类型。</typeparam>
        /// <typeparam name="TUser">指定的用户类型。</typeparam>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="createFactory">给定创建身份构建器的工厂方法。</param>
        /// <param name="setupAction">给定的封装器选项配置动作（可选）。</param>
        /// <param name="rawBuilderAction">给定的原始构建器选项配置动作（可选）。</param>
        /// <param name="identityServerStoresAction">给定的身份服务器存储配置动作（可选）。</param>
        /// <param name="persistedGrantStoresAction">给定的持久化授予存储配置动作（可选）。</param>
        /// <returns>返回 <see cref="IIdentityServerBuilderWrapper"/>。</returns>
        public static IIdentityServerBuilderWrapper AddIdentityServer<TIdentityServerAccessor, TPersistedGrantAccessor, TUser>(this IExtensionBuilder builder,
            Func<IExtensionBuilder, IIdentityServerBuilder, IIdentityServerBuilderWrapper> createFactory,
            Action<IdentityServerBuilderOptions> setupAction = null,
            Action<IdentityServerOptions> rawBuilderAction = null,
            Action<ConfigurationStoreOptions> identityServerStoresAction = null,
            Action<OperationalStoreOptions> persistedGrantStoresAction = null)
            where TIdentityServerAccessor : DbContext, IIdentityServerDbContextAccessor
            where TPersistedGrantAccessor : DbContext, IPersistedGrantDbContextAccessor
            where TUser : class
        {
            createFactory.NotNull(nameof(createFactory));

            builder.Services.OnlyConfigure(setupAction);

            var tablePrefix = nameof(IdentityServer);

            Action<ConfigurationStoreOptions> _identityServerStoresAction = options =>
            {
                // Client
                options.Client = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<Client>());
                options.ClientClaim = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientClaim>());
                options.ClientCorsOrigin = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientCorsOrigin>());
                options.ClientGrantType = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientGrantType>());
                options.ClientIdPRestriction = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientIdPRestriction>());
                options.ClientPostLogoutRedirectUri = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientPostLogoutRedirectUri>());
                options.ClientProperty = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientProperty>());
                options.ClientRedirectUri = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientRedirectUri>());
                options.ClientScopes = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientScope>());
                options.ClientSecret = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ClientSecret>());

                // Resource
                options.IdentityClaim = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<IdentityClaim>());
                options.IdentityResource = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<IdentityResource>());
                options.IdentityResourceProperty = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<IdentityResourceProperty>());
                options.ApiClaim = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ApiResourceClaim>());
                options.ApiResource = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ApiResource>());
                options.ApiResourceProperty = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ApiResourceProperty>());
                options.ApiScope = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ApiScope>());
                options.ApiScopeClaim = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ApiScopeClaim>());
                options.ApiSecret = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<ApiSecret>());

                identityServerStoresAction?.Invoke(options);
            };

            Action<OperationalStoreOptions> _persistedGrantStoresAction = options =>
            {
                options.PersistedGrants = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<PersistedGrant>());
                options.DeviceFlowCodes = new TableConfiguration(tablePrefix + TableSchema.GetEntityPluralName<DeviceFlowCodes>());

                persistedGrantStoresAction?.Invoke(options);
            };

            // Add IdentityServerBuilder
            var rawBuilder = builder.Services
                .AddIdentityServer(rawBuilderAction ?? (_ => { }))
                .AddConfigurationStore<TIdentityServerAccessor>(_identityServerStoresAction)
                .AddOperationalStore<TPersistedGrantAccessor>(_persistedGrantStoresAction)
                .AddAspNetIdentity<TUser>();

            // Add IIdentityServerBuilderWrapper
            var builderWrapper = createFactory.Invoke(builder, rawBuilder);

            return builderWrapper;
        }

    }
}
