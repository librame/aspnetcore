// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.Data.Context.Kendo
{
    /// <summary>
    /// Kendo UI 排序器查询。
    /// </summary>
    /// <author>Librame Pang</author>
    public class KendoSorterQuery : SorterQueryBase
    {
        // 查询参数索引列表
        IList<int> _queryIndexs = null;

        /// <summary>
        /// 构造一个 Kendo UI 排序器查询实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// request 与 option 为空。
        /// </exception>
        /// <param name="request">给定的请求对象。</param>
        /// <param name="option">给定的查询拦截器选项。</param>
        public KendoSorterQuery(RequestBase request, QueryInterceptorOption option)
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
            var fieldConfigurator = (Option.Parameters[0] as QueryInterceptorParameter);
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
        /// 获取信息集合。
        /// </summary>
        /// <returns>返回信息集合。</returns>
        protected override IEnumerable<SortingInfo> GetInfos()
        {
            var infos = new List<SortingInfo>();

            var fieldConfigurator = (Option.Parameters[0] as QueryInterceptorParameter);
            var fieldKeys = fieldConfigurator.Queries.Keys.ToList();
            var dirConfigurator = (Option.Parameters[1] as QueryInterceptorParameter);
            var dirKeys = dirConfigurator.Queries.Keys.ToList();

            int maxIndex = _queryIndexs.Max();
            int i = 0;
            while (i <= maxIndex)
            {
                string fieldKey = fieldKeys[i];
                string dirKey = dirKeys[i];

                infos.Add(new SortingInfo
                {
                    Field = fieldConfigurator.Queries[fieldKey],
                    Direction = SortingInfo.ParseDirection(dirConfigurator.Queries[dirKey])
                });

                i++;
            }

            return infos;
        }

        #endregion

    }
}