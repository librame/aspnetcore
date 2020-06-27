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

    public class TestStoreHub : IdentityStoreHub<IdentityServerDbContextAccessor, Guid, int, Guid>
    {
        public TestStoreHub(IStoreInitializer initializer, IAccessor accessor)
            : base(initializer, accessor)
        {
        }


        public IList<DefaultIdentityRole<Guid, Guid>> GetRoles()
            => Accessor.Roles.ToList();

        public IPageable<DefaultIdentityUser<Guid, Guid>> GetUsers()
            => Accessor.Users.AsPagingByIndex(ordered => ordered.OrderBy(k => k.Id), 1, 10);


        public IList<ApiResource> GetApiResources()
            => Accessor.ApiResources.ToList();

        public IList<Client> GetClients()
            => Accessor.Clients.ToList();

        public IList<IdentityResource> GetIdentityResources()
            => Accessor.IdentityResources.ToList();

    }
}
