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
using System.Collections.Generic;

namespace Librame.AspNetCore.Api.Applications
{
    using AspNetCore.Applications;

    /// <summary>
    /// 抽象 API 应用中间件。
    /// </summary>
    public abstract class AbstractApiApplicationMiddleware : AbstractApplicationMiddleware
    {
        /// <summary>
        /// 构造一个 <see cref="AbstractApiApplicationMiddleware"/> 中间件。
        /// </summary>
        /// <param name="next">给定的 <see cref="RequestDelegate"/>。</param>
        public AbstractApiApplicationMiddleware(RequestDelegate next)
            : base(next)
        {
        }


        /// <summary>
        /// 限定的请求路径。
        /// </summary>
        public override PathString RestrictRequestPath
            => "/api/graphql";

        /// <summary>
        /// 限定的请求方法列表（默认仅支持 POST）。
        /// </summary>
        public override IReadOnlyList<string> RestrictRequestMethods
            => new List<string> { "POST" };
    }
}
