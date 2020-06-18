using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using AspNetCore.Identity.Stores;
    using AspNetCore.IdentityServer.Accessors;
    using Extensions.Data.Accessors;
    using Extensions.Data.Collections;
    using Extensions.Data.Stores;

    public class TestStoreHub : DataStoreHub<Guid, int, Guid>
    {
        private readonly IdentityServerDbContextAccessor _currentAccessor;


        public TestStoreHub(IStoreInitializer<Guid> initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
            _currentAccessor = accessor as IdentityServerDbContextAccessor;
        }


        public IList<DefaultIdentityRole<Guid, Guid>> GetRoles()
            => _currentAccessor.Roles.ToList();

        public IPageable<DefaultIdentityUser<Guid, Guid>> GetUsers()
            => _currentAccessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);


        public IList<ApiResource> GetApiResources()
            => _currentAccessor.ApiResources.ToList();

        public IList<Client> GetClients()
            => _currentAccessor.Clients.ToList();

        public IList<IdentityResource> GetIdentityResources()
            => _currentAccessor.IdentityResources.ToList();

    }
}
