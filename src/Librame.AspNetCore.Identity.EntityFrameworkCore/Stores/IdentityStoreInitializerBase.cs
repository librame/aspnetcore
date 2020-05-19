#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Librame.AspNetCore.Identity.Stores
{
    using Extensions;
    using Extensions.Data.Stores;

    /// <summary>
    /// 身份存储初始化器基类。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class IdentityStoreInitializerBase<TGenId> : IdentityStoreInitializerBase<DefaultIdentityRole<TGenId>,
            DefaultIdentityUser<TGenId>, DefaultIdentityUserRole<TGenId>, TGenId>
        where TGenId : IEquatable<TGenId>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityStoreInitializerBase{TGenId}"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{DefaultIdentityUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{DefaultIdentityRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{DefaultIdentityUser}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator{TGenId}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityStoreInitializerBase(SignInManager<DefaultIdentityUser<TGenId>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId>> roleMananger,
            IUserStore<DefaultIdentityUser<TGenId>> userStore,
            IStoreIdentifierGenerator<TGenId> identifierGenerator, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, identifierGenerator, loggerFactory)
        {
        }
    }


    /// <summary>
    /// 身份存储初始化器基类。
    /// </summary>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    public class IdentityStoreInitializerBase<TRole, TUser, TUserRole, TGenId>
        : AbstractIdentityStoreInitializer<TRole, TUser, TUserRole, TGenId>
        where TRole : DefaultIdentityRole<TGenId>
        where TUser : DefaultIdentityUser<TGenId>
        where TUserRole : DefaultIdentityUserRole<TGenId>
        where TGenId : IEquatable<TGenId>
    {
        private readonly DbContext _dbContext;


        /// <summary>
        /// 构造一个 <see cref="IdentityStoreInitializerBase{TRole, TUser, TUserRole, TGenId}"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator{TGenId}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityStoreInitializerBase(SignInManager<TUser> signInManager,
            RoleManager<TRole> roleMananger, IUserStore<TUser> userStore,
            IStoreIdentifierGenerator<TGenId> identifierGenerator, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, identifierGenerator, loggerFactory)
        {
            _dbContext = (DbContext)UserStore.GetType().GetProperty("Context")?.GetValue(UserStore);
            _dbContext.NotNull(nameof(_dbContext));
        }


        /// <summary>
        /// 添加角色到访问器。
        /// </summary>
        /// <param name="role">给定的 <typeparamref name="TRole"/>。</param>
        protected override void AddRoleToAccessor(TRole role)
            => _dbContext.Add(role);

        /// <summary>
        /// 添加用户到访问器。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        protected override void AddUserToAccessor(TUser user)
            => _dbContext.Add(user);

        /// <summary>
        /// 添加用户角色到访问器。
        /// </summary>
        /// <param name="userRole">给定的 <typeparamref name="TUserRole"/>。</param>
        protected override void AddUserRoleToAccessor(TUserRole userRole)
            => _dbContext.Add(userRole);
    }
}
