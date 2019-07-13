using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Data;

    public class TestInitializerService<TAccessor> : InitializerServiceBase<TAccessor, TestIdentifierService>
        where TAccessor : IdentityDbContextAccessor
    {
        private SignInManager<DefaultIdentityUser> _signInManager;

        private IList<DefaultIdentityRole> _roles;
        private IList<DefaultIdentityUser> _users;


        public TestInitializerService(SignInManager<DefaultIdentityUser> signInManager,
            IIdentifierService identifier, ILoggerFactory loggerFactory)
            : base(identifier, loggerFactory)
        {
            _signInManager = signInManager.NotNull(nameof(signInManager));
        }


        protected override void InitializeStores(IStoreHub<TAccessor> stores)
        {
            base.InitializeStores(stores);

            InitializeRoles(stores);

            InitializeUsers(stores);
        }

        private void InitializeRoles(IStoreHub<TAccessor> stores)
        {
            if (!stores.Accessor.Roles.Any())
            {
                _roles = new List<DefaultIdentityRole>
                {
                    new DefaultIdentityRole("SuperAdministrator")
                    {
                        Id = Identifier.GetRoleIdAsync(default).Result
                    },
                    new DefaultIdentityRole("Administrator")
                    {
                        Id = Identifier.GetRoleIdAsync(default).Result
                    }
                };

                stores.Accessor.Roles.AddRange(_roles);
            }
            else
            {
                _roles = stores.Accessor.Roles.ToList();
            }
        }

        private void InitializeUsers(IStoreHub<TAccessor> stores)
        {
            if (!stores.Accessor.Users.Any())
            {
                _users = new List<DefaultIdentityUser>
                {
                    new DefaultIdentityUser("Librame")
                    {
                        Id = Identifier.GetUserIdAsync(default).Result
                    },
                    new DefaultIdentityUser("LibrameCore")
                    {
                        Id = Identifier.GetUserIdAsync(default).Result
                    }
                };

                var password = "Password!123456";

                var i = 0;
                foreach (var user in _users)
                {
                    _signInManager.UserManager.CreateAsync(user, password).Wait();

                    stores.Accessor.UserRoles.Add(new IdentityUserRole<string>
                    {
                        RoleId = _roles[i].Id,
                        UserId = user.Id
                    });

                    i++;
                }
            }
            else
            {
                _users = stores.Accessor.Users.ToList();
            }
        }
    }
}
