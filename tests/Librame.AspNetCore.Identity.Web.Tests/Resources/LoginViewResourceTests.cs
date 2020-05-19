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

            var title = localizer.GetString(r => r.Title);
            Assert.False(title.ResourceNotFound);

            var descr = localizer.GetString(r => r.Descr);
            Assert.False(descr.ResourceNotFound);

            var buttonText = localizer.GetString(r => r.ButtonText);
            Assert.False(buttonText.ResourceNotFound);

            var forgotPassword = localizer.GetString(r => r.ForgotPassword);
            Assert.False(forgotPassword.ResourceNotFound);

            var registerUser = localizer.GetString(r => r.RegisterUser);
            Assert.False(registerUser.ResourceNotFound);
        }

    }
}
