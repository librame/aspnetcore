// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// 数组变换。
    /// </summary>
    /// <author>Librame Pang</author>
    internal static class ArrayTransformation
    {
        /// <summary>
        /// 获取指定集合的一半元素。
        /// </summary>
        /// <param name="source">给定的字节集合。</param>
        /// <param name="atLast">提取后半部分元素（反之则取前半部分）。</param>
        /// <returns>返回新集合。</returns>
        public static IEnumerable<byte> Half(this IEnumerable<byte> source, bool atLast = true)
        {
            int halfCount = (source.Count() / 2);

            if (atLast)
            {
                return source.Skip(halfCount).Take(halfCount);
            }
            else
            {
                return source.Take(halfCount);
            }
        }

        /// <summary>
        /// 获取成倍扩展集合。
        /// </summary>
        /// <param name="source">给定的字节集合。</param>
        /// <param name="factor">给定的成倍系数（增加倍数的系数）。</param>
        /// <returns>返回新集合。</returns>
        public static IEnumerable<byte> Multiple(this IEnumerable<byte> source, int factor)
        {
            if (factor < 1)
                return source;

            // 给定的字节集合为基础倍
            IList<byte> buffer = new List<byte>(source);

            for (int i = 0; i < factor; i++)
            {
                buffer.Concat(source);
            }

            return buffer;
        }
    }
}