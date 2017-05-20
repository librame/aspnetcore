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

            // HashAlgorithm
            var ha = builder.GetHashAlgorithm();
            Assert.NotNull(ha);

            Assert.NotEmpty(ha.ToMd5(test));
            Assert.NotEmpty(ha.ToSha1(test));
            Assert.NotEmpty(ha.ToSha256(test));
            Assert.NotEmpty(ha.ToSha384(test));
            Assert.NotEmpty(ha.ToSha512(test));

            // SymmetryAlgorithm
            var sa = builder.GetSymmetryAlgorithm();
            Assert.NotNull(sa);

            var aes = sa.ToAes(test);
            Assert.NotEqual(test, aes);
            Assert.Equal(test, sa.FromAes(aes));

            // RsaAsymmetryAlgorithm
            var aa = builder.GetRsaAsymmetryAlgorithm();
            Assert.NotNull(aa);

            var publicKeyString = aa.KeyGenerator.ToPublicKeyString();
            var privateKeyString = aa.KeyGenerator.ToPrivateKeyString();

            var rsa = aa.ToRsa(test);
            Assert.NotEqual(test, rsa);
            Assert.Equal(test, aa.FromRsa(rsa));
        }

    }

}
