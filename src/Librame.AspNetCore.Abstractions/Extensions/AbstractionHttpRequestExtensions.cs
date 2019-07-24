#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Librame.Extensions
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
        {
            return request.Headers.IsAjaxRequest();
        }
        /// <summary>
        /// 是否为 AJAX 请求。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAjaxRequest(this IHeaderDictionary headers)
        {
            if ((bool)headers?.TryGetValue("X-Requested-With", out StringValues value))
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
        public static string AsAbsoluteUrl(this HttpRequest request)
        {
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
        public static string AsRootUrl(this HttpRequest request)
        {
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
        {
            return new Uri(request.AsAbsoluteUrl());
        }

        /// <summary>
        /// 新建 URI。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="pathString">给定的 <see cref="PathString"/>。</param>
        /// <returns>返回 <see cref="Uri"/>。</returns>
        public static Uri NewUri(this HttpRequest request, PathString pathString)
        {
            return new Uri(request.AsUri(), pathString.ToString());
        }


        #region GetIpAddress

        private static string _localIpAddressV4
            = "127.0.0.1";

        private static string _localIpAddressV6
            = "::1";


        /// <summary>
        /// 是否为 NULL、IPv4.None 或 IPv6.None。
        /// </summary>
        /// <param name="ipAddress">给定的 <see cref="IPAddress"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsNullOrNone(this IPAddress ipAddress)
        {
            return ipAddress.IsNull()
                || IPAddress.None.Equals(ipAddress)
                || IPAddress.IPv6None.Equals(ipAddress);
        }


        /// <summary>
        /// 异步获取本机 IP 地址的 V4 版本。
        /// </summary>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetLocalIpAddressV4Async()
        {
            var addresses = await GetLocalIpAddressesAsync();

            return addresses.V6;
        }

        /// <summary>
        /// 异步获取本机 IP 地址的 V6 版本。
        /// </summary>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetLocalIpAddressV6Async()
        {
            var addresses = await GetLocalIpAddressesAsync();

            return addresses.V4;
        }

        /// <summary>
        /// 异步获取本机 IP 地址的 V4 及 V6 版本集合。
        /// </summary>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的元组。</returns>
        public static async Task<(IPAddress V4, IPAddress V6)> GetLocalIpAddressesAsync()
        {
            var v4 = IPAddress.None;
            var v6 = IPAddress.None;

            var addresses = await Dns.GetHostAddressesAsync(Dns.GetHostName());
            foreach (var address in addresses)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    v4 = address;
                    continue;
                }

                if (address.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    v6 = address;
                    continue;
                }

                if (!IPAddress.None.Equals(v4) && !IPAddress.None.Equals(v6))
                    return (v4, v6);
            }

            return (v4, v6);
        }


        /// <summary>
        /// 异步获取真实 IP 地址的 V4 版本。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="headerIpAddressKey">给定的头部 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetRealIpAddressV4Async(this HttpRequest request,
            string headerIpAddressKey = null)
        {
            var ipAddresses = await request.GetIpAddressesAsync(headerIpAddressKey);
            return ipAddresses.V4;
        }

        /// <summary>
        /// 异步获取真实 IP 地址的 V6 版本。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="headerIpAddressKey">给定的头部 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetRealIpAddressV6Async(this HttpRequest request,
            string headerIpAddressKey = null)
        {
            var ipAddresses = await request.GetIpAddressesAsync(headerIpAddressKey);
            return ipAddresses.V6;
        }

        /// <summary>
        /// 异步获取真实 IP 地址的 V4 及 V6 版本集合。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <param name="headerIpAddressKey">给定的头部 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 元组的异步操作。</returns>
        public static Task<(IPAddress V4, IPAddress V6)> GetIpAddressesAsync(this HttpRequest request,
            string headerIpAddressKey = null)
        {
            return request.Headers.GetIpAddressesAsync(headerIpAddressKey);
        }


        /// <summary>
        /// 异步获取 IP 地址的 V4 版本。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <param name="ipAddressKey">给定的 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetIpAddressV4Async(this IHeaderDictionary headers,
            string ipAddressKey = null)
        {
            var ipAddresses = await headers.GetIpAddressesAsync(ipAddressKey);
            return ipAddresses.V4;
        }

        /// <summary>
        /// 异步获取 IP 地址的 V6 版本。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <param name="ipAddressKey">给定的 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 的异步操作。</returns>
        public static async Task<IPAddress> GetIpAddressV6Async(this IHeaderDictionary headers,
            string ipAddressKey = null)
        {
            var ipAddresses = await headers.GetIpAddressesAsync(ipAddressKey);
            return ipAddresses.V6;
        }

        /// <summary>
        /// 异步获取 IP 地址的 V4 及 V6 版本集合。
        /// </summary>
        /// <param name="headers">给定的 <see cref="IHeaderDictionary"/>。</param>
        /// <param name="ipAddressKey">给定的 IP 地址键（可选）。</param>
        /// <returns>返回一个包含 <see cref="IPAddress"/> 元组的异步操作。</returns>
        public static async Task<(IPAddress V4, IPAddress V6)> GetIpAddressesAsync(this IHeaderDictionary headers,
            string ipAddressKey = null)
        {
            if (ipAddressKey.IsNullOrEmpty())
                ipAddressKey = "X-Original-For";

            if ((bool)headers?.TryGetValue(ipAddressKey, out StringValues value))
            {
                var ipAddress = (string)value;

                // [::1]:8080
                // 127.0.0.1:8080
                if (ipAddress.Contains(_localIpAddressV4) || ipAddress.Contains(_localIpAddressV6))
                {
                    // 根据网卡取本机配置的 IP
                    var addresses = await GetLocalIpAddressesAsync();
                    return (addresses.V4, addresses.V6);
                }

                if (ipAddress.IndexOf(",") > 0)
                {
                    var dictionary = new Dictionary<string, string>();

                    // 对于通过多个代理的情况，默认 IP 按照英文逗号分割
                    var ipAddresses = ipAddress.Split(',').Select(s => IPAddress.Parse(s));
                    var v4s = ipAddresses.Where(p => p.AddressFamily == AddressFamily.InterNetwork);
                    var v6s = ipAddresses.Where(p => p.AddressFamily == AddressFamily.InterNetworkV6);

                    // 通常第一个IP为客户端真实 IP
                    return (v4s.FirstOrDefault(), v6s.FirstOrDefault());
                }

                var current = IPAddress.Parse(ipAddress);
                if (current.AddressFamily == AddressFamily.InterNetwork)
                {
                    return (current, IPAddress.None);
                }
                else if (current.AddressFamily == AddressFamily.InterNetworkV6)
                {
                    return (IPAddress.None, current);
                }
                else
                {
                    return (IPAddress.None, IPAddress.None);
                }
            }

            return (IPAddress.None, IPAddress.None);
        }

        #endregion

    }
}
