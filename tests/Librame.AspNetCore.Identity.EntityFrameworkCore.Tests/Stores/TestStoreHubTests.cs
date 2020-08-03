using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions;
    using Extensions.Core.Services;
    using Extensions.Data.Builders;

    public class TestStoreHubTests
    {
        [Fact]
        public void AllTest()
        {
            var stores = TestServiceProvider.Current.GetRequiredService<TestStoreHub>();
            var dependency = TestServiceProvider.Current.GetRequiredService<DataBuilderDependency>();

            var roles = stores.GetRoles();
            VerifyDefaultData(roles);

            roles = stores.UseWriteDbConnection().GetRoles();
            Assert.NotEmpty(roles);

            var users = stores.GetUsers();
            VerifyDefaultData(users);

            users = stores.UseWriteDbConnection().GetUsers();
            Assert.NotEmpty(users);


            void VerifyDefaultData<TEntity>(IEnumerable<TEntity> items)
                where TEntity : class
            {
                Assert.True(dependency.Options.DefaultTenant.DataSynchronization
                    ? items.IsNotEmpty()
                    : items.IsEmpty());
            }
        }

    }
}
