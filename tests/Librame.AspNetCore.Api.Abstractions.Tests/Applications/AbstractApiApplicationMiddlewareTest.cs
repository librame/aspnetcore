using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Xunit;

namespace Librame.AspNetCore.Api.Tests
{
    using Applications;

    public class AbstractApiApplicationMiddlewareTest
    {
        class TestApiApplicationMiddleware : AbstractApiApplicationMiddleware
        {
            public TestApiApplicationMiddleware(RequestDelegate next)
                : base(next)
            {
            }


            protected override Task InvokeCore(HttpContext context)
            {
                return Task.CompletedTask;
            }
        }


        [Fact]
        public void AllTest()
        {
            var middleware = new TestApiApplicationMiddleware(context => Task.CompletedTask);
            Assert.Equal("/api", middleware.RestrictRequestPath);
            Assert.NotEmpty(middleware.RestrictRequestMethods);
        }

    }
}
