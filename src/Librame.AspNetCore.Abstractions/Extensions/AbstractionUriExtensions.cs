#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Microsoft.AspNetCore.Http
{
    /// <summary>
    /// 抽象 URI 静态扩展。
    /// </summary>
    public static class AbstractionUriExtensions
    {
        /// <summary>
        /// 同一 DNS 主机名或 IP 地址及端口号。
        /// </summary>
        /// <param name="uriString">给定的路径或 URL 字符串。</param>
        /// <param name="host">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "uriString")]
        public static bool SameHost(this string uriString, HostString host)
            => uriString.SameHost(host.ToString());


        /// <summary>
        /// 获取 URI 字符串中的主机。
        /// </summary>
        /// <param name="uriString">给定的 URI 字符串。</param>
        /// <returns>返回 <see cref="HostString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "uriString")]
        public static HostString GetHostString(this string uriString)
            => uriString.GetHostString(out _);

        /// <summary>
        /// 获取 URI 字符串中的主机。
        /// </summary>
        /// <param name="uriString">给定的 URI 字符串。</param>
        /// <param name="result">输出可能存在的 <see cref="Uri"/>。</param>
        /// <returns>返回 <see cref="HostString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "uriString")]
        public static HostString GetHostString(this string uriString, out Uri result)
            => uriString.IsAbsoluteUri(out result) ? new HostString(result.Authority) : default;


        /// <summary>
        /// 获取指定路径或 URI 中的路径。
        /// </summary>
        /// <param name="pathOrUri">给定的路径或 URI。</param>
        /// <returns>返回 <see cref="PathString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "pathOrUri")]
        public static PathString GetPathString(this string pathOrUri)
            => pathOrUri.GetPathString(out _);

        /// <summary>
        /// 获取指定路径或 URI 中的路径。
        /// </summary>
        /// <param name="pathOrUri">给定的路径或 URI。</param>
        /// <param name="result">输出可能存在的 <see cref="Uri"/>。</param>
        /// <returns>返回 <see cref="PathString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "pathOrUri")]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "pathOrUri")]
        public static PathString GetPathString(this string pathOrUri, out Uri result)
        {
            PathString path;

            if (pathOrUri.IsAbsoluteUri(out result))
            {
                path = PathString.FromUriComponent(result);
            }
            else
            {
                if (pathOrUri.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
                    pathOrUri = pathOrUri.TrimStart('~'); // PathString 不支持 ~/ 路径模式

                path = new PathString(pathOrUri);
            }

            return path;
        }


        /// <summary>
        /// 获取 URI 字符串中的查询。
        /// </summary>
        /// <param name="uriString">给定的 URI 字符串。</param>
        /// <returns>返回 <see cref="HostString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "uriString")]
        public static QueryString GetQueryString(this string uriString)
            => uriString.GetQueryString(out _);

        /// <summary>
        /// 获取 URI 字符串中的查询。
        /// </summary>
        /// <param name="uriString">给定的 URI 字符串。</param>
        /// <param name="result">输出可能存在的 <see cref="Uri"/>。</param>
        /// <returns>返回 <see cref="HostString"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "uriString")]
        public static QueryString GetQueryString(this string uriString, out Uri result)
            => uriString.IsAbsoluteUri(out result) ? new QueryString(result.Query) : default;


        #region IPAddress

        /// <summary>
        /// 是本机 IPv6 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsLocalIPv6(this HostString hostString)
            => hostString.Host.IsLocalIPv6();

        /// <summary>
        /// 是本机 IPv4 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsLocalIPv4(this HostString hostString)
            => hostString.Host.IsLocalIPv4();

        /// <summary>
        /// 是本机 IP 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsLocalIPAddress(this HostString hostString)
            => hostString.Host.IsLocalIPAddress();


        /// <summary>
        /// 是 IPv6 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsIPv6(this HostString hostString)
            => hostString.Host.IsIPv6();

        /// <summary>
        /// 是 IPv6 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <param name="address">输出 <see cref="IPAddress"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsIPv6(this HostString hostString, out IPAddress address)
            => hostString.Host.IsIPv6(out address);


        /// <summary>
        /// 是 IPv4 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsIPv4(this HostString hostString)
            => hostString.Host.IsIPv4();

        /// <summary>
        /// 是 IPv4 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <param name="address">输出 <see cref="IPAddress"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsIPv4(this HostString hostString, out IPAddress address)
            => hostString.Host.IsIPv4(out address);


        /// <summary>
        /// 是 IP 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsIPAddress(this HostString hostString)
            => hostString.IsIPAddress(out _);

        /// <summary>
        /// 是 IP 地址。
        /// </summary>
        /// <param name="hostString">给定的 <see cref="HostString"/>。</param>
        /// <param name="address">输出 <see cref="IPAddress"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsIPAddress(this HostString hostString, out IPAddress address)
            => hostString.Host.IsIPAddress(out address);

        #endregion

    }
}
