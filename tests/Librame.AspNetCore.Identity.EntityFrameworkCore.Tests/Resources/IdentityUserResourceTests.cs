using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    using Extensions.Core.Utilities;
    using Resources;

    public class IdentityUserResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IStringLocalizer<IdentityUserResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IStringLocalizer<IdentityUserResource> localizer, string cultureName)
        {
            CultureInfoUtility.Register(new CultureInfo(cultureName));

            var localized = localizer.GetString(r => r.Id);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.AccessFailedCount);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.PasswordHash);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.NormalizedUserName);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.UserName);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.NormalizedEmail);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.Email);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.EmailConfirmed);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.PhoneNumber);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.PhoneNumberConfirmed);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.LockoutEnabled);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.LockoutEnd);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.TwoFactorEnabled);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.ConcurrencyStamp);
            Assert.False(localized.ResourceNotFound);

            localized = localizer.GetString(r => r.SecurityStamp);
            Assert.False(localized.ResourceNotFound);
        }

    }
}
