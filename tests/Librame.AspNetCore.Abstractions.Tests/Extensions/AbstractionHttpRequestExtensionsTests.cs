using Librame.Extensions;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Microsoft.AspNetCore.Http
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
            (IPAddress v4, IPAddress v6) = await headers.GetIPv4AndIPv6AddressAsync().ConfigureAwait();
            Assert.NotNull(v4);
            Assert.NotNull(v6);
            Assert.False(v4.IsNullOrNone());
            Assert.False(v6.IsNullOrNone());
        }

    }
}
