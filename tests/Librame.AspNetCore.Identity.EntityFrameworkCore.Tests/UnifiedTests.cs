using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class UnifiedTests
    {

        [Fact]
        public void UnifiedTest()
        {
            var store = TestServiceProvider.Current.GetRequiredService<ITestStore>();

            var roles = store.GetRoles();
            Assert.Empty(roles);

            // Use Write Database
            roles = store.UseWriteDbConnection().GetRoles();
            Assert.NotEmpty(roles);

            // Restore
            store.UseDefaultDbConnection();

            var users = store.GetUsers();
            Assert.Empty(users);

            // Use Write Database
            users = store.UseWriteDbConnection().GetUsers();
            Assert.NotEmpty(users);

            // Restore
            store.UseDefaultDbConnection();
        }

    }
}
