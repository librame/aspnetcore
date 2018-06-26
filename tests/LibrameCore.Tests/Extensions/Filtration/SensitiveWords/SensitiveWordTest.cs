using LibrameCore.Extensions.Filtration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LibrameCore.Tests.Extensions.Filtration.SensitiveWords
{
    public class SensitiveWordTest
    {
        [Fact]
        public void UseSensitiveWordTest()
        {
            var services = new ServiceCollection();
            
            services.AddLibrameCore();

            var serviceProvider = services.BuildServiceProvider();

            var filter = serviceProvider.GetRequiredService<ISensitiveWordFiltration>();
            Assert.NotNull(filter);

            var content = "大刀";
            var result = filter.Filting(content);
            Assert.True(result.Exists);
            Assert.NotEmpty(result.Content);
        }

    }
}