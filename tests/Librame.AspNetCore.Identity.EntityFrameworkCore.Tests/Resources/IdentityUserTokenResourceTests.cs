using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class IdentityUserTokenResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<IdentityUserTokenResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<IdentityUserTokenResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var userId = localizer.GetString(r => r.UserId);
            Assert.False(userId.ResourceNotFound);

            var loginProvider = localizer.GetString(r => r.LoginProvider);
            Assert.False(loginProvider.ResourceNotFound);

            var name = localizer.GetString(r => r.Name);
            Assert.False(name.ResourceNotFound);

            var value = localizer.GetString(r => r.Value);
            Assert.False(value.ResourceNotFound);
        }

    }
}
