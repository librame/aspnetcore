using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Web.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class LoginViewResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<LoginViewResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<LoginViewResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Title);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Descr);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ButtonText);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ForgotPassword);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.RegisterUser);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
