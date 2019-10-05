using Microsoft.Extensions.DependencyInjection;
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
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionLocalizer<LayoutViewResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionLocalizer<LayoutViewResource> localizer, string cultureName)
        {
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var profile = localizer[r => r.Profile];
            Assert.False(profile.ResourceNotFound);

            var changePassword = localizer[r => r.ChangePassword];
            Assert.False(changePassword.ResourceNotFound);

            var externalLogins = localizer[r => r.ExternalLogins];
            Assert.False(externalLogins.ResourceNotFound);

            var twoFactorAuthentication = localizer[r => r.TwoFactorAuthentication];
            Assert.False(twoFactorAuthentication.ResourceNotFound);

            var personalData = localizer[r => r.PersonalData];
            Assert.False(personalData.ResourceNotFound);

            var repository = localizer[r => r.Repository];
            Assert.False(repository.ResourceNotFound);

            var issues = localizer[r => r.Issues];
            Assert.False(issues.ResourceNotFound);

            var licenses = localizer[r => r.Licenses];
            Assert.False(licenses.ResourceNotFound);
        }

    }
}
