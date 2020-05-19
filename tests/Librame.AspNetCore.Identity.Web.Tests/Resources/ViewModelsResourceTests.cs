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

            var email = localizer.GetString(r => r.Email);
            Assert.False(email.ResourceNotFound);

            var password = localizer.GetString(r => r.Password);
            Assert.False(password.ResourceNotFound);

            var confirmPassword = localizer.GetString(r => r.ConfirmPassword);
            Assert.False(confirmPassword.ResourceNotFound);

            var newPassword = localizer.GetString(r => r.NewPassword);
            Assert.False(newPassword.ResourceNotFound);

            var confirmNewPassword = localizer.GetString(r => r.ConfirmNewPassword);
            Assert.False(confirmNewPassword.ResourceNotFound);

            var oldPassword = localizer.GetString(r => r.OldPassword);
            Assert.False(oldPassword.ResourceNotFound);

            var phoneNumber = localizer.GetString(r => r.Phone);
            Assert.False(phoneNumber.ResourceNotFound);

            var rememberMe = localizer.GetString(r => r.RememberMe);
            Assert.False(rememberMe.ResourceNotFound);
        }

    }
}
