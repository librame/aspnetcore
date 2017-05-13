#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Enumerable"/> 实用工具。
    /// </summary>
    public static class EnumerableUtil
    {

        #region JoinString

        /// <summary>
        /// 将字符串集合连接为字符串。
        /// </summary>
        /// <param name="items">给定的集合。</param>
        /// <param name="connector">给定的连接符（默认为空）。</param>
        /// <returns>返回连接字符串。</returns>
        public static string JoinString(this IEnumerable<string> items, string connector = StringUtil.EMPTY)
        {
            if (items == null)
                return string.Empty;

            var count = items.Count();
            var sb = new StringBuilder();

            items.Invoke((d, i) =>
            {
                if (i < count - 1)
                    sb.Append(d + connector);
                else
                    sb.Append(d);
            });
            
            return sb.ToString();
        }

        /// <summary>
        /// 将类型集合转换为字符串。
        /// </summary>
        /// <typeparam name="T">指定集合中的类型。</typeparam>
        /// <param name="items">给定的集合。</param>
        /// <param name="factory">给定的转换方法。</param>
        /// <param name="connector">给定的连接符（默认为空）。</param>
        /// <returns>返回连接字符串。</returns>
        public static string JoinString<T>(this IEnumerable<T> items, Func<T, string> factory,
            string connector = StringUtil.EMPTY)
        {
            if (items == null)
                return string.Empty;

            var count = items.Count();
            var sb = new StringBuilder();

            items.Invoke((d, i) =>
            {
                var str = factory.Invoke(d);

                if (i < count - 1)
                    sb.Append(str + connector);
                else
                    sb.Append(str);
            });

            return sb.ToString();
        }

        #endregion


        #region Invoke

        /// <summary>
        /// 循环调用方法。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="dispatch">给定的调用方法。</param>
        public static void Invoke<T>(this IEnumerable<T> enumerable, Action<T> dispatch)
        {
            if (ReferenceEquals(enumerable, null))
                return;

            foreach (var sink in enumerable)
            {
                try
                {
                    dispatch(sink);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 循环调用方法。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="dispatch">给定的调用方法。</param>
        public static void Invoke<T>(this IEnumerable<T> enumerable, Action<T, int> dispatch)
        {
            if (ReferenceEquals(enumerable, null))
                return;

            var i = 0;
            foreach (var sink in enumerable)
            {
                try
                {
                    dispatch(sink, i);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                i++;
            }
        }


        /// <summary>
        /// 循环调用方法。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TResult">指定的返回类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="dispatch">给定的调用方法。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<TResult> Invoke<T, TResult>(this IEnumerable<T> enumerable, Func<T, TResult> dispatch)
        {
            if (ReferenceEquals(enumerable, null))
                yield return default(TResult);

            foreach (var sink in enumerable)
            {
                var result = default(TResult);

                try
                {
                    result = dispatch(sink);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                yield return result;
            }
        }

        /// <summary>
        /// 循环调用方法。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TResult">指定的返回类型。</typeparam>
        /// <param name="enumerable">给定的 <see cref="IEnumerable{T}"/>。</param>
        /// <param name="dispatch">给定的调用方法。</param>
        /// <returns>返回 <see cref="IEnumerable{T}"/>。</returns>
        public static IEnumerable<TResult> Invoke<T, TResult>(this IEnumerable<T> enumerable, Func<T, int, TResult> dispatch)
        {
            if (ReferenceEquals(enumerable, null))
                yield return default(TResult);

            var i = 0;
            foreach (var sink in enumerable)
            {
                var result = default(TResult);

                try
                {
                    result = dispatch(sink, i);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                i++;
                yield return result;
            }
        }

        #endregion

    }
}
