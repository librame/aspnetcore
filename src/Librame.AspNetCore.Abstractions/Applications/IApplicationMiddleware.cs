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
using System.Threading.Tasks;

namespace Librame.AspNetCore.Applications
{
    /// <summary>
    /// 应用中间件接口。
    /// </summary>
    public interface IApplicationMiddleware
    {
        /// <summary>
        /// 限定的请求路径。
        /// </summary>
        PathString RestrictRequestPath { get; }

        /// <summary>
        /// 限定的请求方法。
        /// </summary>
        string RestrictRequestMethod { get; }


        /// <summary>
        /// 调用中间件。
        /// </summary>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        /// <returns>返回一个异步操作。</returns>
        Task Invoke(HttpContext context);
    }
}
