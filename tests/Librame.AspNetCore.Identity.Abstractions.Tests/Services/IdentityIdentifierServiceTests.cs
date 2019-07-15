using Librame.Extensions.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityIdentifierServiceTests
    {
        class TestIdentityIdentifierService : AbstractDisposable, IIdentityIdentifierService
        {
            public Task<string> GetAuditIdAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetAuditPropertyIdAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetTenantIdAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetRoleIdAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetUserIdAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void AllTest()
        {
            IIdentityIdentifierService test = new TestIdentityIdentifierService();

            Assert.ThrowsAsync<NotImplementedException>(() => test.GetAuditIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetAuditPropertyIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetTenantIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetRoleIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetUserIdAsync());
        }

    }
}
