using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Librame.AspNetCore.Api.Tests
{
    public class GraphApiSubscriptionTest
    {
        [Fact]
        public void AllTest()
        {
            var subscription = TestServiceProvider.Current.GetRequiredService<IGraphApiSubscription>();
            Assert.NotEmpty(subscription.Name);
        }

    }
}
