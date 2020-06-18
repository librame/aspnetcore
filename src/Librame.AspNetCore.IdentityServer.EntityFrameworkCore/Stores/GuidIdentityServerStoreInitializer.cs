#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using Extensions.Data.Stores;
    using Extensions.Data.ValueGenerators;
    using Identity.Stores;

    /// <summary>
    /// GUID 身份服务器存储初始化器。
    /// </summary>
    public class GuidIdentityServerStoreInitializer : GuidIdentityStoreInitializer
    {
        /// <summary>
        /// 构造一个 <see cref="GuidIdentityServerStoreInitializer"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{DefaultIdentityUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{DefaultIdentityRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{DefaultIdentityUser}"/>。</param>
        /// <param name="createdByGenerator">给定的 <see cref="IDefaultValueGenerator{TCreatedBy}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator{TGenId}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityServerStoreInitializer(SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IUserStore<DefaultIdentityUser<Guid, Guid>> userStore,
            IDefaultValueGenerator<Guid> createdByGenerator,
            IStoreIdentifierGenerator<Guid> identifierGenerator, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, createdByGenerator, identifierGenerator, loggerFactory)
        {
        }

    }
}
