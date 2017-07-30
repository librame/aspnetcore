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

            // 注册 Librame MVC （默认使用内存配置源）
            services.AddLibrameCoreByMemory();

            var serviceProvider = services.BuildServiceProvider();
            
            // 获取认证选项
            var options = serviceProvider.GetOptions<AuthenticationOptions>();
            Assert.NotNull(options);
            Assert.NotNull(options.Token);
        }

    }
}
