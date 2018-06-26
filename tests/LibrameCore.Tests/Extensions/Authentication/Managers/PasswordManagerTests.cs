using LibrameCore.Extensions.Authentication.Managers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Tests.Extensions.Authentication.Managers
{
    public class PasswordManagerTests
    {
        [Fact]
        public void UsePasswordManagerTest()
        {
            var services = new ServiceCollection();
            
            services.AddLibrameCore();

            var serviceProvider = services.BuildServiceProvider();

            // Test
            var passwordManager = serviceProvider.GetRequiredService<IPasswordManager>();
            Assert.NotNull(passwordManager);

            var password = "123456";
            var encode = passwordManager.Encode(password);
            Assert.NotEmpty(encode);

            var validate = passwordManager.Validate(encode, password);
            Assert.True(validate);
        }

    }
}
