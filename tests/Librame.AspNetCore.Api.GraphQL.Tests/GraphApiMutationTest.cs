using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace Librame.AspNetCore.Api.Tests
{
    public class GraphApiMutationTest
    {
        [Fact]
        public void AllTest()
        {
            var mutation = TestServiceProvider.Current.GetRequiredService<IGraphApiMutation>();
            Assert.NotEmpty(mutation.Name);
            Assert.NotEmpty(mutation.Fields);
        }

    }
}
