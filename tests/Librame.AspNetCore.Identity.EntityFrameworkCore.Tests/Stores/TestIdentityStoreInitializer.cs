using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core;
    using Extensions.Data;

    public class TestIdentityStoreInitializer : StoreInitializerBase<IdentityDbContextAccessor, IdentityStoreIdentifier>
    {
        private readonly string _defaultCreatedBy
            = nameof(TestIdentityStoreInitializer);

        private readonly SignInManager<DefaultIdentityUser<string>> _signInManager;
        private readonly RoleManager<DefaultIdentityRole<string>> _roleMananger;
        private readonly IUserStore<DefaultIdentityUser<string>> _userStore;
        private readonly IdentityBuilderOptions _options;

        private IList<DefaultIdentityRole<string>> _roles;
        private IList<DefaultIdentityUser<string>> _users;


        public TestIdentityStoreInitializer(SignInManager<DefaultIdentityUser<string>> signInManager,
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


        protected override void InitializeCore(IStoreHub<IdentityDbContextAccessor> stores)
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
                    user.NormalizedUserName = _signInManager.UserManager.NormalizeKey(user.UserName);
                    user.NormalizedEmail = _signInManager.UserManager.NormalizeKey(user.Email);

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
            }
        }

    }
}
