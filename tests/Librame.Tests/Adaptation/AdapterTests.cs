using Librame.Adaptation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Librame.Tests.Adaptation
{
    public class AdapterTests
    {
        [Fact]
        public void UseAdaptationTest()
        {
            var services = new ServiceCollection();

            // 注册 Librame （默认使用内存配置源）
            var builder = services.AddLibrameByMemory()
                .UseAdaptation(typeof(TestAdapter)); // 使用适配模块

            // ServiceType is IAdapter
            var adapter = builder.ServiceProvider.GetService<IAdapter>();
            Assert.NotNull(adapter);

            var name = "Librame";
            var message = (adapter as TestAdapter).GetMessage(name);

            Assert.Equal(name, message);
        }

    }


    public class TestAdapter : AbstractAdapter, IAdapter
    {
        public TestAdapter(IOptions<LibrameOptions> options)
            : base("Test", options)
        {
        }

        public string GetMessage(string name)
        {
            return name;
        }
        
        public override void Dispose()
        {
            //
        }

    }

}
