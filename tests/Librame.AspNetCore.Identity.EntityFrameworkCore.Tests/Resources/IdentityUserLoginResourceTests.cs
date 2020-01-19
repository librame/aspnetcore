using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class IdentityUserLoginResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<IdentityUserLoginResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<IdentityUserLoginResource> localizer, string cultureName)
        {
            CultureUtility.Register(new CultureInfo(cultureName));

            var userId = localizer.GetString(r => r.UserId);
            Assert.False(userId.ResourceNotFound);

            var loginProvider = localizer.GetString(r => r.LoginProvider);
            Assert.False(loginProvider.ResourceNotFound);

            var providerKey = localizer.GetString(r => r.ProviderKey);
            Assert.False(providerKey.ResourceNotFound);

            var providerDisplayName = localizer.GetString(r => r.ProviderDisplayName);
            Assert.False(providerDisplayName.ResourceNotFound);
        }

    }
}
