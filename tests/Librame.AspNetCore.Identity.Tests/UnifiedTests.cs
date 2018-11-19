using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;

    public class UnifiedTests
    {

        [Fact]
        public void UnifiedTest()
        {
            var store = TestServiceProvider.Current.GetRequiredService<ITestStore>();

            var roles = store.GetRoles();
            Assert.Empty(roles);

            // Use Write Database
            roles = store.UseWriteStore().GetRoles();
            Assert.NotEmpty(roles);

            // Restore
            store.UseDefaultStore();

            var users = store.GetUsers();
            Assert.Empty(users);

            // Use Write Database
            users = store.UseWriteStore().GetUsers();
            Assert.NotEmpty(users);

            // Restore
            store.UseDefaultStore();
        }

    }
}
