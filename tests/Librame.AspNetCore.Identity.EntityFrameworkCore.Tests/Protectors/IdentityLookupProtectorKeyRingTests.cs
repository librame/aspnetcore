using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityLookupProtectorKeyRingTests
    {
        [Fact]
        public void AllTest()
        {
            var keyRing = TestServiceProvider.Current.GetRequiredService<ILookupProtectorKeyRing>();
            Assert.NotEmpty(keyRing.CurrentKeyId);

            var keyIds = keyRing.GetAllKeyIds();
            Assert.NotEmpty(keyIds);
        }

    }
}
