using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.UI.Tests
{
    using Extensions.Core;

    public class ErrorMessageResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionStringLocalizer<ErrorMessageResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionStringLocalizer<ErrorMessageResource> localizer, string cultureName)
        {
            BuilderGlobalization.RegisterCultureInfos(cultureName);

            var password = localizer[r => r.Password];
            Assert.False(password.ResourceNotFound);

            var confirmPassword = localizer[r => r.ConfirmPassword];
            Assert.False(confirmPassword.ResourceNotFound);
        }

    }
}
