using LibrameCore.Server;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Tests.Server.StaticPages
{
    public class StaticPageTest
    {
        [Fact]
        public void UseStaticPageTest()
        {
            var services = new ServiceCollection();
            
            services.AddLibrameCore();

            var serviceProvider = services.BuildServiceProvider();

            var staticPage = serviceProvider.GetRequiredService<IStaticPageServer>();
            Assert.NotNull(staticPage);
        }

    }
}
