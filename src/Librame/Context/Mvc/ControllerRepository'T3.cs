// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Librame.Context.Mvc
{
    /// <summary>
    /// 带泛类型编号的 Repository 控制器。
    /// </summary>
    /// <typeparam name="TDbContext">指定的 <see cref="DbContext"/> 类型。</typeparam>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <typeparam name="TId">指定的实体编号类型。</typeparam>
    /// <author>Librame Pang</author>
    public class ControllerRepository<TDbContext, TEntity, TId> : ControllerBase<TDbContext, TEntity, TId>
        where TDbContext : DbContext
        where TEntity : IdEntity<TId>
    {
        /// <summary>
        /// 首页视图（即实体集合查询请求）。
        /// </summary>
        /// <remarks>
        /// 支持分页查询。
        /// </remarks>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpGet]
        public virtual IActionResult Index()
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
        /// 异步首页视图（即实体集合查询请求）。
        /// </summary>
        /// <remarks>
        /// 支持分页查询。
        /// </remarks>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpGet]
        public virtual async Task<IActionResult> IndexAsync()
        {
            var take = Request.Query["take"].ParseOrNull<int>();

            if (take.HasValue && take.Value > 0)
            {
                var skip = Request.Query["skip"].ParseOrNull<int>();

                return this.JsonUTF8Content(await Repository.GetPagedListAsync(skip.HasValue ? skip.Value : 0, take.Value));
            }
            else
            {
                return Json(await Repository.GetListAsync());
            }
        }

        /// <summary>
        /// 详情视图（即实体查询请求）。
        /// </summary>
        /// <param name="id">给定的编号。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpGet]
        public virtual IActionResult Detail(TId id = default(TId))
        {
            return Json(Repository.GetSingle(id));
        }
        /// <summary>
        /// 异步详情视图（即实体查询请求）。
        /// </summary>
        /// <param name="id">给定的编号。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpGet]
        public virtual async Task<IActionResult> DetailAsync(TId id = default(TId))
        {
            return Json(await Repository.GetSingleAsync(id));
        }

        /// <summary>
        /// 创建视图。
        /// </summary>
        /// <returns>返回空视图。</returns>
        [HttpGet]
        public virtual IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// 创建视图（即实体集合创建请求）。
        /// </summary>
        /// <param name="models">给定 JSON 格式字符串的实体集合。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPost]
        public virtual IActionResult Create(string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            return Json(Repository.Creating(entities));
        }
        /// <summary>
        /// 异步创建视图（即实体集合创建请求）。
        /// </summary>
        /// <param name="models">给定 JSON 格式字符串的实体集合。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            return Json(await Repository.CreatingAsync(entities));
        }

        /// <summary>
        /// 更新视图。
        /// </summary>
        /// <returns>返回空视图。</returns>
        [HttpGet]
        public virtual IActionResult Update()
        {
            return View();
        }
        /// <summary>
        /// 更新视图（即实体集合更新请求）。
        /// </summary>
        /// <param name="models">给定 JSON 格式字符串的实体集合。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPost]
        public virtual IActionResult Update(string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            return Json(Repository.Updating(entities));
        }
        /// <summary>
        /// 异步更新视图（即实体集合更新请求）。
        /// </summary>
        /// <param name="models">给定 JSON 格式字符串的实体集合。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPost]
        public virtual async Task<IActionResult> UpdateAsync(string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            return Json(await Repository.UpdatingAsync(entities));
        }

        /// <summary>
        /// 删除视图。
        /// </summary>
        /// <returns>返回空视图。</returns>
        [HttpGet]
        public virtual IActionResult Delete()
        {
            return View();
        }
        /// <summary>
        /// 删除视图（即实体集合删除请求）。
        /// </summary>
        /// <remarks>
        /// 支持逻辑（如果当前实体可从 <see cref="DataIdEntity"/> 分配）或物理删除。
        /// </remarks>
        /// <param name="models">给定 JSON 格式字符串的实体集合。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPost]
        public virtual IActionResult Delete(string models)
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
        /// <summary>
        /// 异步删除视图（即实体集合删除请求）。
        /// </summary>
        /// <remarks>
        /// 支持逻辑（如果当前实体可从 <see cref="DataIdEntity"/> 分配）或物理删除。
        /// </remarks>
        /// <param name="models">给定 JSON 格式字符串的实体集合。</param>
        /// <returns>输出 JSON 格式的字符串。</returns>
        [HttpPost]
        public virtual async Task<IActionResult> DeleteAsync(string models)
        {
            var entities = models.JsonDeserialize<IEnumerable<TEntity>>();

            if (typeof(DataIdEntity<TId>).IsAssignableFrom(typeof(TEntity)))
            {
                entities = await Repository.LogicalDeleteAsync(entities);
            }
            else
            {
                entities = await Repository.PhysicalDeleteAsync(entities);
            }

            return Json(entities);
        }

        /// <summary>
        /// 保存变化的实体。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        [HttpGet]
        public virtual IActionResult Save()
        {
            int status = Repository.Flush();

            return Content(status.ToString());
        }
        /// <summary>
        /// 异步保存变化的实体。
        /// </summary>
        /// <returns>返回受影响的行数。</returns>
        [HttpGet]
        public virtual async Task<IActionResult> SaveAsync()
        {
            int status = await Repository.FlushAsync();

            return Content(status.ToString());
        }

    }
}