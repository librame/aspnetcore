using Microsoft.AspNetCore.Routing;
using Xunit;

namespace Librame.AspNetCore.Web.Tests
{
    using Descriptors;

    public class AbstractionRouteDataExtensionsTests
    {
        [Fact]
        public void AsRouteDescriptorTest()
        {
            var values = new RouteValueDictionary(new
            {
                area = "Default",
                controller = "Test",
                action = "Index"
            });

            var descriptor = values.AsActionRouteDescriptor();

            Assert.Equal("Default", descriptor.Area);
            Assert.Equal("Test", descriptor.Controller);
            Assert.Equal("Index", descriptor.Action);
            Assert.Null(descriptor.Id);
        }

    }
}
