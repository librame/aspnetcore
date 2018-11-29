using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions;
    using Resources;

    public class ErrorMessageResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IEnhancedStringLocalizer<ErrorMessageResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IEnhancedStringLocalizer<ErrorMessageResource> localizer, string cultureName)
        {
            CultureInfo.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);

            var password = localizer[r => r.Password];
            Assert.False(password.ResourceNotFound);

            var confirmPassword = localizer[r => r.ConfirmPassword];
            Assert.False(confirmPassword.ResourceNotFound);
        }

    }
}
