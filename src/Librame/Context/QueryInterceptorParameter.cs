// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Librame.Context
{
    /// <summary>
    /// 查询拦截器参数。
    /// </summary>
    /// <author>Librame Pang</author>
    public class QueryInterceptorParameter
    {
        /// <summary>
        /// 构造一个查询拦截器参数实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// pattern 为空。
        /// </exception>
        /// <exception cref="ArgumentException">
        /// 出现正则表达式分析错误。
        /// </exception>
        /// <param name="pattern">给定要匹配的正则表达式模式。</param>
        public QueryInterceptorParameter(string pattern)
            : this(new Regex(pattern))
        {
        }
        /// <summary>
        /// 构造一个查询拦截器参数实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// rule 为空。
        /// </exception>
        /// <param name="rule">给定要匹配的正则表达式规则。</param>
        public QueryInterceptorParameter(Regex rule)
        {
            if (ReferenceEquals(rule, null))
            {
                throw new ArgumentNullException("rule");
            }
            
            Rule = rule;
            Queries = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// 获取要匹配的正则表达式规则。
        /// </summary>
        public Regex Rule { get; private set; }
        /// <summary>
        /// 获取与当前规则匹配的查询参数键值对集合。
        /// </summary>
        public ConcurrentDictionary<string, string> Queries { get; private set; }
    }
}