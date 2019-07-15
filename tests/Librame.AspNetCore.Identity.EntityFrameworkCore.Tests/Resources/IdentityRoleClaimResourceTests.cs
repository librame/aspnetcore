using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityRoleClaimResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionStringLocalizer<IdentityRoleClaimResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionStringLocalizer<IdentityRoleClaimResource> localizer, string cultureName)
        {
            BuilderGlobalization.RegisterCultureInfos(cultureName);

            var id = localizer[r => r.Id];
            Assert.False(id.ResourceNotFound);

            var roleId = localizer[r => r.RoleId];
            Assert.False(roleId.ResourceNotFound);

            var claimType = localizer[r => r.ClaimType];
            Assert.False(claimType.ResourceNotFound);

            var claimValue = localizer[r => r.ClaimValue];
            Assert.False(claimValue.ResourceNotFound);
        }

    }
}
