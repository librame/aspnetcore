using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Web.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class ErrorMessageResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<ErrorMessageResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<ErrorMessageResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Password);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ConfirmPassword);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
