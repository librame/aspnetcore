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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Identity.Stores
{
    using AspNetCore.Identity.Options;
    using Extensions;
    using Extensions.Core.Utilities;
    using Extensions.Data.Accessors;
    using Extensions.Data.Stores;

    /// <summary>
    /// 抽象身份存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public abstract class AbstractIdentityStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        : AbstractIdentityStoreInitializer<TAccessor,
            DefaultIdentityRole<TGenId, TCreatedBy>,
            DefaultIdentityRoleClaim<TGenId, TCreatedBy>,
            DefaultIdentityUser<TGenId, TCreatedBy>,
            DefaultIdentityUserClaim<TGenId, TCreatedBy>,
            DefaultIdentityUserLogin<TGenId, TCreatedBy>,
            DefaultIdentityUserRole<TGenId, TCreatedBy>,
            DefaultIdentityUserToken<TGenId, TCreatedBy>,
            TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IAccessor
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个抽象身份存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractIdentityStoreInitializer(IdentityStoreInitializationOptions initializationOptions,
            SignInManager<DefaultIdentityUser<TGenId, TCreatedBy>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId, TCreatedBy>> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(initializationOptions, signInManager, roleMananger, identifierGenerator, validator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 抽象身份存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TRole">指定的角色类型。</typeparam>
    /// <typeparam name="TRoleClaim">指定的角色声明类型。</typeparam>
    /// <typeparam name="TUser">指定的用户类型。</typeparam>
    /// <typeparam name="TUserClaim">指定的用户声明类型。</typeparam>
    /// <typeparam name="TUserLogin">指定的用户登陆类型。</typeparam>
    /// <typeparam name="TUserRole">指定的用户角色类型。</typeparam>
    /// <typeparam name="TUserToken">指定的用户令牌类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public abstract class AbstractIdentityStoreInitializer<TAccessor, TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken, TGenId, TIncremId, TCreatedBy>
        : AbstractDataStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IAccessor
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TRoleClaim : DefaultIdentityRoleClaim<TGenId, TCreatedBy>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TUserClaim : DefaultIdentityUserClaim<TGenId, TCreatedBy>
        where TUserLogin : DefaultIdentityUserLogin<TGenId, TCreatedBy>
        where TUserRole : DefaultIdentityUserRole<TGenId, TCreatedBy>
        where TUserToken : DefaultIdentityUserToken<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个抽象身份存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator"/>。</param>
        /// <param name="validator">给定的 <see cref="IStoreInitializationValidator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected AbstractIdentityStoreInitializer(IdentityStoreInitializationOptions initializationOptions,
            SignInManager<TUser> signInManager, RoleManager<TRole> roleMananger,
            IStoreIdentifierGenerator identifierGenerator,
            IStoreInitializationValidator validator, ILoggerFactory loggerFactory)
            : base(identifierGenerator, validator, loggerFactory)
        {
            InitializationOptions = initializationOptions.NotNull(nameof(initializationOptions));

            SignInManager = signInManager.NotNull(nameof(signInManager));
            RoleManager = roleMananger.NotNull(nameof(roleMananger));

            CurrentRoles = new List<TRole>();
            CurrentUsers = new List<TUser>();
        }


        /// <summary>
        /// 初始化选项。
        /// </summary>
        /// <value>返回 <see cref="IdentityStoreInitializationOptions"/>。</value>
        protected IdentityStoreInitializationOptions InitializationOptions { get; }

        /// <summary>
        /// 登入管理器。
        /// </summary>
        public SignInManager<TUser> SignInManager { get; }

        /// <summary>
        /// 角色管理器。
        /// </summary>
        public RoleManager<TRole> RoleManager { get; }

        /// <summary>
        /// 用户管理器。
        /// </summary>
        public UserManager<TUser> UserManager
            => SignInManager.UserManager;


        /// <summary>
        /// 身份存储标识符生成器。
        /// </summary>
        protected IIdentityStoreIdentifierGenerator<TGenId> IdentityIdentifierGenerator
            => IdentifierGenerator as IIdentityStoreIdentifierGenerator<TGenId>;

        /// <summary>
        /// 当前角色列表。
        /// </summary>
        protected List<TRole> CurrentRoles { get; }

        /// <summary>
        /// 当前用户列表。
        /// </summary>
        protected List<TUser> CurrentUsers { get; }


        /// <summary>
        /// 获取默认用户角色。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <returns>返回 <typeparamref name="TRole"/>。</returns>
        protected virtual TRole GetDefaultUserRole(TUser user)
            => CurrentUsers.Count == 0 ? CurrentRoles.First() : CurrentRoles.Last();


        /// <summary>
        /// 初始化核心。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub"/>。</param>
        protected override void InitializeCore(IStoreHub stores)
        {
            base.InitializeCore(stores);

            if (stores is IIdentityStoreHub<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken> identityStores)
            {
                InitializeIdentityRoles(identityStores);

                InitializeIdentityUsers(identityStores);
            }
        }

        /// <summary>
        /// 初始化身份角色集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeIdentityRoles(IIdentityStoreHub<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken> identityStores)
        {
            var roleType = typeof(TRole);

            foreach (var name in InitializationOptions.DefaultRoleNames)
            {
                if (!TryGetRole(name, out var role))
                {
                    role = roleType.EnsureCreate<TRole>();

                    role.NormalizedName = RoleManager.NormalizeKey(role.Name);
                    role.Name = name;

                    role.Id = IdentityIdentifierGenerator.GenerateRoleIdAsync().ConfigureAndResult();

                    role.PopulateCreationAsync(Clock).ConfigureAndResult();

                    identityStores.TryCreate(role);

                    RequiredSaveChanges = true;
                }

                CurrentRoles.Add(role);
            }

            // TryGetRole
            bool TryGetRole(string roleName, out TRole role)
            {
                role = RoleManager.FindByNameAsync(roleName).ConfigureAndResult();
                return role.IsNotNull();
            }
        }

        /// <summary>
        /// 初始化用户集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeIdentityUsers(IIdentityStoreHub<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken> identityStores)
        {
            var userType = typeof(TUser);
            var userRoleType = typeof(TUserRole);

            foreach (var email in InitializationOptions.DefaultUserEmails)
            {
                if (!TryGetUser(email, out var user))
                {
                    user = userType.EnsureCreate<TUser>();

                    user.NormalizedUserName = UserManager.NormalizeName(email);
                    user.UserName = email;

                    if (UserManager.SupportsUserEmail)
                    {
                        user.NormalizedEmail = UserManager.NormalizeEmail(email);
                        user.Email = UserManager.NormalizeEmail(email);
                        user.EmailConfirmed = true;
                    }

                    var defaultPassword = InitializationOptions.DefaultPassword;
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(user, defaultPassword);
                    user.SecurityStamp = RandomUtility.GenerateByteArray(20).AsHexString();

                    user.Id = IdentityIdentifierGenerator.GenerateRoleIdAsync().ConfigureAndResult();

                    user.PopulateCreationAsync(Clock).ConfigureAndResult();

                    identityStores.TryCreate(user);

                    // UserRole
                    var defaultRole = GetDefaultUserRole(user);
                    var userRole = userRoleType.EnsureCreate<TUserRole>();

                    userRole.UserId = user.Id;
                    userRole.RoleId = defaultRole.Id;

                    userRole.PopulateCreationAsync(Clock).ConfigureAndResult();

                    identityStores.TryCreate(userRole);

                    RequiredSaveChanges = true;
                }

                CurrentUsers.Add(user);
            }

            // TryGetUser
            bool TryGetUser(string userName, out TUser user)
            {
                user = UserManager.FindByNameAsync(userName).ConfigureAndResult();
                return user.IsNotNull();
            }
        }

    }
}
