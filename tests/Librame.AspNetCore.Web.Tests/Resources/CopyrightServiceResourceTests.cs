using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Web.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class CopyrightServiceResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<CopyrightServiceResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<CopyrightServiceResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Copyright);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.PoweredBy);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Culture);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Application);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Themepack);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.SearchInNuget);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.GotoMicrosoft);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
