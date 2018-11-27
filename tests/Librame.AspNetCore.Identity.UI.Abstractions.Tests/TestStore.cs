using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions;
    using Extensions.Data;

    public class TestStore : AbstractStore<IIdentityDbContext, IdentityBuilderOptions>, ITestStore
    {
        public TestStore(IIdentityDbContext dbContext)
            : base(dbContext)
        {
            Initialize();
        }

        private void Initialize()
        {
            UseWriteStore();

            var hasRoles = DbContext.Roles.IsNotEmpty();
            var hasUsers = DbContext.Users.IsNotEmpty();

            if (hasRoles && hasUsers)
            {
                UseDefaultStore();
                return;
            }

            IdentityRole firstRole;

            if (!hasRoles)
            {
                var roles = new List<IdentityRole>
                {
                    new IdentityRole("SuperAdministrator")
                };

                DbContext.Roles.AddRange(roles);
                firstRole = roles.Last();
            }
            else
            {
                firstRole = DbContext.Roles.First();
            }

            if (!hasUsers)
            {
                var user = new IdentityUser("Librame");

                DbContext.Users.Add(user);

                DbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = firstRole.Id,
                    UserId = user.Id
                });
            }

            DbContext.SaveChanges();
        }


        public IList<IdentityRole> GetRoles()
        {
            return DbContext.Roles.ToList();
        }

        public IPagingList<IdentityUser> GetUsers()
        {
            return DbContext.Users.AsPagingByIndex(order => order.OrderBy(u => u.Id), 1, 10);
        }


        public ITestStore UseDefaultStore()
        {
            DbContext.TrySwitchConnection(options => options.DefaultConnectionString);

            return this;
        }

        public ITestStore UseWriteStore()
        {
            DbContext.TrySwitchConnection(options => options.WriteConnectionString);

            return this;
        }
    }


    public interface ITestStore : IStore<IdentityBuilderOptions>
    {
        IList<IdentityRole> GetRoles();

        IPagingList<IdentityUser> GetUsers();


        ITestStore UseDefaultStore();

        ITestStore UseWriteStore();
    }
}
