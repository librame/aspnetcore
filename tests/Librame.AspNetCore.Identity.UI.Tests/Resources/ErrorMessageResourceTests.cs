using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
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
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionLocalizer<ErrorMessageResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionLocalizer<ErrorMessageResource> localizer, string cultureName)
        {
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var password = localizer[r => r.Password];
            Assert.False(password.ResourceNotFound);

            var confirmPassword = localizer[r => r.ConfirmPassword];
            Assert.False(confirmPassword.ResourceNotFound);
        }

    }
}
