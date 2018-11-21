using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Data;

    public class TestStore : ITestStore
    {
        private readonly IDefaultIdentityDbContext _dbContext;

        public TestStore(IDefaultIdentityDbContext dbContext)
        {
            _dbContext = dbContext;

            Initialize();
        }

        private void Initialize()
        {
            UseWriteStore();

            var hasRoles = _dbContext.Roles.IsNotEmpty();
            var hasUsers = _dbContext.Users.IsNotEmpty();

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

                _dbContext.Roles.AddRange(roles);
                firstRole = roles.Last();
            }
            else
            {
                firstRole = _dbContext.Roles.First();
            }

            if (!hasUsers)
            {
                var user = new IdentityUser("Librame");
                
                _dbContext.Users.Add(user);

                _dbContext.UserRoles.Add(new IdentityUserRole<string>
                {
                    RoleId = firstRole.Id,
                    UserId = user.Id
                });
            }

            _dbContext.SaveChanges();
        }


        public IList<IdentityRole> GetRoles()
        {
            return _dbContext.Roles.ToList();
        }

        public IPagingList<IdentityUser> GetUsers()
        {
            return _dbContext.Users.AsPagingByIndex(order => order.OrderBy(u => u.Id), 1, 10);
        }


        public ITestStore UseDefaultStore()
        {
            _dbContext.TrySwitchConnection(options => options.DefaultConnectionString);

            return this;
        }

        public ITestStore UseWriteStore()
        {
            _dbContext.TrySwitchConnection(options => options.WriteConnectionString);

            return this;
        }
    }


    public interface ITestStore
    {
        IList<IdentityRole> GetRoles();

        IPagingList<IdentityUser> GetUsers();


        ITestStore UseDefaultStore();

        ITestStore UseWriteStore();
    }
}
