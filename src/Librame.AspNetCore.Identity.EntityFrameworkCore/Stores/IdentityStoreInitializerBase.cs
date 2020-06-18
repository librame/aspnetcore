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
    using AspNetCore.Identity.Accessors;
    using Extensions;
    using Extensions.Core.Utilities;
    using Extensions.Data.Stores;
    using Extensions.Data.ValueGenerators;

    /// <summary>
    /// 身份存储初始化器基类。
    /// </summary>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityStoreInitializerBase<TGenId, TIncremId, TCreatedBy>
        : IdentityStoreInitializerBase<DefaultIdentityRole<TGenId, TCreatedBy>,
            DefaultIdentityUser<TGenId, TCreatedBy>, DefaultIdentityUserRole<TGenId, TCreatedBy>,
            TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份存储初始化器基类。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{DefaultIdentityUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{DefaultIdentityRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{DefaultIdentityUser}"/>。</param>
        /// <param name="createdByGenerator">给定的 <see cref="IDefaultValueGenerator{TCreatedBy}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator{TGenId}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityStoreInitializerBase(SignInManager<DefaultIdentityUser<TGenId, TCreatedBy>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId, TCreatedBy>> roleMananger,
            IUserStore<DefaultIdentityUser<TGenId, TCreatedBy>> userStore,
            IDefaultValueGenerator<TCreatedBy> createdByGenerator,
            IStoreIdentifierGenerator<TGenId> identifierGenerator, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, createdByGenerator, identifierGenerator, loggerFactory)
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
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityStoreInitializerBase<TRole, TUser, TUserRole, TGenId, TIncremId, TCreatedBy>
        : DataStoreInitializerBase<TGenId, TIncremId, TCreatedBy>
        where TRole : DefaultIdentityRole<TGenId, TCreatedBy>
        where TUser : DefaultIdentityUser<TGenId, TCreatedBy>
        where TUserRole : DefaultIdentityUserRole<TGenId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份存储初始化器基类。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="createdByGenerator">给定的 <see cref="IDefaultValueGenerator{TCreatedBy}"/>。</param>
        /// <param name="identifierGenerator">给定的 <see cref="IStoreIdentifierGenerator{TGenId}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityStoreInitializerBase(SignInManager<TUser> signInManager,
            RoleManager<TRole> roleMananger, IUserStore<TUser> userStore,
            IDefaultValueGenerator<TCreatedBy> createdByGenerator,
            IStoreIdentifierGenerator<TGenId> identifierGenerator, ILoggerFactory loggerFactory)
            : base(createdByGenerator, identifierGenerator, loggerFactory)
        {
            SignInManager = signInManager.NotNull(nameof(signInManager));
            RoleManager = roleMananger.NotNull(nameof(roleMananger));
            UserStore = userStore.NotNull(nameof(userStore));

            CurrentRoles = new List<TRole>();
            CurrentUsers = new List<TUser>();
        }


        /// <summary>
        /// 登入管理器。
        /// </summary>
        public SignInManager<TUser> SignInManager { get; }

        /// <summary>
        /// 角色管理器。
        /// </summary>
        public RoleManager<TRole> RoleManager { get; }

        /// <summary>
        /// 用户存储。
        /// </summary>
        public IUserStore<TUser> UserStore { get; }

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
        /// 获取默认密码。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetDefaultPassword(TUser user)
            => "Password!123456";

        /// <summary>
        /// 获取默认角色名称集合。
        /// </summary>
        /// <returns>返回字符串数组。</returns>
        protected virtual string[] GetDefaultRoleNames()
            => new string[] { "SuperAdministrator", "Administrator" };

        /// <summary>
        /// 获取默认用户电邮集合（默认将电邮当作用户名）。
        /// </summary>
        /// <returns>返回字符串数组。</returns>
        protected virtual string[] GetDefaultUserEmails()
            => new string[] { "librame@librame.net", "libramecore@librame.net" };

        /// <summary>
        /// 获取默认用户角色。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <returns>返回 <typeparamref name="TRole"/>。</returns>
        protected virtual TRole GetDefaultUserRole(TUser user)
            => CurrentUsers.Count == 0 ? CurrentRoles.First() : CurrentRoles.Last();


        /// <summary>
        /// 初始化数据。
        /// </summary>
        /// <param name="dataStores">给定的数据存储中心。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected override void InitializeData(IDataStoreHub<DataAudit<TGenId, TCreatedBy>,
            DataAuditProperty<TIncremId, TGenId>, DataEntity<TGenId, TCreatedBy>,
            DataMigration<TGenId, TCreatedBy>, DataTenant<TGenId, TCreatedBy>, TGenId> dataStores)
        {
            base.InitializeData(dataStores);

            if (dataStores.Accessor is IIdentityDbContextAccessor<TRole, TUser, TGenId, TCreatedBy> identityAccessor)
            {
                InitializeIdentityRoles(identityAccessor);

                InitializeIdentityUsers(identityAccessor);
            }
        }

        /// <summary>
        /// 初始化身份角色集合。
        /// </summary>
        /// <param name="identityAccessor">给定的身份数据库上下文访问器。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeIdentityRoles(IIdentityDbContextAccessor<TRole, TUser, TGenId, TCreatedBy> identityAccessor)
        {
            var roleType = typeof(TRole);

            foreach (var name in GetDefaultRoleNames())
            {
                if (!TryGetRole(name, out var role))
                {
                    role = roleType.EnsureCreate<TRole>();

                    role.NormalizedName = RoleManager.NormalizeKey(role.Name);
                    role.Name = name;

                    role.Id = IdentityIdentifierGenerator.GenerateRoleIdAsync().ConfigureAndResult();
                    role.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
                    role.CreatedTimeTicks = role.CreatedTime.Ticks;
                    role.CreatedBy = CreatedByGenerator.GetValueAsync(GetType()).ConfigureAndResult();

                    identityAccessor.Roles.Add(role);
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
        /// <param name="identityAccessor">给定的身份数据库上下文访问器。</param>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeIdentityUsers(IIdentityDbContextAccessor<TRole, TUser, TGenId, TCreatedBy> identityAccessor)
        {
            var userType = typeof(TUser);
            var userRoleType = typeof(TUserRole);

            foreach (var email in GetDefaultUserEmails())
            {
                if (!TryGetUser(email, out TUser user))
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

                    var defaultPassword = GetDefaultPassword(user);
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(user, defaultPassword);
                    user.SecurityStamp = RandomUtility.GenerateByteArray(20).AsHexString();

                    user.Id = IdentityIdentifierGenerator.GenerateRoleIdAsync().ConfigureAndResult();
                    user.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
                    user.CreatedTimeTicks = user.CreatedTime.Ticks;
                    user.CreatedBy = CreatedByGenerator.GetValueAsync(GetType()).ConfigureAndResult();

                    identityAccessor.Users.Add(user);

                    // UserRole
                    var defaultRole = GetDefaultUserRole(user);
                    var userRole = userRoleType.EnsureCreate<TUserRole>();

                    userRole.UserId = user.Id;
                    userRole.RoleId = defaultRole.Id;
                    userRole.CreatedTime = Clock.GetNowOffsetAsync().ConfigureAndResult();
                    userRole.CreatedTimeTicks = userRole.CreatedTime.Ticks;
                    userRole.CreatedBy = CreatedByGenerator.GetValueAsync(GetType()).ConfigureAndResult();

                    identityAccessor.UserRoles.Add(userRole);

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
