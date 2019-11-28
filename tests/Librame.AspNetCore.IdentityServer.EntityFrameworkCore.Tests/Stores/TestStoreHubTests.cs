using Librame.Extensions.Core;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    public class TestStoreHubTests
    {
        [Fact]
        public void AllTest()
        {
            var stores = TestServiceProvider.Current.GetRequiredService<TestStoreHub>();

            var dependencies = stores.ServiceFactory.GetService<MigrationsScaffolderDependencies>();
            Assert.NotNull(dependencies);

            var roles = stores.GetRoles();
            Assert.Empty(roles);

            roles = stores.UseWriteDbConnection().GetRoles();
            Assert.NotEmpty(roles);

            var users = stores.UseDefaultDbConnection().GetUsers();
            Assert.Empty(users);

            users = stores.UseWriteDbConnection().GetUsers();
            Assert.NotEmpty(users);

            var apiResources = stores.UseDefaultDbConnection().GetApiResources();
            Assert.Empty(apiResources);

            apiResources = stores.UseWriteDbConnection().GetApiResources();
            Assert.NotEmpty(apiResources);

            var clients = stores.UseDefaultDbConnection().GetClients();
            Assert.Empty(clients);

            clients = stores.UseWriteDbConnection().GetClients();
            Assert.NotEmpty(clients);

            var identities = stores.UseDefaultDbConnection().GetIdentityResources();
            Assert.Empty(identities);

            identities = stores.UseWriteDbConnection().GetIdentityResources();
            Assert.NotEmpty(identities);
        }

    }
}
