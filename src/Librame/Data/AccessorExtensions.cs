// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Data
{
    /// <summary>
    /// 访问器静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class AccessorExtensions
    {
        /// <summary>
        /// 创建实体仓库实例。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="accessor">给定的当前访问器。</param>
        /// <returns>返回实体仓库对象。</returns>
        public static IRepository<T> CreateRepository<T>(this IAccessor accessor)
            where T : class
        {
            return new Repository<T>(accessor);
        }

    }
}