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

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份服务器构建器依赖选项。
    /// </summary>
    public class IdentityServerBuilderDependency : AbstractExtensionBuilderDependency<IdentityServerBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerBuilderDependency"/>。
        /// </summary>
        public IdentityServerBuilderDependency()
            : base(nameof(IdentityServerBuilderDependency))
        {
        }


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
                options.Client = TableConfigurationHelper.Create<Client>();
                options.ClientClaim = TableConfigurationHelper.Create<ClientClaim>();
                options.ClientCorsOrigin = TableConfigurationHelper.Create<ClientCorsOrigin>();
                options.ClientGrantType = TableConfigurationHelper.Create<ClientGrantType>();
                options.ClientIdPRestriction = TableConfigurationHelper.Create<ClientIdPRestriction>();
                options.ClientPostLogoutRedirectUri = TableConfigurationHelper.Create<ClientPostLogoutRedirectUri>();
                options.ClientProperty = TableConfigurationHelper.Create<ClientProperty>();
                options.ClientRedirectUri = TableConfigurationHelper.Create<ClientRedirectUri>();
                options.ClientScopes = TableConfigurationHelper.Create<ClientScope>();
                options.ClientSecret = TableConfigurationHelper.Create<ClientSecret>();

                // Resource
                options.IdentityClaim = TableConfigurationHelper.Create<IdentityClaim>();
                options.IdentityResource = TableConfigurationHelper.Create<IdentityResource>();
                options.IdentityResourceProperty = TableConfigurationHelper.Create<IdentityResourceProperty>();
                options.ApiClaim = TableConfigurationHelper.Create<ApiResourceClaim>();
                options.ApiResource = TableConfigurationHelper.Create<ApiResource>();
                options.ApiResourceProperty = TableConfigurationHelper.Create<ApiResourceProperty>();
                options.ApiScope = TableConfigurationHelper.Create<ApiScope>();
                options.ApiScopeClaim = TableConfigurationHelper.Create<ApiScopeClaim>();
                options.ApiSecret = TableConfigurationHelper.Create<ApiSecret>();
            };

        /// <summary>
        /// <see cref="OperationalStoreOptions"/> 配置动作。
        /// </summary>
        public Action<OperationalStoreOptions> OperationalAction { get; set; }
            = options =>
            {
                options.PersistedGrants = TableConfigurationHelper.Create<PersistedGrant>();
                options.DeviceFlowCodes = TableConfigurationHelper.Create<DeviceFlowCodes>();
            };
    }
}
