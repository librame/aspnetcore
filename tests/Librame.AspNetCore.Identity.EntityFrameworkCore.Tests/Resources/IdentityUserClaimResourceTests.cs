using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class IdentityUserClaimResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<IdentityUserClaimResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<IdentityUserClaimResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var id = localizer.GetString(r => r.Id);
            Assert.False(id.ResourceNotFound);

            var userId = localizer.GetString(r => r.UserId);
            Assert.False(userId.ResourceNotFound);

            var claimType = localizer.GetString(r => r.ClaimType);
            Assert.False(claimType.ResourceNotFound);

            var claimValue = localizer.GetString(r => r.ClaimValue);
            Assert.False(claimValue.ResourceNotFound);
        }

    }
}
