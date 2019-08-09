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
using System;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 身份服务器构建器依赖选项。
    /// </summary>
    public class IdentityServerBuilderDependencyOptions : ExtensionBuilderDependencyOptions<IdentityServerBuilderOptions>
    {
        private static string _tablePrefix = nameof(IdentityServer);


        /// <summary>
        /// <see cref="IdentityServerOptions"/> 配置动作。
        /// </summary>
        public Action<IdentityServerOptions> BaseSetupAction { get; set; }
            = _ => { };

        /// <summary>
        /// <see cref="ConfigurationStoreOptions"/> 配置动作。
        /// </summary>
        public Action<ConfigurationStoreOptions> ConfigurationSetupAction { get; set; }
            = options =>
            {
                // Client
                options.Client = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<Client>());
                options.ClientClaim = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientClaim>());
                options.ClientCorsOrigin = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientCorsOrigin>());
                options.ClientGrantType = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientGrantType>());
                options.ClientIdPRestriction = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientIdPRestriction>());
                options.ClientPostLogoutRedirectUri = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientPostLogoutRedirectUri>());
                options.ClientProperty = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientProperty>());
                options.ClientRedirectUri = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientRedirectUri>());
                options.ClientScopes = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientScope>());
                options.ClientSecret = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ClientSecret>());

                // Resource
                options.IdentityClaim = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<IdentityClaim>());
                options.IdentityResource = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<IdentityResource>());
                options.IdentityResourceProperty = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<IdentityResourceProperty>());
                options.ApiClaim = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ApiResourceClaim>());
                options.ApiResource = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ApiResource>());
                options.ApiResourceProperty = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ApiResourceProperty>());
                options.ApiScope = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ApiScope>());
                options.ApiScopeClaim = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ApiScopeClaim>());
                options.ApiSecret = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<ApiSecret>());
            };

        /// <summary>
        /// <see cref="OperationalStoreOptions"/> 配置动作。
        /// </summary>
        public Action<OperationalStoreOptions> OperationalSetupAction { get; set; }
            = options =>
            {
                options.PersistedGrants = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<PersistedGrant>());
                options.DeviceFlowCodes = new TableConfiguration(_tablePrefix + TableSchema.GetEntityPluralName<DeviceFlowCodes>());
            };
    }
}
