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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Librame.AspNetCore.Identity.Stores
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Builders;
    using AspNetCore.Identity.Options;
    using Extensions;
    using Extensions.Core.Utilities;
    using Extensions.Data.Accessors;
    using Extensions.Data.Stores;
    using Extensions.Data.Validators;

    /// <summary>
    /// 身份存储初始化器。
    /// </summary>
    public class IdentityStoreInitializer : IdentityStoreInitializer<IdentityDbContextAccessor>
    {
        /// <summary>
        /// 构造一个身份存储初始化器。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityStoreInitializer(IOptions<IdentityBuilderOptions> options,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(options, signInManager, roleMananger, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 身份存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    public class IdentityStoreInitializer<TAccessor> : IdentityStoreInitializer<TAccessor, Guid, int, Guid>
        where TAccessor : class, IIdentityAccessor, IDataAccessor
    {
        /// <summary>
        /// 构造一个身份存储初始化器。
        /// </summary>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityStoreInitializer(IOptions<IdentityBuilderOptions> options,
            SignInManager<DefaultIdentityUser<Guid, Guid>> signInManager,
            RoleManager<DefaultIdentityRole<Guid, Guid>> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(options?.Value.Stores.Initialization,
                  signInManager, roleMananger, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 身份存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的访问器类型。</typeparam>
    /// <typeparam name="TGenId">指定的生成式标识类型。</typeparam>
    /// <typeparam name="TIncremId">指定的增量式标识类型。</typeparam>
    /// <typeparam name="TCreatedBy">指定的创建者类型。</typeparam>
    public class IdentityStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        : IdentityStoreInitializer<TAccessor,
            DefaultIdentityRole<TGenId, TCreatedBy>,
            DefaultIdentityRoleClaim<TGenId, TCreatedBy>,
            DefaultIdentityUser<TGenId, TCreatedBy>,
            DefaultIdentityUserClaim<TGenId, TCreatedBy>,
            DefaultIdentityUserLogin<TGenId, TCreatedBy>,
            DefaultIdentityUserRole<TGenId, TCreatedBy>,
            DefaultIdentityUserToken<TGenId, TCreatedBy>,
            TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IIdentityAccessor<TGenId, TIncremId, TCreatedBy>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>
        where TGenId : IEquatable<TGenId>
        where TIncremId : IEquatable<TIncremId>
        where TCreatedBy : IEquatable<TCreatedBy>
    {
        /// <summary>
        /// 构造一个身份存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityStoreInitializer(IdentityStoreInitializationOptions initializationOptions,
            SignInManager<DefaultIdentityUser<TGenId, TCreatedBy>> signInManager,
            RoleManager<DefaultIdentityRole<TGenId, TCreatedBy>> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(initializationOptions, signInManager, roleMananger, validator, generator, loggerFactory)
        {
        }

    }


    /// <summary>
    /// 身份存储初始化器。
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
    public class IdentityStoreInitializer<TAccessor, TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken, TGenId, TIncremId, TCreatedBy>
        : DataStoreInitializer<TAccessor, TGenId, TIncremId, TCreatedBy>
        where TAccessor : class, IIdentityAccessor<TRole, TRoleClaim, TUser, TUserClaim, TUserLogin, TUserRole, TUserToken>,
            IDataAccessor<TGenId, TIncremId, TCreatedBy>
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
        /// 构造一个身份存储初始化器。
        /// </summary>
        /// <param name="initializationOptions">给定的 <see cref="IdentityStoreInitializationOptions"/>。</param>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="validator">给定的 <see cref="IDataInitializationValidator"/>。</param>
        /// <param name="generator">给定的 <see cref="IStoreIdentificationGenerator"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        protected IdentityStoreInitializer(IdentityStoreInitializationOptions initializationOptions,
            SignInManager<TUser> signInManager, RoleManager<TRole> roleMananger,
            IDataInitializationValidator validator, IStoreIdentificationGenerator generator, ILoggerFactory loggerFactory)
            : base(validator, generator, loggerFactory)
        {
            InitializationOptions = initializationOptions.NotNull(nameof(initializationOptions));

            SignInManager = signInManager.NotNull(nameof(signInManager));
            RoleManager = roleMananger.NotNull(nameof(roleMananger));
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
        /// 身份存储标识生成器。
        /// </summary>
        protected IIdentityStoreIdentificationGenerator<TGenId> IdentityGenerator
            => Generator as IIdentityStoreIdentificationGenerator<TGenId>;


        /// <summary>
        /// 当前角色列表。
        /// </summary>
        protected IReadOnlyList<TRole> CurrentRoles { get; set; }

        /// <summary>
        /// 当前用户列表。
        /// </summary>
        protected IReadOnlyList<TUser> CurrentUsers { get; set; }


        /// <summary>
        /// 获取默认用户角色。
        /// </summary>
        /// <param name="user">给定的 <typeparamref name="TUser"/>。</param>
        /// <returns>返回 <typeparamref name="TRole"/>。</returns>
        protected virtual TRole GetDefaultUserRole(TUser user)
            => CurrentUsers.Count == 0 ? CurrentRoles[0] : CurrentRoles[CurrentRoles.Count - 1];


        /// <summary>
        /// 初始化存储集合。
        /// </summary>
        protected override void InitializeStores()
        {
            base.InitializeStores();

            InitializeRoles();

            InitializeUsers();
        }

        /// <summary>
        /// 异步初始化存储集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        protected override async Task InitializeStoresAsync(CancellationToken cancellationToken)
        {
            await base.InitializeStoresAsync(cancellationToken).ConfigureAwait();

            await InitializeRolesAsync(cancellationToken).ConfigureAwait();

            await InitializeUsersAsync(cancellationToken).ConfigureAwait();
        }


        /// <summary>
        /// 初始化角色集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeRoles()
        {
            if (CurrentRoles.IsEmpty())
            {
                var roleType = typeof(TRole);

                CurrentRoles = InitializationOptions.DefaultRoleNames.Select(name =>
                {
                    var role = roleType.EnsureCreate<TRole>();

                    role.NormalizedName = RoleManager.NormalizeKey(role.Name);
                    role.Name = name;

                    role.Id = IdentityGenerator.GenerateRoleId();

                    role.PopulateCreation(Clock);

                    return role;
                })
                .ToList();
            }
            
            Accessor.RolesManager.TryAddRange(p => p.Equals(CurrentRoles[0]),
                () => CurrentRoles,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化角色集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual Task InitializeRolesAsync(CancellationToken cancellationToken)
        {
            if (CurrentRoles.IsEmpty())
            {
                var roleType = typeof(TRole);

                CurrentRoles = InitializationOptions.DefaultRoleNames.Select(name =>
                {
                    var role = roleType.EnsureCreate<TRole>();

                    role.NormalizedName = RoleManager.NormalizeKey(role.Name);
                    role.Name = name;

                    return role;
                })
                .ToList();

                CurrentRoles.ForEach(async role =>
                {
                    role.Id = await IdentityGenerator.GenerateRoleIdAsync(cancellationToken).ConfigureAwait();

                    await role.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.RolesManager.TryAddRangeAsync(p => p.Equals(CurrentRoles[0]),
                () => CurrentRoles,
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);
        }


        /// <summary>
        /// 初始化用户集合。
        /// </summary>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual void InitializeUsers()
        {
            if (CurrentUsers.IsEmpty())
            {
                var userType = typeof(TUser);

                CurrentUsers = InitializationOptions.DefaultUserEmails.Select(email =>
                {
                    var user = userType.EnsureCreate<TUser>();

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

                    user.Id = IdentityGenerator.GenerateRoleId();

                    user.PopulateCreation(Clock);

                    return user;
                })
                .ToList();
            }

            Accessor.UsersManager.TryAddRange(p => p.Equals(CurrentUsers[0]),
                () =>
                {
                    // AddUserRoles
                    var userRoleType = typeof(TUserRole);
                    var userRoles = CurrentUsers.Select(user =>
                    {
                        var defaultRole = GetDefaultUserRole(user);
                        var userRole = userRoleType.EnsureCreate<TUserRole>();

                        userRole.UserId = user.Id;
                        userRole.RoleId = defaultRole.Id;

                        userRole.PopulateCreation(Clock);

                        return userRole;
                    });

                    Accessor.UserRoles.AddRange(userRoles);

                    return CurrentUsers;
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                });
        }

        /// <summary>
        /// 异步初始化用户集合。
        /// </summary>
        /// <param name="cancellationToken">给定的 <see cref="CancellationToken"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        [SuppressMessage("Design", "CA1062:验证公共方法的参数")]
        protected virtual Task InitializeUsersAsync(CancellationToken cancellationToken)
        {
            if (CurrentUsers.IsEmpty())
            {
                var userType = typeof(TUser);

                CurrentUsers = InitializationOptions.DefaultUserEmails.Select(email =>
                {
                    var user = userType.EnsureCreate<TUser>();

                    user.NormalizedUserName = UserManager.NormalizeName(email);
                    user.UserName = email;

                    if (UserManager.SupportsUserEmail)
                    {
                        user.NormalizedEmail = UserManager.NormalizeEmail(email);
                        user.Email = email;
                        user.EmailConfirmed = true;
                    }

                    var defaultPassword = InitializationOptions.DefaultPassword;
                    user.PasswordHash = UserManager.PasswordHasher.HashPassword(user, defaultPassword);
                    user.SecurityStamp = RandomUtility.GenerateByteArray(20).AsHexString();

                    return user;
                })
                .ToList();

                CurrentUsers.ForEach(async user =>
                {
                    user.Id = await IdentityGenerator.GenerateUserIdAsync(cancellationToken).ConfigureAwait();

                    await user.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                });
            }

            return Accessor.UsersManager.TryAddRangeAsync(p => p.Equals(CurrentUsers[0]),
                async () =>
                {
                    // AddUserRoles
                    var userRoleType = typeof(TUserRole);
                    var userRoles = CurrentUsers.Select(user =>
                    {
                        var defaultRole = GetDefaultUserRole(user);
                        var userRole = userRoleType.EnsureCreate<TUserRole>();

                        userRole.UserId = user.Id;
                        userRole.RoleId = defaultRole.Id;

                        return userRole;
                    });

                    userRoles.ForEach(async userRole =>
                    {
                        await userRole.PopulateCreationAsync(Clock, cancellationToken).ConfigureAwait();
                    });

                    await Accessor.UserRoles.AddRangeAsync(userRoles, cancellationToken).ConfigureAwait();

                    return CurrentUsers;
                },
                addedPost =>
                {
                    if (!Accessor.RequiredSaveChanges)
                        Accessor.RequiredSaveChanges = true;
                },
                cancellationToken);
        }

    }
}
