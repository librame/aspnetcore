#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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
            => ApiSettings.Preference.RestrictRequestPath;

        /// <summary>
        /// 限定的请求方法列表。
        /// </summary>
        public override IReadOnlyList<string> RestrictRequestMethods
            => ApiSettings.Preference.RestrictRequestMethods;

        /// <summary>
        /// 响应内容类型。
        /// </summary>
        public override string ResponseContentType
            => ApiSettings.Preference.ResponseContentType;
    }
}
