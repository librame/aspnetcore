using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class IdentityUserRoleResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<IdentityUserRoleResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<IdentityUserRoleResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var userId = localizer.GetString(r => r.UserId);
            Assert.False(userId.ResourceNotFound);

            var roleId = localizer.GetString(r => r.RoleId);
            Assert.False(roleId.ResourceNotFound);
        }

    }
}
