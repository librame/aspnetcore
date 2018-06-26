using LibrameCore.Extensions.Server;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Tests.Extensions.Server.StaticPages
{
    public class StaticPageTest
    {
        [Fact]
        public void UseStaticPageTest()
        {
            var services = new ServiceCollection();
            
            services.AddLibrameCore();
            services.AddLogging();
            services.AddMvc();

            var serviceProvider = services.BuildServiceProvider();

            var staticPage = serviceProvider.GetRequiredService<IStaticPageServer>();
            Assert.NotNull(staticPage);
        }

    }
}
