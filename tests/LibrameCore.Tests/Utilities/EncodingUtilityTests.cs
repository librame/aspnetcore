using LibrameCore.Utilities;
using Xunit;

namespace LibrameCore.Tests.Utility
{
    public class EncodingUtilityTests
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
