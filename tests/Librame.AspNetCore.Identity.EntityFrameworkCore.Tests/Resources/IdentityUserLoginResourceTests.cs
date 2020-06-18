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
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.UserId);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.LoginProvider);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ProviderKey);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ProviderDisplayName);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
