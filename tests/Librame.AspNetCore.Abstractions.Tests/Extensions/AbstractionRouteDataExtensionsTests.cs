using Librame.Extensions;
using Microsoft.AspNetCore.Routing;
using Xunit;

namespace Librame.AspNetCore.Core.Tests.Extensions
{
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

            var data = new RouteData(values);
            var descriptor = data.AsRouteDescriptor();
            Assert.Equal("Default", descriptor.Area);
            Assert.Equal("Test", descriptor.Controller);
            Assert.Equal("Index", descriptor.Action);
            Assert.Null(descriptor.Id);
        }

    }
}
