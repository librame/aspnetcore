using Librame.Utility;
using System;
using System.Text;
using Xunit;

namespace Librame.Tests.Utility
{
    public class ByteUtilTests
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly string _text = "别人笑我太疯癫，我笑他人看不穿；不见五陵豪杰墓，无花无酒锄作田。";

        [Fact]
        public void Base64Test()
        {
            var buffer = _encoding.GetBytes(_text);
            var base64 = buffer.ToBase64();

            var text = _encoding.GetString(base64.FromBase64());
            Assert.Equal(text, _text);
        }

        [Fact]
        public void HexTest()
        {
            var buffer = _encoding.GetBytes(_text);
            var hex = buffer.ToHex();

            var text = _encoding.GetString(hex.FromHex());
            Assert.Equal(text, _text);

            var bit = BitConverter.ToString(buffer).Replace("-", string.Empty);
            Assert.Equal(hex, bit);

            var raw = _encoding.GetString(bit.FromHex());
            Assert.Equal(text, raw);
        }

    }
}
