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

namespace Librame.AspNetCore.IdentityServer.Stores
{
    using Identity.Stores;

    /// <summary>
    /// 身份服务器存储初始化器。
    /// </summary>
    public class IdentityServerStoreInitializer : IdentityStoreInitializer<IdentityServerStoreIdentifier>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerStoreInitializer"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="identifier">给定的 <see cref="IdentityServerStoreIdentifier"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityServerStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
            RoleManager<DefaultIdentityRole<string>> roleMananger,
            IUserStore<DefaultIdentityUser<string>> userStore,
            IdentityServerStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, identifier, loggerFactory)
        {
        }

    }
}