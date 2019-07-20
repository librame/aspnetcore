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
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Librame.Extensions
{
    /// <summary>
    /// 抽象 HTTP 请求静态扩展。
    /// </summary>
    public static class AbstractionHttpRequestExtensions
    {

        #region Validation

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
        /// 是否为绝对虚拟路径。
        /// </summary>
        /// <example>
        /// ~/VirtualPath or /VirtualPath.
        /// </example>
        /// <param name="path">给定的路径。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAbsoluteVirtualPath(this string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            return path.StartsWith("~/") || path.StartsWith("/");
        }


        /// <summary>
        /// 是否为本地虚拟路径。
        /// </summary>
        /// <param name="pathOrUrl">给定的路径或 URL 字符串。</param>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsLocalUrl(this string pathOrUrl, HttpRequest request)
        {
            return IsLocalUrl(pathOrUrl, request.Host.ToString());
        }
        /// <summary>
        /// 是否为本地虚拟路径。
        /// </summary>
        /// <param name="pathOrUrl">给定的路径或 URL 字符串。</param>
        /// <param name="authority">给定的 DNS 主机名或 IP 地址及端口号（如：localhost:80）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsLocalUrl(this string pathOrUrl, string authority)
        {
            if (pathOrUrl.IsAbsoluteVirtualPath())
                return true; // Local

            if (pathOrUrl.StartsWith("//") || pathOrUrl.StartsWith("/\\"))
                return false;

            try
            {
                var uri = new Uri(pathOrUrl);
                return uri.Authority.Equals(authority, StringComparison.OrdinalIgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion


        /// <summary>
        /// 异步获取真实 IP 地址。
        /// </summary>
        /// <param name="request">给定的 <see cref="HttpRequest"/>。</param>
        /// <returns>返回字符串。</returns>
        public static async Task<string> GetIpAddressAsync(this HttpRequest request)
        {
            string ipAddress = request.Headers["X-Original-For"];

            if (ipAddress.IsNullOrEmpty())
                return ipAddress;

            if (ipAddress.Contains(":"))
                ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(":"));

            if (ipAddress.Equals("127.0.0.1") || ipAddress.Equals("::1"))
            {
                // 根据网卡取本机配置的 IP
                var addresses = await Dns.GetHostAddressesAsync(Dns.GetHostName());

                foreach (var address in addresses)
                {
                    if (address.AddressFamily.ToString() == "InterNetwork")
                    {
                        ipAddress = address.ToString();
                        break;
                    }
                }
            }

            // 对于通过多个代理的情况，第一个IP为客户端真实IP，多个IP按照英文逗号分割
            if (ipAddress.IsNotNull() && ipAddress.Length > 15)
            {
                if (ipAddress.IndexOf(",") > 0)
                    ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(","));
            }

            return ipAddress;
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

    }
}
