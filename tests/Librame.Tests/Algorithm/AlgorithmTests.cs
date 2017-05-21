using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
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
            var builder = services.AddLibrameByMemory();

            // 获取算法适配器
            var algo = builder.GetAlgorithmAdapter();
            Assert.NotNull(algo);

            var test = "向前跑，迎着冷眼和嘲笑";

            // HashAlgorithm
            Assert.NotNull(algo.Hash);

            Assert.NotEmpty(algo.Hash.ToMd5(test));
            Assert.NotEmpty(algo.Hash.ToSha1(test));
            Assert.NotEmpty(algo.Hash.ToSha256(test));
            Assert.NotEmpty(algo.Hash.ToSha384(test));
            Assert.NotEmpty(algo.Hash.ToSha512(test));

            // SymmetryAlgorithm
            Assert.NotNull(algo.Symmetry);

            var aesString = algo.Symmetry.ToAes(test);
            Assert.NotEqual(test, aesString);
            Assert.Equal(test, algo.Symmetry.FromAes(aesString));

            // RsaAsymmetryAlgorithm
            Assert.NotNull(algo.Asymmetry);

            //// 使用新生成的公私钥参数进行运算
            //var parameters = algo.Asymmetry.KeyGenerator.GenerateParameters();

            //// 方法一
            //var rsaString = algo.Asymmetry.ToRsa(test, parameters);
            //var rawString = algo.Asymmetry.FromRsa(rsaString, parameters);

            //// 方法二
            //var pairString = algo.Asymmetry.KeyGenerator.ToParametersPairString(parameters);
            //var rsaString = algo.Asymmetry.ToRsa(test, publicKeyString: pairString.Key);
            //var rawString = algo.Asymmetry.FromRsa(rsaString, privateKeyString: pairString.Value);

            // 使用系统集成的默认公私钥参数
            var rsaString = algo.Asymmetry.ToRsa(test);
            var rawString = algo.Asymmetry.FromRsa(rsaString);

            Assert.NotEqual(test, rsaString);
            Assert.Equal(test, rawString);
        }

    }

}
