using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class IdentityRoleClaimResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<IdentityRoleClaimResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<IdentityRoleClaimResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Id);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.RoleId);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ClaimType);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ClaimValue);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
