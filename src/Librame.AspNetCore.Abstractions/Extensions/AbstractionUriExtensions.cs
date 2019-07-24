#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Http;
using System;

namespace Librame.Extensions
{
    /// <summary>
    /// 抽象 URI 静态扩展。
    /// </summary>
    public static class AbstractionUriExtensions
    {
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
            path.NotNullOrEmpty(nameof(path));

            return path.StartsWith("~/") || path.StartsWith("/");
        }


        /// <summary>
        /// 是否为指定 DNS 主机名或 IP 地址及端口号的 URL。
        /// </summary>
        /// <param name="uriString">给定的路径或 URL 字符串。</param>
        /// <param name="host">给定的 <see cref="HostString"/>。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAuthorityUrl(this string uriString, HostString host)
        {
            return uriString.IsAuthorityUrl(host.ToString());
        }
        /// <summary>
        /// 是否为指定 DNS 主机名或 IP 地址及端口号的 URL。
        /// </summary>
        /// <param name="uriString">给定的路径或 URL 字符串。</param>
        /// <param name="authority">给定的 DNS 主机名或 IP 地址及端口号（如：localhost:80）。</param>
        /// <returns>返回布尔值。</returns>
        public static bool IsAuthorityUrl(this string uriString, string authority)
        {
            if (Uri.TryCreate(uriString, UriKind.Absolute, out Uri uri))
                return uri.Authority.Equals(authority, StringComparison.OrdinalIgnoreCase);

            return false;
        }


        /// <summary>
        /// 尝试从指定路径或 URI 中得到路径字符串。
        /// </summary>
        /// <param name="pathOrUri">给定的路径或 URI。</param>
        /// <returns>返回 <see cref="PathString"/>。</returns>
        public static PathString TryGetPath(this string pathOrUri)
        {
            return pathOrUri.TryGetPath(out _);
        }
        /// <summary>
        /// 尝试从指定路径或 URI 中得到路径字符串。
        /// </summary>
        /// <param name="pathOrUri">给定的路径或 URI。</param>
        /// <param name="uri">输出可能存在的 <see cref="Uri"/>。</param>
        /// <returns>返回 <see cref="PathString"/>。</returns>
        public static PathString TryGetPath(this string pathOrUri, out Uri uri)
        {
            PathString path;

            if (Uri.TryCreate(pathOrUri, UriKind.Absolute, out uri))
            {
                path = PathString.FromUriComponent(uri);
            }
            else
            {
                if (pathOrUri.StartsWith("~/"))
                    pathOrUri = pathOrUri.TrimStart('~'); // 不支持 ~/ 路径模式

                path = new PathString(pathOrUri);
            }

            return path;
        }

    }
}
