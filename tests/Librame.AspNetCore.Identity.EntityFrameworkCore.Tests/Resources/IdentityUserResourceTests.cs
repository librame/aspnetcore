using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
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
            CultureInfo.CurrentCulture
                = CultureInfo.CurrentUICulture
                = new CultureInfo(cultureName);

            var id = localizer.GetString(r => r.Id);
            Assert.False(id.ResourceNotFound);

            var accessFailedCount = localizer.GetString(r => r.AccessFailedCount);
            Assert.False(accessFailedCount.ResourceNotFound);

            var passwordHash = localizer.GetString(r => r.PasswordHash);
            Assert.False(passwordHash.ResourceNotFound);

            var normalizedUserName = localizer.GetString(r => r.NormalizedUserName);
            Assert.False(normalizedUserName.ResourceNotFound);

            var userName = localizer.GetString(r => r.UserName);
            Assert.False(userName.ResourceNotFound);

            var normalizedEmail = localizer.GetString(r => r.NormalizedEmail);
            Assert.False(normalizedEmail.ResourceNotFound);

            var email = localizer.GetString(r => r.Email);
            Assert.False(email.ResourceNotFound);

            var emailConfirmed = localizer.GetString(r => r.EmailConfirmed);
            Assert.False(emailConfirmed.ResourceNotFound);

            var phoneNumber = localizer.GetString(r => r.PhoneNumber);
            Assert.False(phoneNumber.ResourceNotFound);

            var phoneNumberConfirmed = localizer.GetString(r => r.PhoneNumberConfirmed);
            Assert.False(phoneNumberConfirmed.ResourceNotFound);

            var lockoutEnabled = localizer.GetString(r => r.LockoutEnabled);
            Assert.False(lockoutEnabled.ResourceNotFound);

            var lockoutEnd = localizer.GetString(r => r.LockoutEnd);
            Assert.False(lockoutEnd.ResourceNotFound);

            var twoFactorEnabled = localizer.GetString(r => r.TwoFactorEnabled);
            Assert.False(twoFactorEnabled.ResourceNotFound);

            var concurrencyStamp = localizer.GetString(r => r.ConcurrencyStamp);
            Assert.False(concurrencyStamp.ResourceNotFound);

            var securityStamp = localizer.GetString(r => r.SecurityStamp);
            Assert.False(securityStamp.ResourceNotFound);
        }

    }
}
