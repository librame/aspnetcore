using Xunit;

namespace Librame.AspNetCore.Web.Tests
{
    using Descriptors;

    public class ActionRouteDescriptorTests
    {
        [Fact]
        public void AllTest()
        {
            var route = new ActionRouteDescriptor("Index", "Home");
            Assert.Equal("Index", route.Action);
            Assert.Equal("Home", route.Controller);

            route.ChangeAction("Change");
            Assert.Equal("Change", route.Action);

            route.ChangeController("Test");
            Assert.Equal("Test", route.Controller);

            var actionRoute = route.WithAction("Change");
            Assert.Equal(actionRoute.Action, route.Action);
            Assert.False(ReferenceEquals(actionRoute, route));

            var controllerRoute = route.WithController("Test");
            Assert.Equal(controllerRoute.Controller, route.Controller);
            Assert.False(ReferenceEquals(controllerRoute, route));

            var actionControllerRoute = route.WithActionAndController("Change", "Test");
            Assert.Equal(actionControllerRoute.Action, route.Action);
            Assert.Equal(actionControllerRoute.Controller, route.Controller);
            Assert.False(ReferenceEquals(actionControllerRoute, route));

            var viewName = route.GetViewName();
            Assert.Equal("Change", viewName);

            Assert.Equal("/Test/Change", route.ToString());
        }

    }
}
