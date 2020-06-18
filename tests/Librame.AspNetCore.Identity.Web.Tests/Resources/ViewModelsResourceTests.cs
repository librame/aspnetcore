using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Web.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class ViewModelsResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<UserViewModelResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<UserViewModelResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Email);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Password);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ConfirmPassword);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.NewPassword);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ConfirmNewPassword);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.OldPassword);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Phone);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.RememberMe);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
