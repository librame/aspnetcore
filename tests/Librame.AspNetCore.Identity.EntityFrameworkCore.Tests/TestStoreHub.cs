using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.Identity.Tests
{
    using AspNetCore.Identity.Accessors;
    using AspNetCore.Identity.Stores;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;
    using Extensions.Data.Stores;

    public class TestStoreHub : DataStoreHub<Guid, int, Guid>
    {
        private readonly IdentityDbContextAccessor _currentAccessor;


        public TestStoreHub(IStoreInitializer<Guid> initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
            _currentAccessor = accessor as IdentityDbContextAccessor;
        }


        public IList<DefaultIdentityRole<Guid, Guid>> GetRoles()
            => _currentAccessor.Roles.ToList();

        public IPageable<DefaultIdentityUser<Guid, Guid>> GetUsers()
            => _currentAccessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);

    }
}
