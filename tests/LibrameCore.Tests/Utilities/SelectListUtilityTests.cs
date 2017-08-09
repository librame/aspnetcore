using LibrameStandard.Entity;
using LibrameStandard.Utilities;
using Xunit;

namespace LibrameStandard.Tests.Utilities
{
    public class SelectListUtilityTests
    {
        [Fact]
        public void AsDataStatusItemsTest()
        {
            var items = SelectListUtility.AsDataStatusItems(DataStatus.Public);

            Assert.NotNull(items);
        }

    }
}
