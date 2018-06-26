using LibrameCore.Extensions.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace LibrameCore.Tests.Extensions.Authentication
{
    public class AuthenticationTests
    {
        [Fact]
        public void UseAuthenticationTest()
        {
            var services = new ServiceCollection();
            
            services.AddLibrameCore();

            var serviceProvider = services.BuildServiceProvider();
            
            // 获取认证选项
            var options = serviceProvider.GetRequiredService<IOptionsMonitor<AuthenticationExtensionOptions>>();
            Assert.NotNull(options);
        }

    }
}
