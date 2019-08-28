using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Api.Tests
{
    public class GraphApiSchemaTest
    {
        [Fact]
        public void AllTest()
        {
            var schema = TestServiceProvider.Current.GetRequiredService<IGraphApiSchema>();
            Assert.NotNull(schema.Mutation);
            Assert.NotNull(schema.Query);
            Assert.NotNull(schema.Subscription);
        }

    }
}
