// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Context;
using System.Collections.Generic;

namespace Librame.Data.Context.Kendo
{
    /// <summary>
    /// Kendo UI 请求参数。
    /// </summary>
    internal class KendoQuery
    {
        /// <summary>
        /// 获取过滤器查询拦截器参数数组。
        /// </summary>
        public static QueryInterceptorParameter[] FilterParameters
        {
            get
            {
                var configurators = new List<QueryInterceptorParameter>();

                // Logic
                configurators.Add(new QueryInterceptorParameter(@"(filter\[)(logic\])"));

                // Filters
                configurators.Add(new QueryInterceptorParameter(@"(filter\[filters\]\[)([0-9]*)(\]\[field\])"));
                configurators.Add(new QueryInterceptorParameter(@"(filter\[filters\]\[)([0-9]*)(\]\[value\])"));
                configurators.Add(new QueryInterceptorParameter(@"(filter\[filters\]\[)([0-9]*)(\]\[operator\])"));
                configurators.Add(new QueryInterceptorParameter(@"(filter\[filters\]\[)([0-9]*)(\]\[ignoreCase\])"));

                return configurators.ToArray();
            }
        }

        /// <summary>
        /// 获取排序器查询拦截器参数数组。
        /// </summary>
        public static QueryInterceptorParameter[] SorterParameters
        {
            get
            {
                var configurators = new List<QueryInterceptorParameter>();

                // Sorts
                configurators.Add(new QueryInterceptorParameter(@"(sort\[)([0-9]*)(\]\[field\])"));
                configurators.Add(new QueryInterceptorParameter(@"(sort\[)([0-9]*)(\]\[dir\])"));

                return configurators.ToArray();
            }
        }

        /// <summary>
        /// 获取分页器查询拦截器参数数组。
        /// </summary>
        public static QueryInterceptorParameter[] PagerParameters
        {
            get
            {
                var configurators = new List<QueryInterceptorParameter>();

                // Sorts
                configurators.Add(new QueryInterceptorParameter("(page)"));
                configurators.Add(new QueryInterceptorParameter("(pageSize)"));
                configurators.Add(new QueryInterceptorParameter("(take)"));
                configurators.Add(new QueryInterceptorParameter("(skip)"));

                return configurators.ToArray();
            }
        }

    }
}