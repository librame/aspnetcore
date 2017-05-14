using Librame.Algorithm.Hashes;
using Librame.Algorithm.Symmetries;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.Tests.Algorightm
{
    public class AlgorightmTests
    {
        [Fact]
        public void UseAlgorightmTest()
        {
            var services = new ServiceCollection();

            // 注册 Librame （默认使用内存配置源）
            var builder = services.AddLibrameByMemory()
                .UseAlgorithm(); // 使用算法模块

            var test = "向前跑，迎着冷眼和嘲笑";

            // ServiceType is IHashAlgorithm
            var ha = builder.ServiceProvider.GetService<IHashAlgorithm>();
            Assert.NotNull(ha);
            Assert.NotEmpty(ha.ToMd5(test));
            Assert.NotEmpty(ha.ToSha1(test));
            Assert.NotEmpty(ha.ToSha256(test));
            Assert.NotEmpty(ha.ToSha384(test));
            Assert.NotEmpty(ha.ToSha512(test));

            // ServiceType is ISymmetryAlgorithm
            var sa = builder.ServiceProvider.GetService<ISymmetryAlgorithm>();
            Assert.NotNull(sa);

            var encrypt = sa.ToAes(test);
            Assert.Equal(test, sa.FromAes(encrypt));
        }

    }

}
