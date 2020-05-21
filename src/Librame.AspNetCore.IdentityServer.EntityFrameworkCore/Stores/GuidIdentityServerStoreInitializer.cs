﻿#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
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
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator{TGenId}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public GuidIdentityServerStoreInitializer(SignInManager<DefaultIdentityUser<Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid>> roleMananger,
            IUserStore<DefaultIdentityUser<Guid>> userStore,
            IStoreIdentifierGenerator<Guid> identifierGenerator, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, identifierGenerator, loggerFactory)
        {
        }

    }
}