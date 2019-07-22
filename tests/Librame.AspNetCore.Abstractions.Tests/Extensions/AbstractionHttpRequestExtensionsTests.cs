using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Librame.Extensions
{
    public class AbstractionHttpRequestExtensionsTests
    {
        [Fact]
        public void IsAjaxRequestTest()
        {
            var dict = new Dictionary<string, StringValues>();
            dict.Add("X-Requested-With", "xmlhttprequest");

            var headers = new HeaderDictionary(dict);
            Assert.True(headers.IsAjaxRequest());
        }

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
        public async void GetLocalIpAddressesAsyncTest()
        {
            var ipAddresses = await AbstractionHttpRequestExtensions.GetLocalIpAddressesAsync();
            Assert.NotNull(ipAddresses.V4);
            Assert.NotNull(ipAddresses.V6);

            var dict = new Dictionary<string, StringValues>();
            dict.Add("X-Original-For", "::1");

            var headers = new HeaderDictionary(dict);
            ipAddresses = await headers.GetIpAddressesAsync();
            Assert.NotNull(ipAddresses.V4); //ipAddresses.V4.IsNullOrNone();
            Assert.NotNull(ipAddresses.V6);
        }

    }
}
