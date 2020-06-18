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
            Assert.NotEmpty(roles);

            var users = stores.GetUsers();
            Assert.NotEmpty(users);
        }

    }
}
