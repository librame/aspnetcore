using LibrameStandard.Authentication.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameStandard.Mvc.Tests.Authentication
{
    public class AuthenticationTests
    {
        [Fact]
        public void UseAuthenticationTest()
        {
            var services = new ServiceCollection();

            // 注册 Librame MVC （默认使用内存配置源）
            var builder = services.AddLibrameCoreByMemory();
            
            // 获取认证适配器
            var adapter = builder.GetAuthenticationAdapter(new TokenHandlerSettings());
            Assert.NotNull(adapter);
            Assert.NotNull(adapter.UserManager);

            var settings = adapter.TokenManager.HandlerSettings;
            Assert.NotNull(settings);
            Assert.NotNull(settings.SigningCredentials);
        }

    }
}
