// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Librame.Utility
{
    /// <summary>
    /// 反射工具。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class ReflectionUtils
    {
        /// <summary>
        /// 默认绑定搜索标志。
        /// </summary>
        public const BindingFlags DefaultBindingFlags = BindingFlags.Public | BindingFlags.Instance;


        #region PropertyInfo

        /// <summary>
        /// 获取指定名称的属性。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="name">给定的名称。</param>
        /// <param name="bindingAttr">给定的绑定搜索标志。</param>
        /// <returns>返回属性。</returns>
        public static PropertyInfo GetProperty<T>(string name, BindingFlags bindingAttr = DefaultBindingFlags)
        {
            return typeof(T).GetProperty(name, bindingAttr);
        }

        /// <summary>
        /// 获取指定类型的属性集合。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="bindingAttr">给定的绑定搜索标志。</param>
        /// <returns>返回属性集合。</returns>
        public static IEnumerable<PropertyInfo> GetProperties<T>(BindingFlags bindingAttr = DefaultBindingFlags)
        {
            return typeof(T).GetProperties(bindingAttr);
        }
        /// <summary>
        /// 获取指定类型的属性集合。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="bindingAttr">给定的绑定搜索标志。</param>
        /// <returns>返回属性集合。</returns>
        public static IEnumerable<PropertyInfo> GetProperties(Type type, BindingFlags bindingAttr = DefaultBindingFlags)
        {
            return type.GetProperties(bindingAttr);
        }

        /// <summary>
        /// 投影指定类型的结果集合到新表中。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="selector">给定的选择器。</param>
        /// <param name="bindingAttr">给定的绑定搜索标志。</param>
        /// <returns>返回结果集合。</returns>
        public static IEnumerable<TResult> SelectProperties<T, TResult>(Func<PropertyInfo, int, TResult> selector, BindingFlags bindingAttr = DefaultBindingFlags)
        {
            Type type = typeof(T);

            return SelectProperties<TResult>(type, selector, bindingAttr);
        }
        /// <summary>
        /// 投影指定类型的结果集合到新表中。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="selector">给定的选择器。</param>
        /// <param name="bindingAttr">给定的绑定搜索标志。</param>
        /// <returns>返回结果集合。</returns>
        public static IEnumerable<TResult> SelectProperties<T, TResult>(Func<PropertyInfo, TResult> selector, BindingFlags bindingAttr = DefaultBindingFlags)
        {
            Type type = typeof(T);

            return SelectProperties<TResult>(type, selector, bindingAttr);
        }

        /// <summary>
        /// 投影指定类型的结果集合到新表中。
        /// </summary>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="type">给定的类型。</param>
        /// <param name="selector">给定的选择器。</param>
        /// <param name="bindingAttr">给定的绑定搜索标志。</param>
        /// <returns>返回结果集合。</returns>
        public static IEnumerable<TResult> SelectProperties<TResult>(Type type, Func<PropertyInfo, int, TResult> selector, BindingFlags bindingAttr = DefaultBindingFlags)
        {
            var pis = type.GetProperties(bindingAttr);

            return pis.Select(selector);
        }
        /// <summary>
        /// 投影指定类型的结果集合到新表中。
        /// </summary>
        /// <typeparam name="TResult">指定的结果类型。</typeparam>
        /// <param name="type">给定的类型。</param>
        /// <param name="selector">给定的选择器。</param>
        /// <param name="bindingAttr">给定的绑定搜索标志。</param>
        /// <returns>返回结果集合。</returns>
        public static IEnumerable<TResult> SelectProperties<TResult>(Type type, Func<PropertyInfo, TResult> selector, BindingFlags bindingAttr = DefaultBindingFlags)
        {
            var pis = type.GetProperties(bindingAttr);

            return pis.Select(selector);
        }

        #endregion

    }
}