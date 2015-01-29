// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data.Models;
using Microsoft.AspNet.Http;
using Microsoft.Data.Entity;

namespace Librame.Context
{
    /// <summary>
    /// 带整数型编号的上下文数据仓库。
    /// </summary>
    /// <typeparam name="TDbContext">指定的 <see cref="DbContext"/> 类型。</typeparam>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <author>Librame Pang</author>
    public class ContextRepository<TDbContext, TEntity> : ContextRepository<TDbContext, TEntity, int>
        where TDbContext : DbContext
        where TEntity : IdEntity
    {
        /// <summary>
        /// 构造一个带整数型编号的上下文数据仓库实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// context 为空。
        /// </exception>
        /// <param name="context">给定的 <see cref="HttpContext"/>。</param>
        public ContextRepository(HttpContext context)
            : base(context)
        {
        }
    }
}