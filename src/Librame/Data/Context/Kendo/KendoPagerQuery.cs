// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Context;
using System;
using System.Linq;

namespace Librame.Data.Context.Kendo
{
    /// <summary>
    /// Kendo UI 分页器查询。
    /// </summary>
    /// <author>Librame Pang</author>
    public class KendoPagerQuery : PagerQueryBase
    {
        /// <summary>
        /// 构造一个 Kendo UI 分页器查询实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// request 与 option 为空。
        /// </exception>
        /// <param name="request">给定的请求对象。</param>
        /// <param name="option">给定的查询拦截器选项。</param>
        public KendoPagerQuery(RequestBase request, QueryInterceptorOption option)
            : base(request, option)
        {
        }


        #region Query

        /// <summary>
        /// 获取页索引。
        /// </summary>
        /// <returns>返回页索引。</returns>
        protected override int GetIndex()
        {
            return (Option.Parameters[0] as QueryInterceptorParameter).Queries.FirstOrDefault().Value.Parse<int>();
        }
        /// <summary>
        /// 获取页大小。
        /// </summary>
        /// <returns>返回页大小。</returns>
        protected override int GetSize()
        {
            return (Option.Parameters[1] as QueryInterceptorParameter).Queries.FirstOrDefault().Value.Parse<int>();
        }
        /// <summary>
        /// 获取读取条数。
        /// </summary>
        /// <returns>返回读取条数。</returns>
        protected override int GetTake()
        {
            return (Option.Parameters[2] as QueryInterceptorParameter).Queries.FirstOrDefault().Value.Parse(0);
        }
        /// <summary>
        /// 获取跳过条数。
        /// </summary>
        /// <returns>返回跳过条数。</returns>
        protected override int GetSkip()
        {
            return (Option.Parameters[3] as QueryInterceptorParameter).Queries.FirstOrDefault().Value.Parse(0);
        }

        #endregion

    }
}