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
            var localizer = TestServiceProvider.Current.GetRequiredService<IEnhancedStringLocalizer<ErrorMessageResource>>();

            // en-US
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            var password = localizer[r => r.Password];
            Assert.NotEmpty(password.Value);

            var confirmPassword = localizer[r => r.ConfirmPassword];
            Assert.NotEmpty(confirmPassword.Value);
        }

    }
}
