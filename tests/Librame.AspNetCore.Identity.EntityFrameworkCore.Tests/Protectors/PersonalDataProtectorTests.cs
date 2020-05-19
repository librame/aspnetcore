using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Identity.Tests
{
    public class PersonalDataProtectorTests
    {
        [Fact]
        public void AllTest()
        {
            var data = nameof(PersonalDataProtectorTests);

            var dataProtector = TestServiceProvider.Current.GetRequiredService<IPersonalDataProtector>();

            var protect = dataProtector.Protect(data);
            Assert.NotEmpty(protect);

            var unprotect = dataProtector.Unprotect(protect);
            Assert.Equal(data, unprotect);
        }

    }
}
