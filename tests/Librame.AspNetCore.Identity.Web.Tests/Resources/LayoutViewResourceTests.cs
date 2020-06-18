using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Web.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class LayoutViewResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<LayoutViewResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<LayoutViewResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Profile);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ChangePassword);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ExternalLogins);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.TwoFactorAuthentication);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.PersonalData);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
