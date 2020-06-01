using Xunit;

namespace Librame.AspNetCore.Web.Tests
{
    using Descriptors;

    public class PageRouteDescriptorTests
    {
        [Fact]
        public void AllTest()
        {
            var route = new PageRouteDescriptor("/Index");
            Assert.Equal("/Index", route.Page);

            route.ChangePage("/Change");
            Assert.Equal("/Change", route.Page);

            route.ChangePageName("Test");
            Assert.Equal("/Test", route.Page);

            var pageRoute = route.WithPage("/Test");
            Assert.Equal(pageRoute.Page, route.Page);
            Assert.False(ReferenceEquals(pageRoute, route));

            var pageNameRoute = route.WithPageName("Test");
            Assert.Equal(pageNameRoute.Page, route.Page);
            Assert.False(ReferenceEquals(pageNameRoute, route));

            var viewName = route.GetViewName();
            Assert.Equal("Test", viewName);

            Assert.Equal("/Test", route.ToString());
        }

    }
}
