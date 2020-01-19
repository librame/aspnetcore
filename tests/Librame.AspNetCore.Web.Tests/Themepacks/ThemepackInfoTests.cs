using Xunit;

namespace Librame.AspNetCore.Web.Tests
{
    public class ThemepackInfoTests
    {
        [Fact]
        public void AllTest()
        {
            var info = new Themepacks.Simple.ThemepackInfo();
            Assert.NotEmpty(info.Name);
            Assert.NotEmpty(info.Authors);
            Assert.NotEmpty(info.Contact);
            Assert.NotEmpty(info.Copyright);
            Assert.NotNull(info.GetStaticFileProvider());
        }

    }
}
