// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Context;

namespace Librame.Data.Context
{
    /// <summary>
    /// 分页器查询抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class PagerQueryBase : QueryBase
    {
        /// <summary>
        /// 构造一个分页器查询抽象基类实例。
        /// </summary>
        /// <exception cref="System.ArgumentNullException">
        /// request 与 option 为空。
        /// </exception>
        /// <param name="request">给定的请求对象。</param>
        /// <param name="option">给定的查询拦截器。</param>
        public PagerQueryBase(RequestBase request, QueryInterceptorOption option)
            : base(request, option)
        {
        }


        #region Query

        /// <summary>
        /// 获取或设置页索引。
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 获取或设置页大小。
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 获取或设置读取条数。
        /// </summary>
        public int Take { get; set; }
        /// <summary>
        /// 获取或设置跳过条数。
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// 填充请求参数。
        /// </summary>
        protected override void Populate()
        {
            Index = GetIndex();

            Size = GetSize();

            Take = GetTake();

            Skip = GetSkip();
        }

        /// <summary>
        /// 获取页索引。
        /// </summary>
        /// <returns>返回页索引。</returns>
        protected abstract int GetIndex();
        /// <summary>
        /// 获取页大小。
        /// </summary>
        /// <returns>返回页大小。</returns>
        protected abstract int GetSize();
        /// <summary>
        /// 获取读取条数。
        /// </summary>
        /// <returns>返回读取条数。</returns>
        protected abstract int GetTake();
        /// <summary>
        /// 获取跳过条数。
        /// </summary>
        /// <returns>返回跳过条数。</returns>
        protected abstract int GetSkip();

        #endregion

    }
}