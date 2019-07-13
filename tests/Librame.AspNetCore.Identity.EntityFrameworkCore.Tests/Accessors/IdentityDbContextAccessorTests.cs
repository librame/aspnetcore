using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Data;

    public class IdentityDbContextAccessorTests
    {
        [Fact]
        public void AllTest()
        {
            using (var stores = TestServiceProvider.Current.GetRequiredService<ITestStoreHub>())
            {
                var initializer = stores.GetRequiredService<IInitializerService<IdentityDbContextAccessor>>();
                initializer.InitializeService(stores);

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
}
