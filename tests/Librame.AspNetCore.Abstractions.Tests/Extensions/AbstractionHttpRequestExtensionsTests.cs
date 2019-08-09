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
        public async void GetLocalIPAddressTupleAsyncTest()
        {
            var dict = new Dictionary<string, StringValues>();
            dict.Add("X-Original-For", IPAddress.Loopback.ToString() + "," + IPAddress.IPv6Loopback.ToString());

            var headers = new HeaderDictionary(dict);
            var tuple = await headers.GetIPAddressTupleAsync();
            Assert.NotNull(tuple.IPv4);
            Assert.NotNull(tuple.IPv6);
            Assert.False(tuple.IPv4.IsNullOrNone());
            Assert.False(tuple.IPv6.IsNullOrNone());
        }

    }
}
