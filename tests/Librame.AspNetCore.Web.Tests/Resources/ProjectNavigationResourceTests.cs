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

            var index = localizer.GetString(r => r.Index);
            Assert.False(index.ResourceNotFound);

            var about = localizer.GetString(r => r.About);
            Assert.False(about.ResourceNotFound);

            var contact = localizer.GetString(r => r.Contact);
            Assert.False(contact.ResourceNotFound);

            var privacy = localizer.GetString(r => r.Privacy);
            Assert.False(privacy.ResourceNotFound);

            var sitemap = localizer.GetString(r => r.Sitemap);
            Assert.False(sitemap.ResourceNotFound);

            var repository = localizer.GetString(r => r.Repository);
            Assert.False(repository.ResourceNotFound);

            var issues = localizer.GetString(r => r.Issues);
            Assert.False(issues.ResourceNotFound);

            var licenses = localizer.GetString(r => r.Licenses);
            Assert.False(licenses.ResourceNotFound);

            var accessDenied = localizer.GetString(r => r.AccessDenied);
            Assert.False(accessDenied.ResourceNotFound);

            var register = localizer.GetString(r => r.Register);
            Assert.False(register.ResourceNotFound);

            var login = localizer.GetString(r => r.Login);
            Assert.False(login.ResourceNotFound);

            var logout = localizer.GetString(r => r.Logout);
            Assert.False(logout.ResourceNotFound);

            var manage = localizer.GetString(r => r.Manage);
            Assert.False(manage.ResourceNotFound);
        }

    }
}
