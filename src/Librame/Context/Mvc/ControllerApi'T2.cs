// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Data.Models;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

namespace Librame.Context.Mvc
{
    /// <summary>
    /// 带泛类型编号的 API 控制器。
    /// </summary>
    /// <typeparam name="TDbContext">指定的 <see cref="DbContext"/> 类型。</typeparam>
    /// <typeparam name="TEntity">指定的实体类型。</typeparam>
    /// <author>Librame Pang</author>
    [Route("api/[controller]")]
    public class ControllerApi<TDbContext, TEntity> : ControllerApi<TDbContext, TEntity, int>
        where TDbContext : DbContext
        where TEntity : IdEntity<int>
    {
    }
}