using Librame.Extensions.Core;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Librame.AspNetCore.Portal.Tests
{
    public class PortalIdentifierServiceTests
    {
        class TestPortalIdentifierService : AbstractDisposable, IPortalIdentifierService
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

            public Task<string> GetTagClaimIdAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Task<string> GetTagIdAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }
        }


        [Fact]
        public void AllTest()
        {
            IPortalIdentifierService test = new TestPortalIdentifierService();

            Assert.ThrowsAsync<NotImplementedException>(() => test.GetAuditIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetAuditPropertyIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetTenantIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetTagClaimIdAsync());
            Assert.ThrowsAsync<NotImplementedException>(() => test.GetTagIdAsync());
        }

    }
}
