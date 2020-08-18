using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Xunit;

namespace Librame.AspNetCore.IdentityServer.Tests
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

            // Identity
            var roles = stores.GetRoles();
            VerifyDefaultData(roles);

            roles = stores.UseWriteDbConnection().GetRoles();
            Assert.NotEmpty(roles);

            var users = stores.GetUsers();
            VerifyDefaultData(users);

            users = stores.UseWriteDbConnection().GetUsers();
            Assert.NotEmpty(users);

            // IdentityServer
            var apiResources = stores.GetApiResources();
            VerifyDefaultData(apiResources);

            apiResources = stores.GetApiResources();
            Assert.NotEmpty(apiResources);

            var clients = stores.GetClients();
            VerifyDefaultData(clients);

            clients = stores.GetClients();
            Assert.NotEmpty(clients);

            var identityResources = stores.GetIdentityResources();
            VerifyDefaultData(identityResources);

            identityResources = stores.GetIdentityResources();
            Assert.NotEmpty(identityResources);


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
