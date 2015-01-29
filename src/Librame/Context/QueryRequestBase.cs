// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data.Context;
using Microsoft.AspNet.Http;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Librame.Context
{
    /// <summary>
    /// 查询请求抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class QueryRequestBase : RequestBase
    {
        /// <summary>
        /// 构造一个查询请求抽象基类实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// request 为空。
        /// </exception>
        /// <param name="request">给定的 HTTP 请求对象。</param>
        public QueryRequestBase(HttpRequest request)
            : base(request)
        {
        }

        /// <summary>
        /// 获取绑定的所有查询拦截器选项。
        /// </summary>
        /// <returns>返回拦截器列表。</returns>
        protected virtual IList<QueryInterceptorOption> GetOptions()
        {
            var options = LibrameHelper.GetOrCreateBinder().GetOptions(ApplicationBinder.QueryInterceptorDomain);

            return options.Select(pair => (QueryInterceptorOption)pair.Value).ToList();
        }

        /// <summary>
        /// 解析查询请求。
        /// </summary>
        protected override void Resolve()
        {
            Options = GetOptions();
            // 创建副本
            var keys = new List<string>(Request.Query.Keys);

            // 遍历拦截器选项集合
            foreach (var opt in Options)
            {
                // 重置可能存在的成功拦截缓存
                opt.IsSuccess = false;

                // 遍历拦截器参数集合
                foreach (QueryInterceptorParameter parameter in opt.Parameters)
                {
                    // 先清除可能存在的键值对缓存
                    parameter.Queries.Clear();

                    // 遍历请求查询参数集合
                    for (int i = 0; i < keys.Count; i++)
                    {
                        string key = keys[i];

                        // 如果匹配成功
                        if (parameter.Rule.IsMatch(key))
                        {
                            var value = Request.Query[key];
                            // 保存到拦截器的参数键值对中
                            parameter.Queries.AddOrUpdate(key, value, (k, v) => value);

                            // 指示拦截成功
                            if (!opt.IsSuccess)
                            {
                                opt.IsSuccess = true;
                            }

                            // 移除当前键
                            keys.RemoveAt(i);
                            break;
                        }
                    }
                }
            }

            // 填充参数
            Populate();
        }

        /// <summary>
        /// 填充参数。
        /// </summary>
        protected virtual void Populate()
        {
            // 遍历拦截器列表
            foreach (var opt in Options)
            {
                // 如果成功拦截
                if (opt.IsSuccess)
                {
                    // 建立实例（注：拦截器参数集合并非 QueryBase 所需的构造参数，需自行创建实例并更新）
                    Type type = opt.ApplicationType;
                    if (!ReferenceEquals(opt.ImplementationType, null))
                    {
                        type = opt.ImplementationType;
                    }
                    // 构造参数依次为：RequestBase、QueryInterceptorOption
                    object instance = Activator.CreateInstance(type, this, opt);
                    opt.UpdateInstance(instance);

                    // 公开常用查询
                    if (opt.ApplicationType == typeof(PagerQueryBase))
                    {
                        Pager = (PagerQueryBase)instance;
                    }
                    else if (opt.ApplicationType == typeof(FilterQueryBase))
                    {
                        Filter = (FilterQueryBase)instance;
                    }
                    else if (opt.ApplicationType == typeof(SorterQueryBase))
                    {
                        Sorter = (SorterQueryBase)instance;
                    }
                    else
                    {
                    }
                }
            }
        }

        /// <summary>
        /// 获取当前拦截器列表。
        /// </summary>
        public IList<QueryInterceptorOption> Options { get; private set; }

        /// <summary>
        /// 获取分页器查询。
        /// </summary>
        /// <value>返回分页器查询或空。</value>
        public PagerQueryBase Pager { get; private set; }
        /// <summary>
        /// 获取过滤器查询。
        /// </summary>
        /// <value>返回过滤器查询或空。</value>
        public FilterQueryBase Filter { get; private set; }
        /// <summary>
        /// 获取排序器查询。
        /// </summary>
        /// <value>返回过滤器查询或空。</value>
        public SorterQueryBase Sorter { get; private set; }

    }
}