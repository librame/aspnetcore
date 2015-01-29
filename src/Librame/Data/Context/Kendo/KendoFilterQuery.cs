// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.Data.Context.Kendo
{
    /// <summary>
    /// Kendo UI 过滤器查询。
    /// </summary>
    /// <author>Librame Pang</author>
    public class KendoFilterQuery : FilterQueryBase
    {
        // 查询参数索引列表
        IList<int> _queryIndexs = null;

        /// <summary>
        /// 构造一个 Kendo UI 过滤器查询实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// request 与 option 为空。
        /// </exception>
        /// <param name="request">给定的请求对象。</param>
        /// <param name="option">给定的查询拦截器选项。</param>
        public KendoFilterQuery(RequestBase request, QueryInterceptorOption option)
            : base(request, option)
        {
        }


        #region Query

        /// <summary>
        /// 填充请求参数。
        /// </summary>
        protected override void Populate()
        {
            _queryIndexs = new List<int>();

            // 提取最大索引数（使用字段配置器）
            var fieldConfigurator = (Option.Parameters[1] as QueryInterceptorParameter);
            foreach (var pair in fieldConfigurator.Queries)
            {
                var m = fieldConfigurator.Rule.Match(pair.Key);
                if (m.Success)
                {
                    // 提取当前索引（3个捕获组，第2个为索引）
                    var groups = m.Groups;
                    if (!String.IsNullOrEmpty(groups[2].Value))
                    {
                        _queryIndexs.Add(groups[2].Value.Parse(-1));
                    }
                }
            }

            base.Populate();
        }

        /// <summary>
        /// 获取逻辑字符串。
        /// </summary>
        /// <returns>返回逻辑字符串。</returns>
        protected override string GetLogic()
        {
            return (Option.Parameters[0] as QueryInterceptorParameter).Queries.FirstOrDefault().Value.EmptyDefault("AND");
        }
        /// <summary>
        /// 获取信息集合。
        /// </summary>
        /// <returns>返回信息集合。</returns>
        protected override IEnumerable<FiltrationInfo> GetInfos()
        {
            var infos = new List<FiltrationInfo>();

            // 第0索引为逻辑字符
            var fieldConfigurator = (Option.Parameters[1] as QueryInterceptorParameter);
            var fieldKeys = fieldConfigurator.Queries.Keys.ToList();
            var valueConfigurator = (Option.Parameters[2] as QueryInterceptorParameter);
            var valueKeys = valueConfigurator.Queries.Keys.ToList();
            var operatorConfigurator = (Option.Parameters[3] as QueryInterceptorParameter);
            var operatorKeys = operatorConfigurator.Queries.Keys.ToList();
            var ignoreCaseConfigurator = (Option.Parameters[4] as QueryInterceptorParameter);
            var ignoreCaseKeys = ignoreCaseConfigurator.Queries.Keys.ToList();

            int maxIndex = _queryIndexs.Max();
            int i = 0;
            while (i <= maxIndex)
            {
                string fieldKey = fieldKeys[i];
                string valueKey = valueKeys[i];
                string theOperatorKey = operatorKeys[i];
                // 忽略大小写可能不存在
                string ignoreCaseKey = (ignoreCaseKeys.Count > 0 ? ignoreCaseKeys[i] : String.Empty);

                infos.Add(new FiltrationInfo
                {
                    Field = fieldConfigurator.Queries[fieldKey],
                    Value = valueConfigurator.Queries[valueKey].EmptyDefault(),
                    Operator = FiltrationInfo.ParseOperator(operatorConfigurator.Queries[theOperatorKey]),
                    IgnoreCase = (String.IsNullOrEmpty(ignoreCaseKey) ? true : ignoreCaseConfigurator.Queries[ignoreCaseKey].Parse(true))
                });

                i++;
            }

            return infos;
        }

        #endregion

    }
}