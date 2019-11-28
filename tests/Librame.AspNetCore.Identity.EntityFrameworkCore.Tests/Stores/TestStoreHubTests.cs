using Librame.Extensions.Core;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
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
        }

    }
}
