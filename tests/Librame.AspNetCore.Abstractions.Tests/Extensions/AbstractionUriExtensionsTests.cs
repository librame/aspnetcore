using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using Xunit;

namespace Librame.Extensions
{
    public class AbstractionUriExtensionsTests
    {
        [Fact]
        public void IsAbsoluteVirtualPathTest()
        {
            var path = "/path";
            Assert.True(path.IsAbsoluteVirtualPath());

            path = "~" + path;
            Assert.True(path.IsAbsoluteVirtualPath());

            var url = "http://localhost" + path;
            Assert.False(url.IsAbsoluteVirtualPath());
        }

        [Fact]
        public void IsAuthorityUrlTest()
        {
            var url = "http://localhost/path";
            Assert.True(url.IsAuthorityUrl("localhost"));

            url = "http://localhost:8080/path";
            Assert.False(url.IsAuthorityUrl("localhost"));
        }

        [Fact]
        public void TryGetPathTest()
        {
            var url = "http://localhost/path";
            var path = url.TryGetPath(out Uri uri);
            Assert.NotNull(uri);

            var path1 = "/path".TryGetPath();
            Assert.Equal(path, path1);
        }

    }
}
