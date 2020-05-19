using IdentityServer4.Models;
using Xunit;

namespace Librame.AspNetCore.IdentityServer.Tests
{
    using Extensions.Data;

    public class IdentityServerTableDescriptorExtensionsTests
    {
        [Fact]
        public void InsertIdentityPrefixTest()
        {
            var prefix = nameof(IdentityServer);

            var table = TableDescriptor.Create<Client>();
            Assert.Equal($"{prefix}_Clients", table.InsertIdentityServerPrefix());

            table = TableDescriptor.Create<IdentityResource>();
            Assert.Equal($"{prefix}_IdentityResources", table.InsertIdentityServerPrefix());

            table = TableDescriptor.Create<ApiResource>();
            Assert.Equal($"{prefix}_ApiResources", table.InsertIdentityServerPrefix());

            table = TableDescriptor.Create<PersistedGrant>();
            Assert.Equal($"{prefix}_PersistedGrants", table.InsertIdentityServerPrefix());
        }

    }
}
