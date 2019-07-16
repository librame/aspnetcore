using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Portal.Tests
{
    using Extensions.Data;

    public class PortalDbContextAccessorTests
    {
        [Fact]
        public void AllTest()
        {
            using (var stores = TestServiceProvider.Current.GetRequiredService<ITestStoreHub>())
            {
                var initializer = stores.GetRequiredService<IInitializerService<PortalDbContextAccessor>>();
                initializer.InitializeService(stores);

                var claims = stores.GetClaims();
                Assert.Empty(claims);

                claims = stores.UseWriteDbConnection().GetClaims();
                Assert.NotEmpty(claims);

                var categories = stores.UseDefaultDbConnection().GetCategories();
                Assert.Empty(categories);

                categories = stores.UseWriteDbConnection().GetCategories();
                Assert.NotEmpty(categories);

                var panes = stores.UseDefaultDbConnection().GetPanes();
                Assert.Empty(panes);

                panes = stores.UseWriteDbConnection().GetPanes();
                Assert.NotEmpty(panes);

                var sources = stores.UseDefaultDbConnection().GetSources();
                Assert.Empty(sources);

                sources = stores.UseWriteDbConnection().GetSources();
                Assert.NotEmpty(sources);
            }
        }

    }
}
