// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// Librame 只读集合静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameReadOnlyCollectionExtensions
    {
        /// <summary>
        /// 转化为只读集合对象。
        /// </summary>
        /// <typeparam name="T">指定的集合类型。</typeparam>
        /// <param name="enumerable">给定的可枚举集合对象。</param>
        /// <returns>返回只读集合对象。</returns>
        public static IList<T> ToReadOnlyCollection<T>(this IEnumerable<T> enumerable)
        {
            return new ReadOnlyCollection<T>(enumerable.ToList());
        }

        /// <summary>
        /// 转化为只读集合对象。
        /// </summary>
        /// <typeparam name="T">指定的集合类型。</typeparam>
        /// <param name="list">给定的列表对象。</param>
        /// <returns>返回只读集合对象。</returns>
        public static IList<T> ToReadOnlyCollection<T>(this IList<T> list)
        {
            return new ReadOnlyCollection<T>(list);
        }

    }
}