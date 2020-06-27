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
using Microsoft.Extensions.Options;
using System;

namespace Librame.AspNetCore.Identity.Stores
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Builders;
    using Extensions.Data.Stores;

    /// <summary>
    /// <see cref="Guid"/> 身份存储初始化器。
    /// </summary>
    public class GuidIdentityStoreInitializer : GuidIdentityStoreInitializer<IdentityDbContextAccessor>
    {
        /// <summary>
        /// 构造一个 <see cref="GuidIdentityStoreInitializer"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityStoreInitializer(IOptions<IdentityBuilderOptions> options,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options, signInManager, roleMananger,
                  identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// <see cref="Guid"/> 身份存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class GuidIdentityStoreInitializer<TAccessor> : AbstractIdentityStoreInitializer<TAccessor, Guid, int, Guid>
        where TAccessor : IdentityDbContextAccessor
    {
        /// <summary>
        /// 构造一个 <see cref="GuidIdentityStoreInitializer{TAccessor}"/>。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityStoreInitializer(IOptions<IdentityBuilderOptions> options,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(options?.Value.Stores.Initialization, signInManager, roleMananger,
                  identifierGenerator, validator, loggerFactory)
        {
        }

    }
}
