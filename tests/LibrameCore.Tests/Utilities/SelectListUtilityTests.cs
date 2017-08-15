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
            var items = SelectListUtility.AsDataStatusSelectListItems(DataStatus.Public);

            Assert.NotNull(items);
        }

    }
}
