// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Librame.Utility
{
    /// <summary>
    /// 枚举工具。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class EnumUtils
    {
        /// <summary>
        /// 数据范围枚举信息集合。
        /// </summary>
        public static IEnumerable<EnumInfo> DataScopes
        {
            get { return ParseInfos<Librame.Data.DataScope>().OrderBy(k => k.Value); }
        }
        /// <summary>
        /// 跟踪状态枚举信息集合。
        /// </summary>
        public static IEnumerable<EnumInfo> TrackStates
        {
            get { return ParseInfos<Librame.Data.TrackState>().OrderBy(k => k.Value); }
        }
        /// <summary>
        /// 过滤运算方式枚举信息集合。
        /// </summary>
        public static IEnumerable<EnumInfo> FilterOperations
        {
            get { return ParseInfos<Librame.Data.Context.FilterOperation>().OrderBy(k => k.Value); }
        }
        /// <summary>
        /// 排序方向枚举信息集合。
        /// </summary>
        public static IEnumerable<EnumInfo> SortDirections
        {
            get { return ParseInfos<Librame.Data.Context.SortDirection>().OrderBy(k => k.Value); }
        }


        #region EnumInfo

        static ConcurrentDictionary<Type, IEnumerable<EnumInfo>> _cache =
            new ConcurrentDictionary<Type, IEnumerable<EnumInfo>>();

        /// <summary>
        /// 解析指定枚举类型的信息集合。
        /// </summary>
        /// <remarks>
        /// 支持枚举项的 <see cref="DescriptionAttribute"/> 自定义属性（如果此属性存在）。
        /// </remarks>
        /// <typeparam name="TEnum">指定的枚举类型。</typeparam>
        /// <returns>返回枚举信息集合。</returns>
        public static IEnumerable<EnumInfo> ParseInfos<TEnum>() where TEnum : struct
        {
            return ParseInfos(typeof(TEnum));
        }
        /// <summary>
        /// 解析指定枚举类型的信息集合。
        /// </summary>
        /// <remarks>
        /// 支持枚举项的 <see cref="DescriptionAttribute"/> 自定义属性（如果此属性存在）。
        /// </remarks>
        /// <param name="type">给定的枚举类型。</param>
        /// <returns>返回枚举信息集合。</returns>
        public static IEnumerable<EnumInfo> ParseInfos(Type type)
        {
            return _cache.GetOrAdd(type, ToInfos);
        }
        private static IEnumerable<EnumInfo> ToInfos(Type type)
        {
            if (!type.IsEnum)
                throw new ArgumentException("This method only supports enumeration.");

            foreach (int value in type.GetEnumValues())
            {
                string name = type.GetEnumName(value);
                var fi = type.GetField(name, BindingFlags.Public | BindingFlags.Static);

                yield return new EnumInfo(name, value, GetDescription(fi));
            }
        }
        private static string GetDescription(FieldInfo fieldInfo)
        {
            var objs = fieldInfo.GetCustomAttributes(false);

            foreach (object obj in objs)
            {
                if (obj is DescriptionAttribute)
                {
                    return (obj as DescriptionAttribute).Description;
                }
            }

            return fieldInfo.Name;
        }

        #endregion

    }
}