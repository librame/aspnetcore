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

    /// <summary>
    /// 身份服务器构建器依赖选项。
    /// </summary>
    public class IdentityServerBuilderDependencyOptions : ExtensionBuilderDependencyOptions<IdentityServerBuilderDependencyOptions, IdentityServerBuilderOptions>
    {
        /// <summary>
        /// <see cref="IdentityServerOptions"/> 配置动作。
        /// </summary>
        public Action<IdentityServerOptions> IdentityServer { get; set; }
            = _ => { };

        /// <summary>
        /// <see cref="ConfigurationStoreOptions"/> 配置动作。
        /// </summary>
        public Action<ConfigurationStoreOptions> ConfigurationAction { get; set; }
            = options =>
            {
                // Client
                options.Client = TableConfigurationUtility.CreateTableConfiguration<Client>();
                options.ClientClaim = TableConfigurationUtility.CreateTableConfiguration<ClientClaim>();
                options.ClientCorsOrigin = TableConfigurationUtility.CreateTableConfiguration<ClientCorsOrigin>();
                options.ClientGrantType = TableConfigurationUtility.CreateTableConfiguration<ClientGrantType>();
                options.ClientIdPRestriction = TableConfigurationUtility.CreateTableConfiguration<ClientIdPRestriction>();
                options.ClientPostLogoutRedirectUri = TableConfigurationUtility.CreateTableConfiguration<ClientPostLogoutRedirectUri>();
                options.ClientProperty = TableConfigurationUtility.CreateTableConfiguration<ClientProperty>();
                options.ClientRedirectUri = TableConfigurationUtility.CreateTableConfiguration<ClientRedirectUri>();
                options.ClientScopes = TableConfigurationUtility.CreateTableConfiguration<ClientScope>();
                options.ClientSecret = TableConfigurationUtility.CreateTableConfiguration<ClientSecret>();

                // Resource
                options.IdentityClaim = TableConfigurationUtility.CreateTableConfiguration<IdentityClaim>();
                options.IdentityResource = TableConfigurationUtility.CreateTableConfiguration<IdentityResource>();
                options.IdentityResourceProperty = TableConfigurationUtility.CreateTableConfiguration<IdentityResourceProperty>();
                options.ApiClaim = TableConfigurationUtility.CreateTableConfiguration<ApiResourceClaim>();
                options.ApiResource = TableConfigurationUtility.CreateTableConfiguration<ApiResource>();
                options.ApiResourceProperty = TableConfigurationUtility.CreateTableConfiguration<ApiResourceProperty>();
                options.ApiScope = TableConfigurationUtility.CreateTableConfiguration<ApiScope>();
                options.ApiScopeClaim = TableConfigurationUtility.CreateTableConfiguration<ApiScopeClaim>();
                options.ApiSecret = TableConfigurationUtility.CreateTableConfiguration<ApiSecret>();
            };

        /// <summary>
        /// <see cref="OperationalStoreOptions"/> 配置动作。
        /// </summary>
        public Action<OperationalStoreOptions> OperationalAction { get; set; }
            = options =>
            {
                options.PersistedGrants = TableConfigurationUtility.CreateTableConfiguration<PersistedGrant>();
                options.DeviceFlowCodes = TableConfigurationUtility.CreateTableConfiguration<DeviceFlowCodes>();
            };
    }
}
