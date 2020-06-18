using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Web.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class ProjectNavigationResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<ProjectNavigationResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<ProjectNavigationResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Index);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.About);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Contact);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Privacy);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Sitemap);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Repository);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Issues);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Licenses);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.AccessDenied);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Register);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Login);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Logout);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Manage);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
