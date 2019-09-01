using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Data;

    public class TestIdentityStoreInitializer : StoreInitializerBase<IdentityDbContextAccessor, IdentityStoreIdentifier>
    {
        private readonly SignInManager<DefaultIdentityUser> _signInManager;
        private readonly RoleManager<DefaultIdentityRole> _roleMananger;
        private readonly IUserStore<DefaultIdentityUser> _userStore;
        private readonly IUserEmailStore<DefaultIdentityUser> _emailStore;

        private IList<DefaultIdentityRole> _roles;
        private IList<DefaultIdentityUser> _users;


        public TestIdentityStoreInitializer(SignInManager<DefaultIdentityUser> signInManager,
            RoleManager<DefaultIdentityRole> roleMananger,
            IUserStore<DefaultIdentityUser> userStore,
            IStoreIdentifier identifier, ILoggerFactory loggerFactory)
            : base(identifier, loggerFactory)
        {
            _signInManager = signInManager.NotNull(nameof(signInManager));
            _roleMananger = roleMananger.NotNull(nameof(roleMananger));
            _userStore = userStore.NotNull(nameof(userStore));
            _emailStore = userStore.GetUserEmailStore(signInManager);
        }


        protected override void InitializeCore(IStoreHub<IdentityDbContextAccessor> stores)
        {
            base.InitializeCore(stores);

            InitializeRoles(stores);

            InitializeUsers(stores);

            InitializeClaims(stores);
        }

        private void InitializeRoles(IStoreHub<IdentityDbContextAccessor> stores)
        {
            if (!stores.Accessor.Roles.Any())
            {
                _roles = new List<DefaultIdentityRole>
                {
                    new DefaultIdentityRole("SuperAdministrator")
                    {
                        Id = Identifier.GetRoleIdAsync().Result
                    },
                    new DefaultIdentityRole("Administrator")
                    {
                        Id = Identifier.GetRoleIdAsync().Result
                    }
                };

                foreach (var role in _roles)
                    _roleMananger.CreateAsync(role).Wait();
            }
            else
            {
                _roles = stores.Accessor.Roles.ToList();
            }
        }

        private void InitializeUsers(IStoreHub<IdentityDbContextAccessor> stores)
        {
            if (!stores.Accessor.Users.Any())
            {
                // Identity 默认以邮箱为用户名
                _users = new List<DefaultIdentityUser>
                {
                    new DefaultIdentityUser("librame@librame.net")
                    {
                        Id = Identifier.GetUserIdAsync().Result
                    },
                    new DefaultIdentityUser("libramecore@librame.net")
                    {
                        Id = Identifier.GetUserIdAsync().Result
                    }
                };

                var password = "Password!123456";

                var i = 0;
                foreach (var user in _users)
                {
                    _emailStore.SetEmailAsync(user, user.UserName, default).Wait();
                    _userStore.SetUserNameAsync(user, user.UserName, default).Wait();

                    var result = _signInManager.UserManager.CreateAsync(user, password).Result;
                    if (result.Succeeded)
                    {
                        stores.Accessor.UserRoles.Add(new IdentityUserRole<string>
                        {
                            RoleId = _roles[i].Id,
                            UserId = user.Id
                        });
                    }

                    i++;
                }
            }
            else
            {
                _users = stores.Accessor.Users.ToList();
            }
        }

        private void InitializeClaims(IStoreHub<IdentityDbContextAccessor> stores)
        {
            if (!stores.Accessor.UserClaims.Any())
            {
                foreach (var user in _users)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Gender, "male"),
                        new Claim(ClaimTypes.Country, "China")
                    };

                    _signInManager.UserManager.AddClaimsAsync(user, claims).Wait();
                }
            }
        }

    }
}
