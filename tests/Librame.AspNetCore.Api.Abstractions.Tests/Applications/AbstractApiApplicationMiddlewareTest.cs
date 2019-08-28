using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace Librame.AspNetCore.Api.Tests
{
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
            Assert.Equal("/api/graphql", middleware.RestrictRequestPath);
            Assert.Equal("POST", middleware.RestrictRequestMethod);
        }

    }
}
