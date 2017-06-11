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

namespace LibrameStandard.Utilities
{
    /// <summary>
    /// <see cref="HttpRequest"/> 实用工具。
    /// </summary>
    public static class HttpRequestUtility
    {

        /// <summary>
        /// 转换为 URI 格式。
        /// </summary>
        /// <param name="request">给定的 HTTP 请求。</param>
        /// <returns>返回字符串。</returns>
        public static string AsUriString(this HttpRequest request)
        {
            return (request.Scheme + "://" + request.Host + request.PathBase + request.Path + request.QueryString);
        }

    }
}
