using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityRoleResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<IdentityRoleResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<IdentityRoleResource> localizer, string cultureName)
        {
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var id = localizer.GetString(r => r.Id);
            Assert.False(id.ResourceNotFound);

            var normalizedName = localizer.GetString(r => r.NormalizedName);
            Assert.False(normalizedName.ResourceNotFound);

            var name = localizer.GetString(r => r.Name);
            Assert.False(name.ResourceNotFound);

            var concurrencyStamp = localizer.GetString(r => r.ConcurrencyStamp);
            Assert.False(concurrencyStamp.ResourceNotFound);
        }

    }
}
