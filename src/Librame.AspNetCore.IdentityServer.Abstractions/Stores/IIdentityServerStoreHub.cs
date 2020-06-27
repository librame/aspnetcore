#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Linq;

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using Extensions.Data;
    using Extensions.Data.Stores;

    /// <summary>
    /// 身份服务器存储中心接口。
    /// </summary>
    /// <typeparam name="TClient">指定的客户端类型。</typeparam>
    /// <typeparam name="TApiResource">指定的 API 资源类型。</typeparam>
    /// <typeparam name="TIdentityResource">指定的身份资源类型。</typeparam>
    public interface IIdentityServerStoreHub<TClient, TApiResource, TIdentityResource> : IStoreHub
        where TClient : class
        where TApiResource : class
        where TIdentityResource : class
    {
        /// <summary>
        /// 客户端查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TClient}"/>。</value>
        IQueryable<TClient> Clients { get; }

        /// <summary>
        /// API 资源查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TApiResource}"/>。</value>
        IQueryable<TApiResource> ApiResources { get; }

        /// <summary>
        /// 身份资源查询。
        /// </summary>
        /// <value>返回 <see cref="IQueryable{TIdentityResource}"/>。</value>
        IQueryable<TIdentityResource> IdentityResources { get; }


        /// <summary>
        /// 尝试创建客户端集合。
        /// </summary>
        /// <param name="clients">给定的 <typeparamref name="TClient"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TClient[] clients);

        /// <summary>
        /// 尝试创建 API 资源集合。
        /// </summary>
        /// <param name="apiResources">给定的 <typeparamref name="TApiResource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TApiResource[] apiResources);

        /// <summary>
        /// 尝试创建身份资源集合。
        /// </summary>
        /// <param name="identityResources">给定的 <typeparamref name="TIdentityResource"/> 数组。</param>
        /// <returns>返回 <see cref="OperationResult"/>。</returns>
        OperationResult TryCreate(params TIdentityResource[] identityResources);
    }
}
