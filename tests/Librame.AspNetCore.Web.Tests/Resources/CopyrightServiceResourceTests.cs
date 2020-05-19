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

            var copyright = localizer.GetString(r => r.Copyright);
            Assert.False(copyright.ResourceNotFound);

            var poweredBy = localizer.GetString(r => r.PoweredBy);
            Assert.False(poweredBy.ResourceNotFound);

            var culture = localizer.GetString(r => r.Culture);
            Assert.False(culture.ResourceNotFound);

            var application = localizer.GetString(r => r.Application);
            Assert.False(application.ResourceNotFound);

            var themepack = localizer.GetString(r => r.Themepack);
            Assert.False(themepack.ResourceNotFound);

            var searchInNuget = localizer.GetString(r => r.SearchInNuget);
            Assert.False(searchInNuget.ResourceNotFound);

            var gotoMicrosoft = localizer.GetString(r => r.GotoMicrosoft);
            Assert.False(gotoMicrosoft.ResourceNotFound);
        }

    }
}
