using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using Xunit;

namespace Librame.AspNetCore.Web.Tests
{
    using Descriptors;

    public class AbstractionRouteDescriptorExtensionsTests
    {
        [Fact]
        public void AsActionRouteDescriptorTest()
        {
            var values = new RouteValueDictionary(new
            {
                area = "Default",
                controller = "Test",
                action = "Index",
                id = "name=test"
            });

            var descriptor = values.AsActionRouteDescriptor();

            Assert.Equal("Default", descriptor.Area);
            Assert.Equal("Test", descriptor.Controller);
            Assert.Equal("Index", descriptor.Action);
            Assert.Equal("name=test", descriptor.Id);

            var dict = new Dictionary<string, string>
            {
                { "area", "Default" },
                { "controller", "Test" },
                { "action", "Index" },
                { "id", "name=test" }
            };

            descriptor = dict.AsActionRouteDescriptor();

            Assert.Equal("Default", descriptor.Area);
            Assert.Equal("Test", descriptor.Controller);
            Assert.Equal("Index", descriptor.Action);
            Assert.Equal("name=test", descriptor.Id);
        }

        [Fact]
        public void AsPageRouteDescriptorTest()
        {
            var values = new RouteValueDictionary(new
            {
                area = "Default",
                page = "/Test",
                id = "name=test"
            });

            var descriptor = values.AsPageRouteDescriptor();

            Assert.Equal("Default", descriptor.Area);
            Assert.Equal("/Test", descriptor.Page);
            Assert.Equal("name=test", descriptor.Id);

            var dict = new Dictionary<string, string>
            {
                { "area", "Default" },
                { "page", "/Test" },
                { "id", "name=test" }
            };

            descriptor = dict.AsPageRouteDescriptor();

            Assert.Equal("Default", descriptor.Area);
            Assert.Equal("/Test", descriptor.Page);
            Assert.Equal("name=test", descriptor.Id);
        }

    }
}
