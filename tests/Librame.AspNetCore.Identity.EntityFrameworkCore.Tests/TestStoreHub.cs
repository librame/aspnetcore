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

    public class TestStoreHub : StoreHub<Guid, int>
    {
        private readonly IdentityDbContextAccessor _currentAccessor;


        public TestStoreHub(IStoreInitializer<Guid> initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
            _currentAccessor = accessor as IdentityDbContextAccessor;
        }


        public IList<DefaultIdentityRole<Guid>> GetRoles()
            => _currentAccessor.Roles.ToList();

        public IPageable<DefaultIdentityUser<Guid>> GetUsers()
            => _currentAccessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);


        public TestStoreHub UseDefaultDbConnection()
        {
            _currentAccessor.ChangeConnectionString(t => t.DefaultConnectionString);
            return this;
        }

        public TestStoreHub UseWriteDbConnection()
        {
            _currentAccessor.ChangeConnectionString(t => t.WritingConnectionString);
            return this;
        }

    }
}
