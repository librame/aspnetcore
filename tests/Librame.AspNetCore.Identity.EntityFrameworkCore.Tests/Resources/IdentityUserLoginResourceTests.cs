using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityUserLoginResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionLocalizer<IdentityUserLoginResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionLocalizer<IdentityUserLoginResource> localizer, string cultureName)
        {
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var userId = localizer[r => r.UserId];
            Assert.False(userId.ResourceNotFound);

            var loginProvider = localizer[r => r.LoginProvider];
            Assert.False(loginProvider.ResourceNotFound);

            var providerKey = localizer[r => r.ProviderKey];
            Assert.False(providerKey.ResourceNotFound);

            var providerDisplayName = localizer[r => r.ProviderDisplayName];
            Assert.False(providerDisplayName.ResourceNotFound);
        }

    }
}
