using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions;
    using Resources;

    public class LoginViewResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var localizer = TestServiceProvider.Current.GetRequiredService<IEnhancedStringLocalizer<LoginViewResource>>();

            // en-US
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");

            var title = localizer[r => r.Title];
            Assert.NotEmpty(title.Value);

            var descr = localizer[r => r.Descr];
            Assert.NotEmpty(descr.Value);

            var buttonText = localizer[r => r.ButtonText];
            Assert.NotEmpty(buttonText.Value);

            var forgotPassword = localizer[r => r.ForgotPassword];
            Assert.NotEmpty(forgotPassword.Value);

            var registerUser = localizer[r => r.RegisterUser];
            Assert.NotEmpty(registerUser.Value);
        }

    }
}
