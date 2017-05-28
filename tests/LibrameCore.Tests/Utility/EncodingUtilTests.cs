using LibrameCore.Utility;
using Xunit;

namespace LibrameCore.Tests.Utility
{
    public class EncodingUtilTests
    {
        private readonly string _encodingName = "utf-8";

        [Fact]
        public void AsEncodingTest()
        {
            var encoding = _encodingName.AsEncoding();

            Assert.NotNull(encoding);
        }

        [Fact]
        public void AsNameTest()
        {
            var name = System.Text.Encoding.UTF8.AsName();

            Assert.Equal(name, _encodingName);
        }

    }
}
