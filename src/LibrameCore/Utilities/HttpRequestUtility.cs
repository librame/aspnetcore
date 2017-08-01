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
using System.Net;
using System.Threading.Tasks;

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
        /// 表现为根 URI 格式字符串。
        /// </summary>
        /// <example>http://localhost:3030</example>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回字符串。</returns>
        public static string AsRootUriString(this HttpRequest request)
        {
            return (request.Scheme + "://" + request.Host.ToString());
        }


        /// <summary>
        /// 转换为 URI 格式字符串。
        /// </summary>
        /// <example>http://localhost:3030/Path+QueryString</example>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回字符串。</returns>
        public static string AsUriString(this HttpRequest request)
        {
            var rootUrl = request.AsRootUriString();

            return (rootUrl + request.Path.ToString() + request.QueryString.ToString());
        }


        /// <summary>
        /// 是否为本地虚拟路径。
        /// </summary>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <param name="url">给定的 URL 字符串。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsLocalUrl(this HttpRequest request, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            if (url.StartsWith("~/"))
                return true;

            if (url.StartsWith("//") || url.StartsWith("/\\"))
                return false;

            // at this point is the url starts with "/" it is local
            if (url.StartsWith("/"))
                return true;

            // at this point, check for an fully qualified url
            try
            {
                var uri = new Uri(url);
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
