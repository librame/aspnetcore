using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityUserRoleResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionLocalizer<IdentityUserRoleResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionLocalizer<IdentityUserRoleResource> localizer, string cultureName)
        {
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var userId = localizer[r => r.UserId];
            Assert.False(userId.ResourceNotFound);

            var roleId = localizer[r => r.RoleId];
            Assert.False(roleId.ResourceNotFound);
        }

    }
}
