using LibrameCore.Adaptation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using Xunit;

namespace LibrameCore.Tests.Adaptation
{
    public class AdaptationTests
    {
        [Fact]
        public void UseAdaptationTest()
        {
            var services = new ServiceCollection();

            // 注册 Librame （默认使用内存配置源）
            var builder = services.AddLibrameByMemory()
                .TryAddAdaptation(typeof(TestAdapter).GetTypeInfo().Assembly);
            
            var adapters = builder.GetAllAdapters();
            Assert.NotNull(adapters);

            var name = "Librame";
            var testAdapter = builder.GetAdapter<ITestAdapter>();
            var message = testAdapter.GetMessage(name);

            Assert.Equal(name, message);
        }

    }


    public interface ITestAdapter : IAdapter
    {
        string GetMessage(string name);
    }

    public class TestAdapter : AbstractAdapter, ITestAdapter
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
