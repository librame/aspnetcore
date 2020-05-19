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

            var id = localizer.GetString(r => r.Id);
            Assert.False(id.ResourceNotFound);

            var roleId = localizer.GetString(r => r.RoleId);
            Assert.False(roleId.ResourceNotFound);

            var claimType = localizer.GetString(r => r.ClaimType);
            Assert.False(claimType.ResourceNotFound);

            var claimValue = localizer.GetString(r => r.ClaimValue);
            Assert.False(claimValue.ResourceNotFound);
        }

    }
}
