// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Librame.Context
{
    /// <summary>
    /// 查询抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class QueryBase
    {
        /// <summary>
        /// 构造一个查询抽象基类实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// request 与 option 为空。
        /// </exception>
        /// <param name="request">给定的请求对象。</param>
        /// <param name="option">给定的查询拦截器选项。</param>
        public QueryBase(RequestBase request, QueryInterceptorOption option)
        {
            if (ReferenceEquals(request, null))
            {
                throw new ArgumentNullException("request");
            }
            if (ReferenceEquals(option, null))
            {
                throw new ArgumentNullException("option");
            }

            Request = request;
            Option = option;

            Initialize();
        }

        /// <summary>
        /// 获取请求对象。
        /// </summary>
        public RequestBase Request { get; private set; }
        /// <summary>
        /// 获取查询拦截器选项。
        /// </summary>
        public QueryInterceptorOption Option { get; private set; }

        /// <summary>
        /// 初始化查询。
        /// </summary>
        private void Initialize()
        {
            // 如果拦截成功，则填充查询参数
            if (Option.IsSuccess)
            {
                Populate();
            }
        }

        /// <summary>
        /// 填充查询参数。
        /// </summary>
        protected abstract void Populate();
    }
}