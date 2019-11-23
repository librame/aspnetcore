#region License

/* **************************************************************************************
 * Copyright (c) zwbwl All rights reserved.
 * 
 * http://51zwb.com
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Extensions;
using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// 抽象 LINQ 静态扩展。
    /// </summary>
    public static class AbstractionLinqExtensions
    {
        /// <summary>
        /// 存在指定断定条件项。
        /// </summary>
        /// <typeparam name="TSource">指定的源类型。</typeparam>
        /// <param name="sources">给定的源集合。</param>
        /// <param name="predicate">给定的断定条件。</param>
        /// <param name="source">输出满足断定条件的源实例。</param>
        /// <returns>返回布尔值。</returns>
        public static bool Any<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate, out TSource source)
        {
            if (sources.IsEmpty())
            {
                source = default;
                return false;
            }

            source = sources.FirstOrDefault(predicate);
            return source.IsNotNull();
        }

    }
}
