// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;

namespace Librame.Context.Mvc
{
    /// <summary>
    /// 带泛类型编号的 API 控制器。
    /// </summary>
    /// <typeparam name="TDbContext">指定的 <see cref="DbContext"/> 类型。</typeparam>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <typeparam name="TId">指定的实体编号类型。</typeparam>
    /// <author>Librame Pang</author>
    [Route("api/[controller]")]
    public class ControllerApi<TDbContext, TEntity, TId> : ControllerBase<TDbContext, TEntity, TId>
        where TDbContext : DbContext
        where TEntity : IdEntity<TId>
    {
        /// <summary>
        /// 获取数据集合（GET: api/values）。
        /// </summary>
        /// <remarks>
        /// 支持分页查询。
        /// </remarks>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpGet]
        public virtual IActionResult Get()
        {
            var take = Request.Query["take"].ParseOrNull<int>();

            if (take.HasValue && take.Value > 0)
            {
                var skip = Request.Query["skip"].ParseOrNull<int>();

                return this.JsonUTF8Content(Repository.GetPagedList(skip.HasValue ? skip.Value : 0, take.Value));
            }
            else
            {
                return Json(Repository.GetList());
            }
        }

        /// <summary>
        /// 获取指定 ID 的数据（GET: api/values/5）。
        /// </summary>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpGet("{id}")]
        public virtual IActionResult Get(TId id)
        {
            return Json(Repository.GetSingle(id));
        }

        /// <summary>
        /// 创建数据集合（POST: api/values）。
        /// </summary>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPost]
        public virtual IActionResult Post([FromForm]string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            // Create
            return Json(Repository.Creating(entities));
        }

        /// <summary>
        /// 更新数据集合（PUT: api/values）。
        /// </summary>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPut()]
        public virtual IActionResult Put([FromForm] string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            // Update
            return Json(Repository.Updating(entities));
        }

        /// <summary>
        /// 删除数据集合（DELETE: api/values）。
        /// </summary>
        /// <remarks>
        /// 支持逻辑（如果当前实体可从 <see cref="DataIdEntity"/> 分配）或物理删除。
        /// </remarks>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpDelete()]
        public virtual IActionResult Delete([FromForm]string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            if (typeof(DataIdEntity<TId>).IsAssignableFrom(typeof(TEntity)))
            {
                entities = Repository.LogicalDelete(entities);
            }
            else
            {
                entities = Repository.PhysicalDelete(entities);
            }

            return Json(entities);
        }

    }
}
