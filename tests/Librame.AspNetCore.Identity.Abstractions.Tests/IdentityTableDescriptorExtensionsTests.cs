using System;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using AspNetCore.Identity.Stores;
    using Extensions.Data;

    public class IdentityTableDescriptorExtensionsTests
    {
        [Fact]
        public void InsertIdentityPrefixTest()
        {
            var prefix = nameof(Identity);

            var table = TableDescriptor.Create<DefaultIdentityRole<Guid, Guid>>();
            Assert.Equal($"{prefix}_Roles", table.InsertIdentityPrefix());

            table = TableDescriptor.Create<DefaultIdentityRoleClaim<Guid, Guid>>();
            Assert.Equal($"{prefix}_RoleClaims", table.InsertIdentityPrefix());

            table = TableDescriptor.Create<DefaultIdentityUser<Guid, Guid>>();
            Assert.Equal($"{prefix}_Users", table.InsertIdentityPrefix());

            table = TableDescriptor.Create<DefaultIdentityUserClaim<Guid, Guid>>();
            Assert.Equal($"{prefix}_UserClaims", table.InsertIdentityPrefix());

            table = TableDescriptor.Create<DefaultIdentityUserLogin<Guid, Guid>>();
            Assert.Equal($"{prefix}_UserLogins", table.InsertIdentityPrefix());

            table = TableDescriptor.Create<DefaultIdentityUserRole<Guid, Guid>>();
            Assert.Equal($"{prefix}_UserRoles", table.InsertIdentityPrefix());

            table = TableDescriptor.Create<DefaultIdentityUserToken<Guid, Guid>>();
            Assert.Equal($"{prefix}_UserTokens", table.InsertIdentityPrefix());
        }

    }
}
