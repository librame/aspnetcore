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
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;
    using Extensions.Data;

    /// <summary>
    /// 身份存储初始化器。
    /// </summary>
    public class IdentityStoreInitializer : IdentityStoreInitializer<IdentityDbContextAccessor, IdentityStoreIdentifier>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityStoreInitializer"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="identifier">给定的 <see cref="IStoreIdentifier"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
            RoleManager<DefaultIdentityRole<string>> roleMananger,
            IUserStore<DefaultIdentityUser<string>> userStore,
            IOptions<IdentityBuilderOptions> options,
            IClockService clock, IStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(signInManager, roleMananger, userStore, options, clock, identifier, loggerFactory)
        {
        }
    }


    /// <summary>
    /// 身份存储初始化器。
    /// </summary>
    /// <typeparam name="TAccessor">指定的身份数据库上下文访问器类型。</typeparam>
    /// <typeparam name="TIdentifier">指定的身份存储标识符类型。</typeparam>
    public class IdentityStoreInitializer<TAccessor, TIdentifier> : StoreInitializerBase<TAccessor, TIdentifier>
        where TAccessor : IdentityDbContextAccessor
        where TIdentifier : IdentityStoreIdentifier
    {
        private readonly string _defaultCreatedBy
            = nameof(IdentityStoreInitializer);

        private readonly SignInManager<DefaultIdentityUser<string>> _signInManager;
        private readonly RoleManager<DefaultIdentityRole<string>> _roleMananger;
        private readonly IUserStore<DefaultIdentityUser<string>> _userStore;
        private readonly IdentityBuilderOptions _options;

        private IList<DefaultIdentityRole<string>> _roles;
        private IList<DefaultIdentityUser<string>> _users;


        /// <summary>
        /// 构造一个 <see cref="IdentityStoreInitializer"/>。
        /// </summary>
        /// <param name="signInManager">给定的 <see cref="SignInManager{TUser}"/>。</param>
        /// <param name="roleMananger">给定的 <see cref="RoleManager{TRole}"/>。</param>
        /// <param name="userStore">给定的 <see cref="IUserStore{TUser}"/>。</param>
        /// <param name="options">给定的 <see cref="IOptions{IdentityBuilderOptions}"/>。</param>
        /// <param name="clock">给定的 <see cref="IClockService"/>。</param>
        /// <param name="identifier">给定的 <see cref="IStoreIdentifier"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public IdentityStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
            RoleManager<DefaultIdentityRole<string>> roleMananger,
            IUserStore<DefaultIdentityUser<string>> userStore,
            IOptions<IdentityBuilderOptions> options,
            IClockService clock, IStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(clock, identifier, loggerFactory)
        {
            _signInManager = signInManager;
            _roleMananger = roleMananger;
            _userStore = userStore;
            _options = options.Value;
        }


        /// <summary>
        /// 初始化核心。
        /// </summary>
        /// <param name="stores">给定的 <see cref="IStoreHub{TAccessor, TAudit, TEntity, TMigration, TTenant}"/>。</param>
        protected override void InitializeCore<TAudit, TEntity, TMigration, TTenant>(IStoreHub<TAccessor, TAudit, TEntity, TMigration, TTenant> stores)
        {
            base.InitializeCore(stores);

            InitializeRoles(stores.Accessor);

            InitializeUsers(stores.Accessor);

            InitializeClaims(stores.Accessor);
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
                    role.Id = Identifier.GetRoleIdAsync().Result;
                    role.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).Result;
                    role.CreatedBy = _defaultCreatedBy;
                    role.NormalizedName = _roleMananger.NormalizeKey(role.Name);

                    accessor.Roles.Add(role);
                    //_roleMananger.CreateAsync(role).Wait();
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
                        emailStore.SetEmailAsync(user, user.UserName, CancellationToken.None).Wait();
                    }

                    _userStore.SetUserNameAsync(user, user.UserName, default).Wait();

                    user.Id = Identifier.GetUserIdAsync().Result;
                    user.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).Result;
                    user.CreatedBy = _defaultCreatedBy;

                    user.PasswordHash = _signInManager.UserManager.PasswordHasher.HashPassword(user, _options.DefaultPassword);
                    user.NormalizedUserName = _signInManager.UserManager.NormalizeName(user.UserName);
                    user.NormalizedEmail = _signInManager.UserManager.NormalizeEmail(user.Email);

                    var identifier = new RandomNumberAlgorithmIdentifier(20, Base32AlgorithmConverter.Default);
                    user.SecurityStamp = identifier;
                    user.EmailConfirmed = true;

                    accessor.Users.Add(user);
                    //result = _signInManager.UserManager.CreateAsync(user, _options.DefaultPassword).Result;

                    var userRole = new DefaultIdentityUserRole<string>
                    {
                        RoleId = _roles[i].Id,
                        UserId = user.Id,
                        CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).Result,
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

                        userClaim.CreatedTime = Clock.GetOffsetNowAsync(DateTimeOffset.UtcNow, true).Result;
                        userClaim.CreatedBy = _defaultCreatedBy;

                        accessor.UserClaims.Add(userClaim);
                    }
                    //_signInManager.UserManager.AddClaimsAsync(user, claims).Wait();
                }

                RequiredSaveChanges = true;
            }
        }

    }
}
