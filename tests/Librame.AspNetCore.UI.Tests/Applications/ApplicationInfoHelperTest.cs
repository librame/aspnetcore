using Xunit;

namespace Librame.AspNetCore.UI.Tests
{
    public class ApplicationInfoHelperTest
    {
        [Fact]
        public void AllTest()
        {
            Assert.NotEmpty(ApplicationInfoUtility.Uis);
            Assert.NotEmpty(ApplicationInfoUtility.Themepacks);
        }
    }
}
