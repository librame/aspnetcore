// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Context;
using System.Collections.Generic;

namespace Librame.Data.Context
{
    /// <summary>
    /// 过滤器查询抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class FilterQueryBase : QueryBase
    {
        /// <summary>
        /// 构造一个过滤器查询抽象基类实例。
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// request 与 option 为空。
        /// </exception>
        /// <param name="request">给定的请求对象。</param>
        /// <param name="option">给定的查询拦截器选项。</param>
        public FilterQueryBase(RequestBase request, QueryInterceptorOption option)
            : base(request, option)
        {
        }


        #region Query

        /// <summary>
        /// 获取或设置逻辑字符串。
        /// </summary>
        public string Logic { get; set; }
        /// <summary>
        /// 获取或设置信息集合。
        /// </summary>
        public IEnumerable<FiltrationInfo> Infos { get; set; }

        /// <summary>
        /// 填充请求参数。
        /// </summary>
        protected override void Populate()
        {
            Logic = GetLogic();

            Infos = GetInfos();
        }

        /// <summary>
        /// 获取逻辑字符串。
        /// </summary>
        /// <returns>返回逻辑字符串。</returns>
        protected abstract string GetLogic();
        /// <summary>
        /// 获取信息集合。
        /// </summary>
        /// <returns>返回信息集合。</returns>
        protected abstract IEnumerable<FiltrationInfo> GetInfos();

        #endregion

    }
}