// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data.Models;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Librame.Context.Mvc
{
    /// <summary>
    /// 带整数型编号的控制器抽象基类。
    /// </summary>
    /// <typeparam name="TDbContext">指定的 <see cref="DbContext"/> 类型。</typeparam>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <author>Librame Pang</author>
    public abstract class ControllerBase<TDbContext, TEntity> : ControllerBase<TDbContext, TEntity, int>
        where TDbContext : DbContext
        where TEntity : IdEntity<int>
    {
        /// <summary>
        /// 验证编号不为空。
        /// </summary>
        /// <param name="id">给定的编号。</param>
        /// <returns>返回是否为空的布尔值。</returns>
        protected override bool IdIsNotNull(int id)
        {
            return (id > 0);
        }

        /// <summary>
        /// 验证指定编号与当前编号是否相等。
        /// </summary>
        /// <param name="id">给定的编号。</param>
        /// <param name="compareId">给定的对比编号。</param>
        /// <returns>返回是否相等的布尔值。</returns>
        protected override bool IdEquals(int id, int compareId)
        {
            return (id == compareId);
        }


        #region GetList

        /// <summary>
        /// 自定义分页实体列表完成处理。
        /// </summary>
        /// <param name="pagedList">给定的分页实体列表。</param>
        /// <returns>返回实体集合（默认支持绑定初始项）。</returns>
        protected override IPageable<TEntity> PagedListComplete(IPageable<TEntity> pagedList)
        {
            // 绑定初始项
            var initid = Request.Query["initid"].ParseOrNull<int>();
            if (initid.HasValue && initid.Value > 0)
            {
                var exist = pagedList.Rows.FirstOrDefault(p => p.Id == initid.Value);
                // 如果当前列表不存在初始项，则附加
                if (ReferenceEquals(exist, null))
                {
                    var first = Repository.GetSingle(initid.Value);
                    pagedList.Rows.Insert(0, first);
                }
            }

            return base.PagedListComplete(pagedList);
        }

        #endregion

    }
}