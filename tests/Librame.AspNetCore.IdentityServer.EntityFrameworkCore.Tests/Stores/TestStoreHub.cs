using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions.Data;
    using Identity;

    public class TestStoreHub : StoreHub<IdentityServerDbContextAccessor, IdentityServerStoreInitializer>
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


        public IList<ApiResource> GetApiResources()
            => Accessor.ApiResources.ToList();

        public IList<Client> GetClients()
            => Accessor.Clients.ToList();

        public IList<IdentityResource> GetIdentityResources()
            => Accessor.IdentityResources.ToList();


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
