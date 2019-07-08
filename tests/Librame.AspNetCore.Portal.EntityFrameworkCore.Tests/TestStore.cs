using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Portal.Tests
{
    using Extensions.Data;

    public interface ITestStore : IPortalStore<PortalDbContextAccessor>
    {
        IList<DefaultPortalRole> GetRoles();

        IPageable<DefaultPortalUser> GetUsers();


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


    public class TestStore : PortalStore<PortalDbContextAccessor>, ITestStore
    {
        public TestStore(IPortalIdentifierService identifierService, IAccessor accessor) // or PortalDbContextAccessor
            : base(accessor)
        {
            Initialize(identifierService);
        }

        private void Initialize(IPortalIdentifierService identifierService)
        {
            UseWriteDbConnection();

            DefaultPortalRole firstRole;

            if (!Accessor.Roles.Any())
            {
                firstRole = new DefaultPortalRole("SuperAdministrator")
                {
                    Id = identifierService.GetRoleIdAsync().Result
                };

                Accessor.Roles.Add(firstRole);
            }
            else
            {
                firstRole = Accessor.Roles.First();
            }

            if (!Accessor.Users.Any())
            {
                var firstUser = new DefaultPortalUser("Librame")
                {
                    Id = identifierService.GetUserIdAsync().Result
                };

                var result = _signInManager.UserManager.CreateAsync(firstUser, "123456");
                if (result.IsCompletedSuccessfully)
                {
                    Accessor.UserRoles.Add(new PortalUserRole<string>
                    {
                        RoleId = firstRole.Id,
                        UserId = firstUser.Id
                    });
                }
            }

            Accessor.SaveChanges();

            UseDefaultDbConnection();
        }


        public IList<DefaultPortalRole> GetRoles()
        {
            return Accessor.Roles.ToList();
        }

        public IPageable<DefaultPortalUser> GetUsers()
        {
            return Accessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);
        }


        public ITestStore UseDefaultDbConnection()
        {
            Accessor.TryChangeDbConnection(t => t.DefaultConnectionString);
            return this;
        }

        public ITestStore UseWriteDbConnection()
        {
            Accessor.TryChangeDbConnection(t => t.WriteConnectionString);
            return this;
        }
    }
}
