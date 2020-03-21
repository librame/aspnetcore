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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Librame.AspNetCore.Identity.Stores
{
    using Accessors;
    using Extensions;
    using Extensions.Core.Services;
    using Extensions.Core.Utilities;
    using Extensions.Data.Stores;
    using Services;

    /// <summary>
    /// 身份存储初始化器。
    /// </summary>
    public class IdentityStoreInitializer : IdentityStoreInitializer<IdentityStoreIdentifier>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityStoreInitializer"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="identifier">给定的 <see cref="IStoreIdentifier"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
            RoleManager<DefaultIdentityRole<string>> roleMananger,
            IUserStore<DefaultIdentityUser<string>> userStore,
            IStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, identifier, loggerFactory)
        {
        }
    }


    /// <summary>
    /// 身份存储初始化器。
    /// </summary>
    /// <typeparam name="TIdentifier">指定的标识符类型。</typeparam>
    public class IdentityStoreInitializer<TIdentifier> : StoreInitializer<TIdentifier>
         where TIdentifier : IdentityStoreIdentifier
    {
        private readonly string _defaultCreatedBy
            = nameof(IdentityStoreInitializer);

        private readonly SignInManager<DefaultIdentityUser<string>> _signInManager;
        private readonly RoleManager<DefaultIdentityRole<string>> _roleMananger;
        private readonly IUserStore<DefaultIdentityUser<string>> _userStore;

        private IList<DefaultIdentityRole<string>> _roles;
        private IList<DefaultIdentityUser<string>> _users;


        /// <summary>
        /// 构造一个身份存储初始化器。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="identifier">给定的 <see cref="IStoreIdentifier"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
            RoleManager<DefaultIdentityRole<string>> roleMananger,
            IUserStore<DefaultIdentityUser<string>> userStore,
            IStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(identifier, loggerFactory)
        {
            _signInManager = signInManager;
            _roleMananger = roleMananger;
            _userStore = userStore;
        }


        /// <summary>
        /// 初始化核心。
        /// </summary>
        /// <param name="stores">给定的 <see cref="StoreHub{TAccessor, TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId}"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "stores")]
        protected override void InitializeCore<TAccessor, TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId>
            (StoreHub<TAccessor, TAudit, TAuditProperty, TEntity, TMigration, TTenant, TGenId, TIncremId> stores)
        {
            base.InitializeCore(stores);

            if (stores.Accessor is IdentityDbContextAccessor dbContextAccessor)
            {
                InitializeRoles(dbContextAccessor);

                InitializeUsers(dbContextAccessor);

                InitializeClaims(dbContextAccessor);
            }
        }

        private void InitializeRoles(IdentityDbContextAccessor accessor)
        {
            if (!accessor.Roles.Any())
            {
                _roles = new List<DefaultIdentityRole<string>>
                {
                    new DefaultIdentityRole<string>("SuperAdministrator"),
                    new DefaultIdentityRole<string>("Administrator")
                };

                foreach (var role in _roles)
                {
                    role.Id = Identifier.GetRoleIdAsync().ConfigureAndResult();
                    role.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
                    role.CreatedBy = _defaultCreatedBy;
                    role.NormalizedName = _roleMananger.NormalizeKey(role.Name);

                    accessor.Roles.Add(role);
                    //_roleMananger.CreateAsync(role).ConfigureAndWait();
                }

                RequiredSaveChanges = true;
            }
            else
            {
                _roles = accessor.Roles.ToList();
            }
        }

        private void InitializeUsers(IdentityDbContextAccessor accessor)
        {
            if (!accessor.Users.Any())
            {
                var defaultPasswordService = accessor.ServiceFactory.GetRequiredService<IDefaultPasswordService>();

                // Identity 默认以邮箱为用户名
                _users = new List<DefaultIdentityUser<string>>
                {
                    new DefaultIdentityUser<string>("librame@librame.net"),
                    new DefaultIdentityUser<string>("libramecore@librame.net")
                };

                var i = 0;
                foreach (var user in _users)
                {
                    if (_signInManager.UserManager.SupportsUserEmail)
                    {
                        var emailStore = (IUserEmailStore<DefaultIdentityUser<string>>)_userStore;
                        emailStore.SetEmailAsync(user, user.UserName, CancellationToken.None).ConfigureAndWait();
                    }

                    _userStore.SetUserNameAsync(user, user.UserName, default).ConfigureAndWait();

                    user.NormalizedUserName = _signInManager.UserManager.NormalizeName(user.UserName);
                    user.NormalizedEmail = _signInManager.UserManager.NormalizeEmail(user.Email);

                    user.Id = Identifier.GetUserIdAsync().ConfigureAndResult();
                    user.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
                    user.CreatedBy = _defaultCreatedBy;

                    user.SecurityStamp = RandomUtility.GenerateNumber(20).AsHexString();
                    user.EmailConfirmed = true;

                    var defaultPassword = defaultPasswordService.GetDefaultPassword(user);
                    user.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user, defaultPassword);

                    accessor.Users.Add(user);
                    //result = _signInManager.UserManager.CreateAsync(user, _options.DefaultPassword).ConfigureAndResult();

                    var userRole = new DefaultIdentityUserRole<string>
                    {
                        RoleId = _roles[i].Id,
                        UserId = user.Id,
                        CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult(),
                        CreatedBy = _defaultCreatedBy
                    };

                    accessor.UserRoles.Add(userRole);

                    i++;
                }

                RequiredSaveChanges = true;
            }
            else
            {
                _users = accessor.Users.ToList();
            }
        }

        private void InitializeClaims(IdentityDbContextAccessor accessor)
        {
            if (!accessor.UserClaims.Any())
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Gender, "male"),
                    new Claim(ClaimTypes.Country, "China")
                };

                foreach (var user in _users)
                {
                    foreach (var claim in claims)
                    {
                        var userClaim = new DefaultIdentityUserClaim<string>()
                        {
                            UserId = user.Id
                        };
                        userClaim.InitializeFromClaim(claim);

                        userClaim.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).ConfigureAndResult();
                        userClaim.CreatedBy = _defaultCreatedBy;

                        accessor.UserClaims.Add(userClaim);
                    }
                    //_signInManager.UserManager.AddClaimsAsync(user, claims).ConfigureAndWait();
                }

                RequiredSaveChanges = true;
            }
        }

    }
}
