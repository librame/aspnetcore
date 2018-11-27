using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions;
    using Resources;

    public class ViewModelsResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var localizer = TestServiceProvider.Current.GetRequiredService<IEnhancedStringLocalizer<ViewModelsResource>>();

            // en-US
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            var email = localizer[r => r.Email];
            Assert.NotEmpty(email.Value);

            var password = localizer[r => r.Password];
            Assert.NotEmpty(password.Value);

            var confirmPassword = localizer[r => r.ConfirmPassword];
            Assert.NotEmpty(confirmPassword.Value);

            var newPassword = localizer[r => r.NewPassword];
            Assert.NotEmpty(newPassword.Value);

            var confirmNewPassword = localizer[r => r.ConfirmNewPassword];
            Assert.NotEmpty(confirmNewPassword.Value);

            var oldPassword = localizer[r => r.OldPassword];
            Assert.NotEmpty(oldPassword.Value);

            var phoneNumber = localizer[r => r.PhoneNumber];
            Assert.NotEmpty(phoneNumber.Value);

            var rememberBrowser = localizer[r => r.RememberBrowser];
            Assert.NotEmpty(newPassword.Value);

            var rememberMe = localizer[r => r.RememberMe];
            Assert.NotEmpty(rememberMe.Value);
        }

    }
}
