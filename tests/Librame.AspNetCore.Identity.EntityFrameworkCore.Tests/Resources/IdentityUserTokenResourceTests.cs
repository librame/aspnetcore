using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityUserTokenResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionStringLocalizer<IdentityUserTokenResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionStringLocalizer<IdentityUserTokenResource> localizer, string cultureName)
        {
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var userId = localizer[r => r.UserId];
            Assert.False(userId.ResourceNotFound);

            var loginProvider = localizer[r => r.LoginProvider];
            Assert.False(loginProvider.ResourceNotFound);

            var name = localizer[r => r.Name];
            Assert.False(name.ResourceNotFound);

            var value = localizer[r => r.Value];
            Assert.False(value.ResourceNotFound);
        }

    }
}
