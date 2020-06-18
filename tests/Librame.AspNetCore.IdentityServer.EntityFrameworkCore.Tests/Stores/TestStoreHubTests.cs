using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions.Core.Services;

    public class TestStoreHubTests
    {
        [Fact]
        public void AllTest()
        {
            var stores = TestServiceProvider.Current.GetRequiredService<TestStoreHub>();

            // Identity
            var roles = stores.GetRoles();
            Assert.NotEmpty(roles);

            var users = stores.GetUsers();
            Assert.NotEmpty(users);

            // IdentityServer
            var apiResources = stores.GetApiResources();
            Assert.NotEmpty(apiResources);

            var clients = stores.GetClients();
            Assert.NotEmpty(clients);

            var identities = stores.GetIdentityResources();
            Assert.NotEmpty(identities);
        }

    }
}
