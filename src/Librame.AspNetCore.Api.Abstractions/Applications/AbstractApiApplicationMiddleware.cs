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
        /// 限定的请求方法。
        /// </summary>
        public override string RestrictRequestMethod
            => "POST";
    }
}
