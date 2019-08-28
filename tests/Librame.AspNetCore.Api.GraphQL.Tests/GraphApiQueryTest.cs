using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Api.Tests
{
    public class GraphApiQueryTest
    {
        [Fact]
        public void AllTest()
        {
            var query = TestServiceProvider.Current.GetRequiredService<IGraphApiQuery>();
            Assert.NotEmpty(query.Name);
            Assert.NotEmpty(query.Fields);
        }

    }
}
