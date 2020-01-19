using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using Accessors;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;
    using Extensions.Data.Stores;
    using Stores;

    public class TestStoreHub : StoreHub<IdentityDbContextAccessor, IdentityStoreInitializer>
    {
        public TestStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }


        public IList<DefaultIdentityRole<string>> GetRoles()
        {
            return Accessor.Roles.ToList();
        }

        public IPageable<DefaultIdentityUser<string>> GetUsers()
        {
            return Accessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);
        }


        public TestStoreHub UseDefaultDbConnection()
        {
            Accessor.ChangeDbConnection(t => t.DefaultConnectionString);
            return this;
        }

        public TestStoreHub UseWriteDbConnection()
        {
            Accessor.ChangeDbConnection(t => t.WritingConnectionString);
            return this;
        }
    }
}
