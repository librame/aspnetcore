using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using AspNetCore.Identity.Stores;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;

    public class TestStoreHub : IdentityStoreHub
    {
        public TestStoreHub(IAccessor accessor)
            : base(accessor)
        {
        }


        public IList<DefaultIdentityRole<Guid, Guid>> GetRoles()
            => Accessor.Roles.ToList();

        public IPageable<DefaultIdentityUser<Guid, Guid>> GetUsers()
            => Accessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);


        public TestStoreHub UseWriteDbConnection()
        {
            Accessor.ChangeConnectionString(t => t.WritingConnectionString);
            return this;
        }

        public TestStoreHub UseDefaultDbConnection()
        {
            Accessor.ChangeConnectionString(t => t.DefaultConnectionString);
            return this;
        }

    }
}
