#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;

namespace Librame.AspNetCore.IdentityServer.Accessors
{
    using Extensions.Data;
    using Extensions.Data.Accessors;

    /// <summary>
    /// 配置访问器接口。
    /// </summary>
    public interface IConfigurationAccessor : IConfigurationDbContext, IAccessor
    {
        /// <summary>
        /// 客户端数据集管理器。
        /// </summary>
        DbSetManager<Client> ClientsManager { get; }

        /// <summary>
        /// 客户端跨域数据集管理器。
        /// </summary>
        DbSetManager<ClientCorsOrigin> ClientCorsOriginsManager { get; }

        /// <summary>
        /// 身份资源数据集管理器。
        /// </summary>
        DbSetManager<IdentityResource> IdentityResourcesManager { get; }

        /// <summary>
        /// API 资源数据集管理器。
        /// </summary>
        DbSetManager<ApiResource> ApiResourcesManager { get; }

        /// <summary>
        /// API 范围数据集管理器。
        /// </summary>
        DbSetManager<ApiScope> ApiScopesManager { get; }
    }
}
