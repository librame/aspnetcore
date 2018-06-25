using LibrameCore.Authentication;
using LibrameStandard;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Tests.Authentication
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
            var options = serviceProvider.GetOptions<LibrameAuthenticationOptions>();
            Assert.NotNull(options);
        }

    }
}
