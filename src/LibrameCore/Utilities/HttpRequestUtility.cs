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
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LibrameStandard.Utilities
{
    /// <summary>
    /// <see cref="HttpRequest"/> 实用工具。
    /// </summary>
    public static class HttpRequestUtility
    {

        /// <summary>
        /// 异步得到真实 IP 地址。
        /// </summary>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回字符串。</returns>
        public static async Task<string> AsRealIpAddressAsync(this HttpRequest request)
        {
            string ipAddress = request.Headers["X-Original-For"];

            if (string.IsNullOrEmpty(ipAddress))
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
            if (ipAddress != null && ipAddress.Length > 15)
            {
                if (ipAddress.IndexOf(",") > 0)
                    ipAddress = ipAddress.Substring(0, ipAddress.IndexOf(","));
            }

            return ipAddress;
        }


        /// <summary>
        /// 表现为绝对 URL 字符串。
        /// </summary>
        /// <example>http://localhost:80/path?query</example>
        /// <param name="request">给定的 HTTP 请求。</param>
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
        /// 表现为绝对 URL 字符串。
        /// </summary>
        /// <param name="pathOrUrl">给定的路径或 URL 字符串。</param>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <param name="returnUrlParameter">给定的返回 URL 参数。</param>
        /// <param name="queries">给定的查询参数集合。</param>
        /// <returns>返回字符串。</returns>
        public static string AsAbsoluteUrl(this string pathOrUrl, HttpRequest request,
            string returnUrlParameter, params KeyValuePair<string, string>[] queries)
        {
            if (!pathOrUrl.IsLocalUrl(request))
            {
                var uri = new Uri(pathOrUrl);

                // 跨域模式
                if (!uri.Authority.Equals(request.Host.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    var requestUrl = request.AsAbsoluteUrl();

                    return new StringBuilder()
                        .Append(pathOrUrl)
                        .Append("?")
                        .Append(returnUrlParameter)
                        .Append("=")
                        .Append(HttpUtility.UrlEncode(requestUrl))
                        .ToString();
                }

                // 本域 URL 直接返回
                return pathOrUrl;
            }

            var sb = new StringBuilder()
                .Append(request.Scheme)
                .Append(Uri.SchemeDelimiter)
                .Append(request.Host)
                .Append(pathOrUrl); // Path

            if (queries.Length > 0)
            {
                var queryString = queries.JoinString(q => q.Key + "=" + HttpUtility.UrlEncode(q.Value), "&");
                sb.Append("?" + queryString);
            }

            return sb.ToString();
        }


        /// <summary>
        /// 表现为根 URL 格式字符串。
        /// </summary>
        /// <example>http://localhost:80</example>
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
        /// 是否为 AJAX 请求。
        /// </summary>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            return (request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }


        /// <summary>
        /// 是否为相对虚拟路径。
        /// </summary>
        /// <param name="path">给定的路径。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsRelativeVirtualPath(this string path)
        {
            return (path.StartsWith("~/") || path.StartsWith("/"));
        }


        /// <summary>
        /// 是否为本地虚拟路径。
        /// </summary>
        /// <param name="pathOrUrl">给定的路径或 URL 字符串。</param>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsLocalUrl(this string pathOrUrl, HttpRequest request)
        {
            if (string.IsNullOrWhiteSpace(pathOrUrl))
                return false;

            if (pathOrUrl.StartsWith("~/"))
                return true;

            if (pathOrUrl.StartsWith("//") || pathOrUrl.StartsWith("/\\"))
                return false;

            // at this point is the url starts with "/" it is local
            if (pathOrUrl.StartsWith("/"))
                return true;

            // at this point, check for an fully qualified url
            try
            {
                var uri = new Uri(pathOrUrl);

                // same host and port number
                if (uri.Authority.Equals(request.Host.ToString(), StringComparison.OrdinalIgnoreCase))
                    return true;

                //// finally, check the base url from the settings
                //var workContext = request.RequestContext.GetWorkContext();
                //if (workContext != null) {
                //    var baseUrl = workContext.CurrentSite.BaseUrl;
                //    if (!string.IsNullOrWhiteSpace(baseUrl)) {
                //        if (uri.Authority.Equals(new Uri(baseUrl).Authority, StringComparison.OrdinalIgnoreCase)) {
                //            return true;
                //        }
                //    }
                //}

                return false;
            }
            catch
            {
                // mall-formed url e.g, "abcdef"
                return false;
            }
        }

    }
}
