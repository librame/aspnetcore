using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core.Services;

    public class TestStoreHubTests
    {
        [Fact]
        public void AllTest()
        {
            var stores = TestServiceProvider.Current.GetRequiredService<TestStoreHub>();

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
