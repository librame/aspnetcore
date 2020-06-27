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
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using AspNetCore.IdentityServer.Builders;
    using Extensions.Data.Stores;

    /// <summary>
    /// <see cref="Guid"/> 身份服务器存储初始化器。
    /// </summary>
    public class GuidIdentityServerStoreInitializer : GuidIdentityServerStoreInitializer<IdentityServerDbContextAccessor>
    {
        /// <summary>
        /// 构造一个身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverOptions">给定的 <see cref="IOptions{IdentityServerBuilderOptions}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityServerStoreInitializer(IOptions<IdentityServerBuilderOptions> serverOptions,
            IOptions<IdentityBuilderOptions> options,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(serverOptions, options, signInManager, roleMananger,
                  identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// <see cref="Guid"/> 身份服务器存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class GuidIdentityServerStoreInitializer<TAccessor> : GuidIdentityServerStoreInitializer<TAccessor, Guid, int, Guid>
        where TAccessor : IdentityServerDbContextAccessor
    {
        /// <summary>
        /// 构造一个身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverOptions">给定的 <see cref="IOptions{IdentityServerBuilderOptions}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityServerStoreInitializer(IOptions<IdentityServerBuilderOptions> serverOptions,
            IOptions<IdentityBuilderOptions> options,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(serverOptions, options, signInManager, roleMananger,
                  identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// <see cref="Guid"/> 身份服务器存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class GuidIdentityServerStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        : AbstractIdentityServerStoreInitializer<TAccessor,
            Client, ApiResource, IdentityResource,
            TGenId, TIncremId, TCreatedBy>
        where TAccessor : IdentityServerDbContextAccessor<TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份服务器存储初始化器。
        /// </summary>
        /// <param name="serverOptions">给定的 <see cref="IOptions{IdentityServerBuilderOptions}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityServerStoreInitializer(IOptions<IdentityServerBuilderOptions> serverOptions,
            IOptions<IdentityBuilderOptions> options,
            SignInManager<DefaultIdentityUser<TGenId, TCreatedBy>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId, TCreatedBy>> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(serverOptions?.Value.Stores.Initialization,
                  options?.Value.Stores.Initialization, signInManager, roleMananger,
                  identifierGenerator, validator, loggerFactory)
        {
        }


        /// <summary>
        /// 获取客户端标识。
        /// </summary>
        /// <param name="client">给定的 <see cref="Client"/>。</param>
        /// <returns>返回字符串。</returns>
        protected override string GetClientId(Client client)
            => client?.ClientId;

        /// <summary>
        /// 获取 API 资源名称。
        /// </summary>
        /// <param name="apiResource">给定的 <see cref="ApiResource"/>。</param>
        /// <returns>返回字符串。</returns>
        protected override string GetApiResourceName(ApiResource apiResource)
            => apiResource?.Name;

        /// <summary>
        /// 获取身份资源名称。
        /// </summary>
        /// <param name="identityResource">给定的 <see cref="IdentityResource"/>。</param>
        /// <returns>返回字符串。</returns>
        protected override string GetIdentityResourceName(IdentityResource identityResource)
            => identityResource?.Name;


        /// <summary>
        /// 转换为客户端实体。
        /// </summary>
        /// <param name="client">给定的 <see cref="IdentityServer4.Models.Client"/>。</param>
        /// <returns>返回 <see cref="Client"/>。</returns>
        protected override Client ToClientEntity(IdentityServer4.Models.Client client)
            => client.ToEntity();

        /// <summary>
        /// 转换为 API 资源实体。
        /// </summary>
        /// <param name="apiResource">给定的 <see cref="IdentityServer4.Models.ApiResource"/>。</param>
        /// <returns>返回 <see cref="ApiResource"/>。</returns>
        protected override ApiResource ToApiResourceEntity(IdentityServer4.Models.ApiResource apiResource)
            => apiResource.ToEntity();

        /// <summary>
        /// 转换为身份资源实体。
        /// </summary>
        /// <param name="identityResource">给定的 <see cref="IdentityServer4.Models.IdentityResource"/>。</param>
        /// <returns>返回 <see cref="IdentityResource"/>。</returns>
        protected override IdentityResource ToIdentityResourceEntity(IdentityServer4.Models.IdentityResource identityResource)
            => identityResource.ToEntity();

    }
}
