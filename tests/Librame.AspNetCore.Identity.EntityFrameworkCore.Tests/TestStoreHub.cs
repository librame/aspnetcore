using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Data;

    public interface ITestStoreHub : IStoreHub<IdentityDbContextAccessor>
    {
        IList<DefaultIdentityRole> GetRoles();

        IPageable<DefaultIdentityUser> GetUsers();


        /// <summary>
        /// 供手动切换读取写入库测试。
        /// </summary>
        /// <returns></returns>
        ITestStoreHub UseWriteDbConnection();

        /// <summary>
        /// 供手动切换读取写入库测试。
        /// </summary>
        /// <returns></returns>
        ITestStoreHub UseDefaultDbConnection();
    }


    public class TestStoreHub : StoreHubBase<IdentityDbContextAccessor>, ITestStoreHub
    {
        public TestStoreHub(IAccessor accessor) // or IdentityDbContextAccessor
            : base(accessor)
        {
        }


        public IList<DefaultIdentityRole> GetRoles()
        {
            return Accessor.Roles.ToList();
        }

        public IPageable<DefaultIdentityUser> GetUsers()
        {
            return Accessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);
        }


        public ITestStoreHub UseDefaultDbConnection()
        {
            Accessor.TryChangeDbConnection(t => t.DefaultConnectionString);
            return this;
        }

        public ITestStoreHub UseWriteDbConnection()
        {
            Accessor.TryChangeDbConnection(t => t.WritingConnectionString);
            return this;
        }
    }
}
