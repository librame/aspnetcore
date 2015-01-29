// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity;
using System;

namespace Librame.Data
{
    /// <summary>
    /// 访问器抽象工厂。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class AccessorFactory
    {
        /// <summary>
        /// 创建访问器实例。
        /// </summary>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        /// <param name="options">给定的 <see cref="DbContextOptions"/>。</param>
        /// <returns>返回访问器对象。</returns>
        public abstract IAccessor Create(IServiceProvider serviceProvider, DbContextOptions options);
        /// <summary>
        /// 创建访问器实例。
        /// </summary>
        /// <param name="context">给定的 <see cref="DbContext"/>。</param>
        /// <returns>返回访问器对象。</returns>
        public abstract IAccessor Create(DbContext context);
    }
}