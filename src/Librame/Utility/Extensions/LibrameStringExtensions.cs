// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace System
{
    /// <summary>
    /// Librame 字符串静态扩展。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class LibrameStringExtensions
    {
        private static T? TryGetValue<T>(bool isNull, Func<T> convert) where T : struct
        {
            return (isNull ? new Nullable<T>() : new Nullable<T>(convert()));
        }
        /// <summary>
        /// 解析指定类型的空结构体。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <param name="str">给定的字符串。</param>
        /// <returns>返回该类型的空结构体。</returns>
        public static T? ParseOrNull<T>(this string str) where T : struct
        {
            bool isNull = String.IsNullOrEmpty(str);

            switch (typeof(T).Name)
            {
                case "SByte":
                    return TryGetValue<sbyte>(isNull, () => SByte.Parse(str)) as T?;

                case "Byte":
                    return TryGetValue<byte>(isNull, () => Byte.Parse(str)) as T?;

                case "Char":
                    return TryGetValue<char>(isNull, () => Char.Parse(str)) as T?;

                case "Int16":
                    return TryGetValue<short>(isNull, () => Int16.Parse(str)) as T?;
                    
                case "Int32":
                    return TryGetValue<int>(isNull, () => Int32.Parse(str)) as T?;

                case "Int64":
                    return TryGetValue<long>(isNull, () => Int64.Parse(str)) as T?;

                case "UInt16":
                    return TryGetValue<ushort>(isNull, () => UInt16.Parse(str)) as T?;

                case "UInt32":
                    return TryGetValue<uint>(isNull, () => UInt32.Parse(str)) as T?;

                case "UInt64":
                    return TryGetValue<ulong>(isNull, () => UInt64.Parse(str)) as T?;

                case "Single":
                    return TryGetValue<float>(isNull, () => Single.Parse(str)) as T?;

                case "Double":
                    return TryGetValue<double>(isNull, () => Double.Parse(str)) as T?;

                case "Decimal":
                    return TryGetValue<decimal>(isNull, () => Decimal.Parse(str)) as T?;

                case "Boolean":
                    return TryGetValue<bool>(isNull, () => Boolean.Parse(str)) as T?;

                case "DateTime":
                    return TryGetValue<DateTime>(isNull, () => DateTime.Parse(str)) as T?;

                case "Guid":
                    return TryGetValue<Guid>(isNull, () => Guid.Parse(str)) as T?;

                case "String":
                    return null; // String 不可为空

                default:
                    goto case "String";
            }
        }

        /// <summary>
        /// 解析指定泛类型的字符串值。
        /// </summary>
        /// <remarks>
        /// 支持枚举类型的解析。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// strType 为空。
        /// </exception>
        /// <typeparam name="T">指定的对象类型。</typeparam>
        /// <param name="str">给定的字符串。</param>
        /// <param name="defaultValue">给定的默认值（可选；默认返回空）。</param>
        /// <returns>返回指定类型的对象。</returns>
        /// <seealso cref="Enum.Parse(Type, string)"/>
        /// <seealso cref="Convert.ChangeType(object, Type)"/>
        public static T Parse<T>(this string str, T defaultValue = default(T))
        {
            return (T)Parse(str, typeof(T), defaultValue);
        }
        /// <summary>
        /// 解析指定类型的字符串值。
        /// </summary>
        /// <remarks>
        /// 支持枚举类型的解析。
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// strType 为空。
        /// </exception>
        /// <param name="str">给定的字符串。</param>
        /// <param name="strType">指定的对象类型。</param>
        /// <param name="defaultValue">给定的默认值（可选；默认返回空）。</param>
        /// <returns>返回指定类型的对象。</returns>
        /// <seealso cref="Enum.Parse(Type, string)"/>
        /// <seealso cref="Convert.ChangeType(object, Type)"/>
        public static object Parse(this string str, Type strType, object defaultValue = null)
        {
            if (!String.IsNullOrEmpty(str))
            {
                if (ReferenceEquals(strType, null))
                {
                    throw new ArgumentNullException("strType");
                }

                // 支持枚举
                if (strType.IsEnum)
                {
                    return Enum.Parse(strType, str);
                }

                return Convert.ChangeType(str, strType);
            }

            return defaultValue;
        }

        /// <summary>
        /// 检测当前字符串，并根据是否为空来返回当前值或默认值。
        /// </summary>
        /// <param name="str">给定的当前字符串。</param>
        /// <param name="defaultValue">给定的默认值（可选；默认返回空字符串）。</param>
        /// <returns>返回字符串。</returns>
        public static string EmptyDefault(this string str, string defaultValue = "")
        {
            if (!String.IsNullOrEmpty(str))
            {
                return str;
            }

            return defaultValue;
        }

    }
}