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
        /// <param name="extractSingular">提取奇数行元素（反之则取偶数行）。</param>
        /// <returns>返回新集合。</returns>
        public static IEnumerable<byte> Half(this IEnumerable<byte> source, bool extractSingular = true)
        {
            if (extractSingular)
            {
                // 获取奇数行（跳过偶数行）
                return source.SkipWhile((b, i) => (i % 2 == 0));
            }
            else
            {
                // 获取偶数行数组（跳过奇数行）
                return source.SkipWhile((b, i) => (i % 2 != 0));
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