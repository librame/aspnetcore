using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Data;

    public interface ITestStore : IBaseStore
    {
        IList<DefaultIdentityRole> GetRoles();

        IPagingList<DefaultIdentityUser> GetUsers();


        /// <summary>
        /// 供手动切换读取写入库测试。
        /// </summary>
        /// <returns></returns>
        ITestStore UseWriteDbConnection();

        /// <summary>
        /// 供手动切换读取写入库测试。
        /// </summary>
        /// <returns></returns>
        ITestStore UseDefaultDbConnection();
    }


    public class TestStore : AbstractBaseStore, ITestStore
    {
        public TestStore(IIdentifierService identifier, IAccessor accessor)
            : base(accessor)
        {
            Initialize(identifier);
        }

        private async void Initialize(IIdentifierService identifier)
        {
            if (Accessor is IdentityDbContextAccessor dbContextAccessor)
            {
                UseWriteDbConnection();

                var hasRoles = dbContextAccessor.Roles.IsNotNullOrEmpty();
                var hasUsers = dbContextAccessor.Users.IsNotNullOrEmpty();

                if (hasRoles && hasUsers)
                {
                    UseDefaultDbConnection();
                    return;
                }

                DefaultIdentityRole firstRole;

                if (!hasRoles)
                {
                    var roles = new List<DefaultIdentityRole>
                    {
                        new DefaultIdentityRole("SuperAdministrator")
                        {
                            Id = await identifier.GetIdAsync()
                        }
                    };

                    dbContextAccessor.Roles.AddRange(roles);
                    firstRole = roles.Last();
                }
                else
                {
                    firstRole = dbContextAccessor.Roles.First();
                }

                if (!hasUsers)
                {
                    var user = new DefaultIdentityUser("Librame")
                    {
                        Id = await identifier.GetIdAsync()
                    };

                    dbContextAccessor.Users.Add(user);

                    dbContextAccessor.UserRoles.Add(new IdentityUserRole<string>
                    {
                        RoleId = firstRole.Id,
                        UserId = user.Id
                    });
                }

                dbContextAccessor.SaveChanges();
            }
        }


        public IList<DefaultIdentityRole> GetRoles()
        {
            if (Accessor is IdentityDbContextAccessor dbContextAccessor)
                return dbContextAccessor.Roles.ToList();

            return null;
        }

        public IPagingList<DefaultIdentityUser> GetUsers()
        {
            if (Accessor is IdentityDbContextAccessor dbContextAccessor)
                return dbContextAccessor.Users.AsPagingListByIndex(order => order.OrderBy(u => u.Id), 1, 10);

            return null;
        }


        public ITestStore UseDefaultDbConnection()
        {
            Accessor.ChangeDbConnection(t => t.DefaultConnectionString);

            return this;
        }

        public ITestStore UseWriteDbConnection()
        {
            Accessor.ChangeDbConnection(t => t.WriteConnectionString);

            return this;
        }
    }
}
