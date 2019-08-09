using System;
using Xunit;

namespace Librame.Extensions
{
    public class AbstractionUriExtensionsTests
    {
        [Fact]
        public void SameHostTest()
        {
            var url = "http://localhost/path";
            var host = "http://localhost:8080/path".GetHostString();
            Assert.False(url.SameHost(host));
        }

        [Fact]
        public void GetHostTest()
        {
            var url = "http://localhost/path";
            var host = url.GetHostString(out Uri uri);
            Assert.Equal("localhost", host.ToString());
            Assert.NotNull(uri);
        }

        [Fact]
        public void GetPathTest()
        {
            var url = "http://localhost/path";
            var path = url.GetPathString(out Uri uri);
            Assert.NotNull(uri);

            var path1 = "/path".GetPathString();
            Assert.Equal(path, path1);
        }

        [Fact]
        public void GetQueryTest()
        {
            var url = "http://localhost/path?q=123#456";
            var query = url.GetQueryString(out Uri uri);
            Assert.Equal("?q=123", query.ToString());
            Assert.NotNull(uri);
        }

    }
}
