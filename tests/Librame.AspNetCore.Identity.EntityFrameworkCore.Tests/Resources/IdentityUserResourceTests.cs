using Librame.Extensions.Core;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class IdentityUserResourceTests
    {
        [Fact]
        public void ResourceTest()
        {
            var cultureNames = new string[] { "en-US", "zh-CN", "zh-TW" };
            var localizer = TestServiceProvider.Current.GetRequiredService<IExpressionStringLocalizer<IdentityUserResource>>();

            foreach (var name in cultureNames)
                RunTest(localizer, name);
        }

        private void RunTest(IExpressionStringLocalizer<IdentityUserResource> localizer, string cultureName)
        {
            BuilderGlobalization.RegisterCultureInfos(cultureName);

            var id = localizer[r => r.Id];
            Assert.False(id.ResourceNotFound);

            var accessFailedCount = localizer[r => r.AccessFailedCount];
            Assert.False(accessFailedCount.ResourceNotFound);

            var passwordHash = localizer[r => r.PasswordHash];
            Assert.False(passwordHash.ResourceNotFound);

            var normalizedUserName = localizer[r => r.NormalizedUserName];
            Assert.False(normalizedUserName.ResourceNotFound);

            var userName = localizer[r => r.UserName];
            Assert.False(userName.ResourceNotFound);

            var normalizedEmail = localizer[r => r.NormalizedEmail];
            Assert.False(normalizedEmail.ResourceNotFound);

            var email = localizer[r => r.Email];
            Assert.False(email.ResourceNotFound);

            var emailConfirmed = localizer[r => r.EmailConfirmed];
            Assert.False(emailConfirmed.ResourceNotFound);

            var phoneNumber = localizer[r => r.PhoneNumber];
            Assert.False(phoneNumber.ResourceNotFound);

            var phoneNumberConfirmed = localizer[r => r.PhoneNumberConfirmed];
            Assert.False(phoneNumberConfirmed.ResourceNotFound);

            var lockoutEnabled = localizer[r => r.LockoutEnabled];
            Assert.False(lockoutEnabled.ResourceNotFound);

            var lockoutEnd = localizer[r => r.LockoutEnd];
            Assert.False(lockoutEnd.ResourceNotFound);

            var twoFactorEnabled = localizer[r => r.TwoFactorEnabled];
            Assert.False(twoFactorEnabled.ResourceNotFound);

            var concurrencyStamp = localizer[r => r.ConcurrencyStamp];
            Assert.False(concurrencyStamp.ResourceNotFound);

            var securityStamp = localizer[r => r.SecurityStamp];
            Assert.False(securityStamp.ResourceNotFound);
        }

    }
}
