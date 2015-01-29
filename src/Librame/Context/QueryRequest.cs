// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNet.Http;

namespace Librame.Context
{
    /// <summary>
    /// 查询请求。
    /// </summary>
    /// <author>Librame Pang</author>
    public class QueryRequest : QueryRequestBase
    {
        /// <summary>
        /// 构造一个查询请求实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// request 为空。
        /// </exception>
        /// <param name="request">给定的 HTTP 请求对象。</param>
        public QueryRequest(HttpRequest request)
            : base(request)
        {
        }

    }
}