using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.IdentityServer.UI.Tests
{
    using Extensions.Core;

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
            CultureUtility.Register(new CultureInfo(cultureName));

            var profile = localizer.GetString(r => r.Profile);
            Assert.False(profile.ResourceNotFound);

            var changePassword = localizer.GetString(r => r.ChangePassword);
            Assert.False(changePassword.ResourceNotFound);

            var externalLogins = localizer.GetString(r => r.ExternalLogins);
            Assert.False(externalLogins.ResourceNotFound);

            var twoFactorAuthentication = localizer.GetString(r => r.TwoFactorAuthentication);
            Assert.False(twoFactorAuthentication.ResourceNotFound);

            var personalData = localizer.GetString(r => r.PersonalData);
            Assert.False(personalData.ResourceNotFound);

            var repository = localizer.GetString(r => r.Repository);
            Assert.False(repository.ResourceNotFound);

            var issues = localizer.GetString(r => r.Issues);
            Assert.False(issues.ResourceNotFound);

            var licenses = localizer.GetString(r => r.Licenses);
            Assert.False(licenses.ResourceNotFound);
        }

    }
}
