#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using Librame.Extensions.Core.Utilities;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 抽象 HTTP 请求静态扩展。
    /// </summary>
    public static class AbstractionHttpRequestExtensions
    {
        /// <summary>
        /// 是否为 AJAX 请求。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAjaxRequest(this HttpRequest request)
            => request.NotNull(nameof(request)).Headers.IsAjaxRequest();

        /// <summary>
        /// 是否为 AJAX 请求。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAjaxRequest(this IHeaderDictionary headers)
        {
            if (true == headers?.TryGetValue("X-Requested-With", out StringValues value))
                return "XMLHttpRequest".Equals(value, StringComparison.OrdinalIgnoreCase);

            return false;
        }


        /// <summary>
        /// 表现为绝对 URL 字符串。
        /// </summary>
        /// <example>
        /// http://localhost:80/path?query
        /// </example>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public static string AsAbsoluteUrl(this HttpRequest request)
        {
            request.NotNull(nameof(request));

            return new StringBuilder()
                .Append(request.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(request.Host)
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

        /// <summary>
        /// 表现为根 URL 字符串。
        /// </summary>
        /// <example>
        /// http://localhost:80
        /// </example>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        public static string AsRootUrl(this HttpRequest request)
        {
            request.NotNull(nameof(request));

            return new StringBuilder()
                .Append(request.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(request.Host)
                .ToString();
        }

        /// <summary>
        /// 表现为 URI。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <returns>返回 <see cref="Uri"/>。</returns>
        public static Uri AsUri(this HttpRequest request)
            => new Uri(request.AsAbsoluteUrl());

        /// <summary>
        /// 新建 URI。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="pathString">给定的 <see cref="PathString"/>。</param>
        /// <returns>返回 <see cref="Uri"/>。</returns>
        public static Uri NewUri(this HttpRequest request, PathString pathString)
            => new Uri(request.AsUri(), pathString.ToString());


        #region GetIpAddress

        /// <summary>
        /// 异步获取 IPv4 地址。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="addressKey">给定的头部 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetIPv4Async(this HttpRequest request,
            string addressKey = null)
        {
            (IPAddress v4, _) = await request.GetIPv4AndIPv6AddressAsync(addressKey).ConfigureAwait();
            return v4;
        }

        /// <summary>
        /// 异步获取 IPv6 地址。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="addressKey">给定的头部 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetIPv6Async(this HttpRequest request,
            string addressKey = null)
        {
            (_, IPAddress v6) = await request.GetIPv4AndIPv6AddressAsync(addressKey).ConfigureAwait();
            return v6;
        }

        /// <summary>
        /// 异步获取 IPv4 和 IPv6 地址的元组。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="addressKey">给定的 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="Tuple{IPAddress, IPAddress}"/> 的元组。</returns>
        public static Task<(IPAddress v4, IPAddress v6)> GetIPv4AndIPv6AddressAsync(this HttpRequest request,
            string addressKey = null)
            => request.NotNull(nameof(request)).Headers.GetIPv4AndIPv6AddressAsync(addressKey);


        /// <summary>
        /// 异步获取 IPv4 地址。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <param name="ipAddressKey">给定的 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetIPv4Async(this IHeaderDictionary headers,
            string ipAddressKey = null)
        {
            (IPAddress v4, _) = await headers.GetIPv4AndIPv6AddressAsync(ipAddressKey).ConfigureAwait();
            return v4;
        }

        /// <summary>
        /// 异步获取 IPv6 地址。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <param name="addressKey">给定的 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetIPv6Async(this IHeaderDictionary headers,
            string addressKey = null)
        {
            (_, IPAddress v6) = await headers.GetIPv4AndIPv6AddressAsync(addressKey).ConfigureAwait();
            return v6;
        }

        /// <summary>
        /// 异步获取 IPv4 和 IPv6 地址的元组。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <param name="addressKey">给定的 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 IPv4 和 IPv6 的元组。</returns>
        public static async Task<(IPAddress v4, IPAddress v6)> GetIPv4AndIPv6AddressAsync(this IHeaderDictionary headers,
            string addressKey = null)
        {
            if (addressKey.IsEmpty())
                addressKey = "X-Original-For";

            if (false == headers?.TryGetValue(addressKey, out StringValues value))
                return (null, null);

            var address = value.ToString();
            if (address.IndexOf(",", StringComparison.OrdinalIgnoreCase) > 0)
            {
                var dictionary = new Dictionary<string, string>();

                // 拆分多个 IP 地址
                var addresses = address.Split(',').Select(str =>
                {
                    return IPAddress.TryParse(str, out IPAddress a) ? a : null;
                })
                .Where(a => a.IsNotNull());

                var ipv4s = addresses.Where(p => p.AddressFamily == AddressFamily.InterNetwork);
                var ipv6s = addresses.Where(p => p.AddressFamily == AddressFamily.InterNetworkV6);

                // 默认返回第一组 IP 地址
                return (ipv4s.FirstOrDefault(), ipv6s.FirstOrDefault());
            }

            var host = new HostString(address);
            if (host.IsLocalIPAddress())
                return await IPAddressUtility.GetLocalIPv4AndIPv6AddressAsync().ConfigureAwait();

            if (IPAddress.TryParse(host.Host, out IPAddress current))
            {
                if (current.AddressFamily == AddressFamily.InterNetwork)
                    return (current, null);

                if (current.AddressFamily == AddressFamily.InterNetworkV6)
                    return (null, current);
            }

            return (null, null);
        }

        #endregion

    }
}
