#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using AspNetCore.Identity.Options;
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Options;
    using Extensions;
    using Extensions.Data.Accessors;
    using Extensions.Data.Stores;

    /// <summary>
    /// 抽象身份服务器存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TClient">指定的客户端类型。</typeparam>
    /// <typeparam name="TApiResource">指定的 API 资源类型。</typeparam>
    /// <typeparam name="TIdentityResource">指定的身份资源类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public abstract class AbstractIdentityServerStoreInitializer<TAccessor, TClient, TApiResource, TIdentityResource, TGenId, TIncremId, TCreatedBy>
        : AbstractIdentityStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IAccessor
        where TClient : class
        where TApiResource : class
        where TIdentityResource : class
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个抽象身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverInitializationOptions">给定的 <see cref="IdentityServerStoreInitializationOptions"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractIdentityServerStoreInitializer(IdentityServerStoreInitializationOptions serverInitializationOptions,
            IdentityStoreInitializationOptions initializationOptions,
            SignInManager<DefaultIdentityUser<TGenId, TCreatedBy>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId, TCreatedBy>> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(initializationOptions, signInManager, roleMananger,
                  identifierGenerator, validator, loggerFactory)
        {
            ServerInitializationOptions = serverInitializationOptions.NotNull(nameof(serverInitializationOptions));
        }


        /// <summary>
        /// 初始化选项。
        /// </summary>
        /// <value>返回 <see cref="IdentityStoreInitializationOptions"/>。</value>
        protected IdentityServerStoreInitializationOptions ServerInitializationOptions { get; }


        /// <summary>
        /// 获取客户端标识。
        /// </summary>
        /// <param name="client">给定的 <typeparamref name="TClient"/>。</param>
        /// <returns>返回字符串。</returns>
        protected abstract string GetClientId(TClient client);

        /// <summary>
        /// 获取 API 资源名称。
        /// </summary>
        /// <param name="apiResource">给定的 <typeparamref name="TApiResource"/>。</param>
        /// <returns>返回字符串。</returns>
        protected abstract string GetApiResourceName(TApiResource apiResource);

        /// <summary>
        /// 获取身份资源名称。
        /// </summary>
        /// <param name="identityResource">给定的 <typeparamref name="TIdentityResource"/>。</param>
        /// <returns>返回字符串。</returns>
        protected abstract string GetIdentityResourceName(TIdentityResource identityResource);


        /// <summary>
        /// 转换为客户端实体。
        /// </summary>
        /// <param name="client">给定的 <see cref="Client"/>。</param>
        /// <returns>返回 <typeparamref name="TClient"/>。</returns>
        protected abstract TClient ToClientEntity(Client client);

        /// <summary>
        /// 转换为 API 资源实体。
        /// </summary>
        /// <param name="apiResource">给定的 <see cref="ApiResource"/>。</param>
        /// <returns>返回 <typeparamref name="TApiResource"/>。</returns>
        protected abstract TApiResource ToApiResourceEntity(ApiResource apiResource);

        /// <summary>
        /// 转换为身份资源实体。
        /// </summary>
        /// <param name="identityResource">给定的 <see cref="IdentityResource"/>。</param>
        /// <returns>返回 <typeparamref name="TIdentityResource"/>。</returns>
        protected abstract TIdentityResource ToIdentityResourceEntity(IdentityResource identityResource);


        /// <summary>
        /// 初始化核心。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub"/>。</param>
        protected override void InitializeCore(IStoreHub stores)
        {
            base.InitializeCore(stores);

            if (stores is IIdentityServerStoreHub<TClient, TApiResource, TIdentityResource> identityServerStores)
            {
                InitializeClients(identityServerStores);

                InitializeApiResources(identityServerStores);

                InitializeIdentityResources(identityServerStores);
            }
        }

        /// <summary>
        /// 初始化客户端。
        /// </summary>
        /// <param name="identityServerStores">给定的身份服务器存储中心。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeClients(IIdentityServerStoreHub<TClient, TApiResource, TIdentityResource> identityServerStores)
        {
            foreach (var model in ServerInitializationOptions.DefaultClients)
            {
                if (!TryGetClient(model.ClientId, out var client))
                {
                    client = ToClientEntity(model);

                    identityServerStores.TryCreate(client);

                    RequiredSaveChanges = true;
                }
            }

            // TryGetClient
            bool TryGetClient(string clientId, out TClient client)
            {
                client = identityServerStores.Clients.FirstOrDefault(p => GetClientId(p) == clientId);
                return client.IsNotNull();
            }
        }

        /// <summary>
        /// 初始化 API 资源。
        /// </summary>
        /// <param name="identityServerStores">给定的身份服务器存储中心。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeApiResources(IIdentityServerStoreHub<TClient, TApiResource, TIdentityResource> identityServerStores)
        {
            foreach (var model in ServerInitializationOptions.DefaultApiResources)
            {
                if (!TryGetApiResource(model.Name, out var apiResource))
                {
                    apiResource = ToApiResourceEntity(model);

                    identityServerStores.TryCreate(apiResource);

                    RequiredSaveChanges = true;
                }
            }

            // TryGetApiResource
            bool TryGetApiResource(string name, out TApiResource apiResource)
            {
                apiResource = identityServerStores.ApiResources.FirstOrDefault(p => GetApiResourceName(p) == name);
                return apiResource.IsNotNull();
            }
        }

        /// <summary>
        /// 初始化身份资源。
        /// </summary>
        /// <param name="identityServerStores">给定的身份服务器存储中心。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeIdentityResources(IIdentityServerStoreHub<TClient, TApiResource, TIdentityResource> identityServerStores)
        {
            foreach (var model in ServerInitializationOptions.DefaultIdentityResources)
            {
                if (!TryGetIdentityResource(model.Name, out var identityResource))
                {
                    identityResource = ToIdentityResourceEntity(model);

                    identityServerStores.TryCreate(identityResource);

                    RequiredSaveChanges = true;
                }
            }

            // TryGetIdentityResource
            bool TryGetIdentityResource(string name, out TIdentityResource identityResource)
            {
                identityResource = identityServerStores.IdentityResources.FirstOrDefault(p => GetIdentityResourceName(p) == name);
                return identityResource.IsNotNull();
            }
        }

    }
}
