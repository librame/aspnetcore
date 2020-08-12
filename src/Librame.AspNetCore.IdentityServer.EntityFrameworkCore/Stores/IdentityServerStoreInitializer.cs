#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using ClientEntity = IdentityServer4.EntityFramework.Entities.Client;
using ApiResourceEntity = IdentityServer4.EntityFramework.Entities.ApiResource;
using IdentityResourceEntity = IdentityServer4.EntityFramework.Entities.IdentityResource;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Options;
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using AspNetCore.IdentityServer.Options;
    using Extensions;
    using Extensions.Data.Accessors;
    using Extensions.Data.Stores;
    using Extensions.Data.Validators;

    /// <summary>
    /// 身份服务器存储初始化器。
    /// </summary>
    public class IdentityServerStoreInitializer
        : IdentityServerStoreInitializer<IdentityServerDbContextAccessor>
    {
        /// <summary>
        /// 构造一个身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverInitializationOptions">给定的 <see cref="IdentityServerStoreInitializationOptions"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityServerStoreInitializer
            (IdentityServerStoreInitializationOptions serverInitializationOptions,
            IdentityStoreInitializationOptions initializationOptions,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(serverInitializationOptions, initializationOptions, signInManager, roleMananger,
                  validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 身份服务器存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class IdentityServerStoreInitializer<TAccessor>
        : IdentityServerStoreInitializer<TAccessor, Guid, int, Guid>
        where TAccessor : class, IIdentityAccessor,
            IDataAccessor, IConfigurationAccessor
    {
        /// <summary>
        /// 构造一个身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverInitializationOptions">给定的 <see cref="IdentityServerStoreInitializationOptions"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityServerStoreInitializer
            (IdentityServerStoreInitializationOptions serverInitializationOptions,
            IdentityStoreInitializationOptions initializationOptions,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(serverInitializationOptions, initializationOptions, signInManager, roleMananger,
                  validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 身份服务器存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityServerStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        : IdentityServerStoreInitializer<TAccessor,
            ClientEntity, ApiResourceEntity, IdentityResourceEntity,
            TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IIdentityAccessor<TGenId, TIncremId, TCreatedBy>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>, IConfigurationAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverInitializationOptions">给定的 <see cref="IdentityServerStoreInitializationOptions"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityServerStoreInitializer
            (IdentityServerStoreInitializationOptions serverInitializationOptions,
            IdentityStoreInitializationOptions initializationOptions,
            SignInManager<DefaultIdentityUser<TGenId, TCreatedBy>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId, TCreatedBy>> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(serverInitializationOptions, initializationOptions, signInManager, roleMananger,
                  validator, generator, loggerFactory)
        {
        }


        /// <summary>
        /// 转换为客户端实体。
        /// </summary>
        /// <param name="client">给定的 <see cref="Client"/>。</param>
        /// <returns>返回 <see cref="IdentityServer4.EntityFramework.Entities.Client"/>。</returns>
        protected override ClientEntity ToClientEntity(Client client)
            => client.ToEntity();

        /// <summary>
        /// 转换为 API 资源实体。
        /// </summary>
        /// <param name="apiResource">给定的 <see cref="ApiResource"/>。</param>
        /// <returns>返回 <see cref="IdentityServer4.EntityFramework.Entities.Client"/>。</returns>
        protected override ApiResourceEntity ToApiResourceEntity(ApiResource apiResource)
            => apiResource.ToEntity();

        /// <summary>
        /// 转换为身份资源实体。
        /// </summary>
        /// <param name="identityResource">给定的 <see cref="IdentityResource"/>。</param>
        /// <returns>返回 <see cref="IdentityServer4.EntityFramework.Entities.Client"/>。</returns>
        protected override IdentityResourceEntity ToIdentityResourceEntity(IdentityResource identityResource)
            => identityResource.ToEntity();

    }


    /// <summary>
    /// 身份服务器存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TClient">指定的客户端类型。</typeparam>
    /// <typeparam name="TApiResource">指定的 API 资源类型。</typeparam>
    /// <typeparam name="TIdentityResource">指定的身份资源类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityServerStoreInitializer<TAccessor, TClient, TApiResource, TIdentityResource, TGenId, TIncremId, TCreatedBy>
        : IdentityStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IIdentityAccessor<TGenId, TIncremId, TCreatedBy>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>, IConfigurationAccessor
        where TClient : ClientEntity
        where TApiResource : ApiResourceEntity
        where TIdentityResource : IdentityResourceEntity
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverInitializationOptions">给定的 <see cref="IdentityServerStoreInitializationOptions"/>。</param>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityServerStoreInitializer
            (IdentityServerStoreInitializationOptions serverInitializationOptions,
            IdentityStoreInitializationOptions initializationOptions,
            SignInManager<DefaultIdentityUser<TGenId, TCreatedBy>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId, TCreatedBy>> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(initializationOptions, signInManager, roleMananger, validator, generator, loggerFactory)
        {
            ServerInitializationOptions = serverInitializationOptions
                .NotNull(nameof(serverInitializationOptions));
        }


        /// <summary>
        /// 初始化选项。
        /// </summary>
        /// <value>返回 <see cref="IdentityStoreInitializationOptions"/>。</value>
        protected IdentityServerStoreInitializationOptions ServerInitializationOptions { get; }


        /// <summary>
        /// 转换为客户端实体。
        /// </summary>
        /// <param name="client">给定的 <see cref="Client"/>。</param>
        /// <returns>返回 <typeparamref name="TClient"/>。</returns>
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        protected virtual TClient ToClientEntity(Client client)
            => throw new NotSupportedException("You need to override this method.");

        /// <summary>
        /// 转换为 API 资源实体。
        /// </summary>
        /// <param name="apiResource">给定的 <see cref="ApiResource"/>。</param>
        /// <returns>返回 <typeparamref name="TApiResource"/>。</returns>
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        protected virtual TApiResource ToApiResourceEntity(ApiResource apiResource)
            => throw new NotSupportedException("You need to override this method.");

        /// <summary>
        /// 转换为身份资源实体。
        /// </summary>
        /// <param name="identityResource">给定的 <see cref="IdentityResource"/>。</param>
        /// <returns>返回 <typeparamref name="TIdentityResource"/>。</returns>
        [SuppressMessage("Globalization", "CA1303:请不要将文本作为本地化参数传递", Justification = "<挂起>")]
        protected virtual TIdentityResource ToIdentityResourceEntity(IdentityResource identityResource)
            => throw new NotSupportedException("You need to override this method.");


        /// <summary>
        /// 初始化存储集合。
        /// </summary>
        protected override void InitializeStores()
        {
            base.InitializeStores();

            InitializeClients();

            InitializeApiResources();

            InitializeIdentityResources();
        }

        /// <summary>
        /// 异步初始化存储集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected override async Task InitializeStoresAsync(CancellationToken cancellationToken)
        {
            await base.InitializeStoresAsync(cancellationToken).ConfigureAwait();

            await InitializeClientsAsync(cancellationToken).ConfigureAwait();

            await InitializeApiResourcesAsync(cancellationToken).ConfigureAwait();

            await InitializeIdentityResourcesAsync(cancellationToken).ConfigureAwait();
        }


        /// <summary>
        /// 初始化客户端集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeClients()
        {
            Accessor.ClientsManager.TryAddRange(p => p.ClientId == ServerInitializationOptions.DefaultClients[0].ClientId,
                () =>
                {
                    return ServerInitializationOptions.DefaultClients.Select(s => s.ToEntity());
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化客户端集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual Task InitializeClientsAsync(CancellationToken cancellationToken)
            => cancellationToken.RunOrCancelAsync(InitializeClients);


        /// <summary>
        /// 初始化 API 资源集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeApiResources()
        {
            Accessor.ApiResourcesManager.TryAddRange(p => p.Name == ServerInitializationOptions.DefaultApiResources[0].Name,
                () =>
                {
                    return ServerInitializationOptions.DefaultApiResources.Select(s => s.ToEntity());
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化 API 资源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual Task InitializeApiResourcesAsync(CancellationToken cancellationToken)
            => cancellationToken.RunOrCancelAsync(InitializeApiResources);


        /// <summary>
        /// 初始化身份资源集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeIdentityResources()
        {
            Accessor.IdentityResourcesManager.TryAddRange(p => p.Name == ServerInitializationOptions.DefaultIdentityResources[0].Name,
                () =>
                {
                    return ServerInitializationOptions.DefaultIdentityResources.Select(s => s.ToEntity());
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化身份资源集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual Task InitializeIdentityResourcesAsync(CancellationToken cancellationToken)
            => cancellationToken.RunOrCancelAsync(InitializeIdentityResources);

    }
}
