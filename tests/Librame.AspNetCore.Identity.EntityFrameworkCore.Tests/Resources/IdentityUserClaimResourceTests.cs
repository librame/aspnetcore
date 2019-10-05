using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityUserClaimResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionLocalizer<IdentityUserClaimResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionLocalizer<IdentityUserClaimResource> localizer, string cultureName)
        {
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var id = localizer[r => r.Id];
            Assert.False(id.ResourceNotFound);

            var userId = localizer[r => r.UserId];
            Assert.False(userId.ResourceNotFound);

            var claimType = localizer[r => r.ClaimType];
            Assert.False(claimType.ResourceNotFound);

            var claimValue = localizer[r => r.ClaimValue];
            Assert.False(claimValue.ResourceNotFound);
        }

    }
}
