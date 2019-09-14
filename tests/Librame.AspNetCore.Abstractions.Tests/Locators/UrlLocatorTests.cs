using Microsoft.AspNetCore.Http;
using Xunit;

namespace Librame.AspNetCore.Tests
{
    public class UrlLocatorTests
    {
        [Fact]
        public void AllTest()
        {
            var uriString = "https://developer.microsoft.com/en-us/fabric#/get-started";

            var locator = (UrlLocatorCore)uriString;
            Assert.Equal("https", locator.Scheme);
            Assert.Equal("developer.microsoft.com", locator.HostString.ToString());
            Assert.Equal("/en-us/fabric", locator.PathString.ToString());
            Assert.False(locator.QueryString.HasValue);
            Assert.Equal("#/get-started", locator.Anchor);

            Assert.Equal("www.microsoft.com", locator.ChangeHost(new HostString("www.microsoft.com")).HostString.ToString());
            Assert.Equal("/zh-cn/fabric", locator.ChangePath(new PathString("/zh-cn/fabric")).PathString.ToString());

            Assert.NotEqual(locator, locator.NewHost(new HostString("developer.microsoft.com")));
            Assert.NotEqual(locator, locator.NewPath(new PathString("/en-us/fabric")));

            Assert.Equal("?query=testQuery", locator.ChangeQuery(new QueryString("?query=testQuery")).QueryString.ToString());
            Assert.NotEmpty(locator.Queries);
        }
    }
}
