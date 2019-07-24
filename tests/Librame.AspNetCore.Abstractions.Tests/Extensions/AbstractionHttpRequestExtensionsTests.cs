using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
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
