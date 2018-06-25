using LibrameCore.Authentication;
using LibrameCore.Authentication.Managers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Tests.Authentication.Managers
{
    using Repositories;

    public class TokenManagerTests
    {
        [Fact]
        public void UseTokenManagerTest()
        {
            var services = new ServiceCollection();
            
            services.AddLibrameCore();

            var serviceProvider = services.BuildServiceProvider();

            // Test
            var tokenManager = serviceProvider.GetRequiredService<ITokenManager>();
            Assert.NotNull(tokenManager);
            
            var user = new User
            {
                Name = "Librame",
                Passwd = "123456"
            };
            var roles = new string[] { "Administrator" };
            var identity = new LibrameIdentity(user.Name, roles, tokenManager.Options);
            
            var token = tokenManager.Encode(identity);
            Assert.NotEmpty(token);

            var decode = tokenManager.Decode(token);
            Assert.Equal(decode.Name, identity.Name);
        }

    }
}
