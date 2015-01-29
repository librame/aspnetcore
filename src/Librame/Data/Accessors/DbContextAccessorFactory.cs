// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity;
using System;

namespace Librame.Data.Accessors
{
    /// <summary>
    /// DbContext 访问器工厂。
    /// </summary>
    /// <author>Librame Pang</author>
    public class DbContextAccessorFactory : AccessorFactory
    {
        /// <summary>
        /// 创建访问器实例。
        /// </summary>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        /// <returns>返回访问器对象。</returns>
        public override IAccessor Create(IServiceProvider serviceProvider, DbContextOptions options)
        {
            var context = new DbContext(serviceProvider, options);

            return Create(context);
        }
        /// <summary>
        /// 创建访问器实例。
        /// </summary>
        /// <param name="context">给定的 <see cref="DbContext"/>。</param>
        /// <returns>返回访问器对象。</returns>
        public override IAccessor Create(DbContext context)
        {
            return new DbContextAccessor(context);
        }

    }
}