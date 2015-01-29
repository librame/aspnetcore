// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// Librame 枚举静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameEnumerableExtensions
    {
        /// <summary>
        /// 将单个对象解析为枚举集合对象。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="item">给定的对象。</param>
        /// <returns>返回枚举集合。</returns>
        public static IEnumerable<T> ParseEnumerable<T>(this T item)
        {
            yield return item;
        }

    }
}