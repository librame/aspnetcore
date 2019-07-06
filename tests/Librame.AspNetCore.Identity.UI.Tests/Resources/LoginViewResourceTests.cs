using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions.Core;

    public class LoginViewResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionStringLocalizer<LoginViewResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionStringLocalizer<LoginViewResource> localizer, string cultureName)
        {
            BuilderGlobalization.RegisterCultureInfos(cultureName);

            var title = localizer[r => r.Title];
            Assert.False(title.ResourceNotFound);

            var descr = localizer[r => r.Descr];
            Assert.False(descr.ResourceNotFound);

            var buttonText = localizer[r => r.ButtonText];
            Assert.False(buttonText.ResourceNotFound);

            var forgotPassword = localizer[r => r.ForgotPassword];
            Assert.False(forgotPassword.ResourceNotFound);

            var registerUser = localizer[r => r.RegisterUser];
            Assert.False(registerUser.ResourceNotFound);
        }

    }
}