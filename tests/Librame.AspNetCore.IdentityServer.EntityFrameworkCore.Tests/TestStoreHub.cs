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

    public class TestStoreHub : IdentityStoreHub<IdentityServerDbContextAccessor>
    {
        public TestStoreHub(IAccessor accessor)
            : base(accessor)
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
