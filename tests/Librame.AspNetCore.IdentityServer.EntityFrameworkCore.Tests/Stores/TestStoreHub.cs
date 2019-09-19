using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions.Data;

    public class TestStoreHub : StoreHubBase<IdentityServerDbContextAccessor>
    {
        public TestStoreHub(IAccessor accessor, IStoreInitializer<IdentityServerDbContextAccessor> initializer)
            : base(accessor, initializer)
        {
        }


        //public IList<DefaultIdentityRole<string>> GetRoles()
        //{
        //    return Accessor.Roles.ToList();
        //}

        //public IPageable<DefaultIdentityUser<string>> GetUsers()
        //{
        //    return Accessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);
        //}


        public TestStoreHub UseDefaultDbConnection()
        {
            Accessor.ToggleTenant(t => t.DefaultConnectionString);
            return this;
        }

        public TestStoreHub UseWriteDbConnection()
        {
            Accessor.ToggleTenant(t => t.WritingConnectionString);
            return this;
        }
    }
}
