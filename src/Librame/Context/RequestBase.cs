// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;
using System;

namespace Librame.Context
{
    /// <summary>
    /// 请求抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class RequestBase
    {
        /// <summary>
        /// 构造一个请求抽象基类实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// request 为空。
        /// </exception>
        /// <param name="request">给定的 HTTP 请求对象。</param>
        public RequestBase(HttpRequest request)
        {
            if (ReferenceEquals(request, null))
            {
                throw new ArgumentNullException("request");
            }

            Request = request;

            Initialize();
        }

        /// <summary>
        /// 获取当前 HTTP 请求对象。
        /// </summary>
        public HttpRequest Request { get; private set; }

        /// <summary>
        /// 初始化请求。
        /// </summary>
        private void Initialize()
        {
            // 如果包含查询请求
            if (!ReferenceEquals(Request.Query, null) && Request.Query.Count > 0)
            {
                Resolve();
            }
        }

        /// <summary>
        /// 解析请求。
        /// </summary>
        protected abstract void Resolve();

    }
}