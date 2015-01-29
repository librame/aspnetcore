// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Librame.Context
{
    /// <summary>
    /// 查询拦截器选项。
    /// </summary>
    /// <author>Librame Pang</author>
    public class QueryInterceptorOption : ApplicationOption
    {
        /// <summary>
        /// 构造一个查询拦截器选项实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// interceptorType 为空。
        /// </exception>
        /// <param name="interceptorType">给定的拦截器类型。</param>
        /// <param name="implementationType">给定的实现类型。</param>
        /// <param name="parameters">给定的拦截器参数数组。</param>
        public QueryInterceptorOption(Type interceptorType, Type implementationType, params QueryInterceptorParameter[] parameters)
            : base(interceptorType, implementationType, parameters)
        {
            IsSuccess = false;
        }

        /// <summary>
        /// 获取拦截是否成功。
        /// </summary>
        public bool IsSuccess { get; internal set; }
    }
}