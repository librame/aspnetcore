// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.IdentityServer
{
    /// <summary>
    /// 绝对 URL 工厂接口。
    /// </summary>
    public interface IAbsoluteUrlFactory
    {
        /// <summary>
        /// 获取绝对 URL。
        /// </summary>
        /// <param name="path">给定的路径。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        string GetAbsoluteUrl(string path);

        /// <summary>
        /// 获取绝对 URL。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <param name="path">给定的路径。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        string GetAbsoluteUrl(HttpContext context, string path);
    }
}
