using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using AspNetCore.Identity.Stores;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;
    using Extensions.Data.Stores;

    public class TestStoreHub : IdentityStoreHub
    {
        public TestStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }


        public IList<DefaultIdentityRole<Guid, Guid>> GetRoles()
            => Accessor.Roles.ToList();

        public IPageable<DefaultIdentityUser<Guid, Guid>> GetUsers()
            => Accessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);

    }
}
