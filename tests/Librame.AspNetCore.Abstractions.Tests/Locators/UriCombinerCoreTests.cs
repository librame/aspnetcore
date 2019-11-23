using Microsoft.AspNetCore.Http;
using Xunit;

namespace Librame.AspNetCore.Tests
{
    using Extensions;
    using Extensions.Core;

    public class UriCombinerCoreTests
    {
        [Fact]
        public void AllTest()
        {
            var uriString = "https://developer.microsoft.com/en-us/fabric#/get-started";

            var combiner = new UriCombinerCore(uriString.AsAbsoluteUri());
            Assert.Equal("https", combiner.Scheme);
            Assert.Equal("developer.microsoft.com", combiner.HostString.ToString());
            Assert.Equal("/en-us/fabric", combiner.PathString.ToString());
            Assert.False(combiner.QueryString.HasValue);
            Assert.Equal("#/get-started", combiner.Anchor);

            Assert.Equal("www.microsoft.com", combiner.ChangeHost(new HostString("www.microsoft.com")).HostString.ToString());
            Assert.Equal("/zh-cn/fabric", combiner.ChangePath(new PathString("/zh-cn/fabric")).PathString.ToString());

            Assert.NotEqual(combiner, combiner.NewHost(new HostString("developer.microsoft.com")));
            Assert.NotEqual(combiner, combiner.NewPath(new PathString("/en-us/fabric")));

            Assert.Equal("?query=testQuery", combiner.ChangeQuery(new QueryString("?query=testQuery")).QueryString.ToString());
            Assert.NotEmpty(combiner.Queries);
        }
    }
}
