using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityDbContextAccessorTests
    {
        [Fact]
        public void AllTest()
        {
            using (var store = TestServiceProvider.Current.GetRequiredService<ITestStore>())
            {
                var roles = store.GetRoles();
                Assert.Empty(roles);

                roles = store.UseWriteDbConnection().GetRoles();
                Assert.NotEmpty(roles);

                var users = store.UseDefaultDbConnection().GetUsers();
                Assert.Empty(users);

                users = store.UseWriteDbConnection().GetUsers();
                Assert.NotEmpty(users);
            }
        }

    }
}
