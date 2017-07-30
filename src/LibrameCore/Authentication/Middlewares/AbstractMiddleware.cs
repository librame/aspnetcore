#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace LibrameCore.Authentication.Middlewares
{
    /// <summary>
    /// 抽象中间件。
    /// </summary>
    public abstract class AbstractMiddleware
    {
        /// <summary>
        /// 构造一个抽象中间件实例。
        /// </summary>
        /// <param name="next">给定的下一步请求委托。</param>
        /// <param name="options">给定的认证选项。</param>
        public AbstractMiddleware(RequestDelegate next, IOptions<AuthenticationOptions> options)
        {
            Next = next.NotNull(nameof(next));
            Options = options.NotNull(nameof(options)).Value;
        }


        /// <summary>
        /// 下一步请求委托。
        /// </summary>
        protected RequestDelegate Next { get; private set; }

        /// <summary>
        /// 认证选项。
        /// </summary>
        protected AuthenticationOptions Options { get; private set; }

    }
}
