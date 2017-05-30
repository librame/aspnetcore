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
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace LibrameCore.Utilities
{
    /// <summary>
    /// 搜索方式。
    /// </summary>
    [Description("搜索方式")]
    public enum SearchMode
    {
        /// <summary>
        /// 默认。
        /// </summary>
        [Description("默认")]
        Default = 0,

        /// <summary>
        /// 包含。
        /// </summary>
        [Description("包含")]
        Include = 1,

        /// <summary>
        /// 排除。
        /// </summary>
        [Description("排除")]
        Exclude = 2
    }


    /// <summary>
    /// <see cref="Type"/> 实用工具。
    /// </summary>
    public static class TypeUtility
    {

        /// <summary>
        /// 转换为指定类型实例（虚方法，不执行实际转换）。
        /// </summary>
        /// <typeparam name="TValue">指定的值类型。</typeparam>
        /// <param name="value">给定的当前值类型实例。</param>
        /// <param name="defaultValue">如果实例为空要返回的默认值。</param>
        /// <returns>返回当前值或默认值。</returns>
        public static TValue AsOrDefault<TValue>(this TValue value, TValue defaultValue)
        {
            return value.AsOrDefault(defaultValue, v => v);
        }

        /// <summary>
        /// 泛型类型通用转换。
        /// </summary>
        /// <typeparam name="TInput">指定的输入类型。</typeparam>
        /// <typeparam name="TOutput">指定的输出类型。</typeparam>
        /// <param name="input">给定的输入类型实例。</param>
        /// <param name="defaultValue">如果类型实例为空或转换失败要返回的默认值。</param>
        /// <param name="factory">给定的转换方法。</param>
        /// <returns>返回经过转换的值或默认值。</returns>
        public static TOutput AsOrDefault<TInput, TOutput>(this TInput input, TOutput defaultValue,
            Func<TInput, TOutput> factory)
        {
            if (input == null)
                return defaultValue;

            try
            {
                return factory.Invoke(input);
            }
            catch //(Exception ex)
            {
                return defaultValue;
            }
        }


        /// <summary>
        /// 转换为键名。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <returns>返回字符串。</returns>
        public static string AsKey<T>()
        {
            return typeof(T).AsKey();
        }
        /// <summary>
        /// 转换为键名。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <returns>返回字符串。</returns>
        public static string AsKey(this Type type)
        {
            return type.NotNull(nameof(type)).AssemblyQualifiedNameWithoutVcp();
        }


        #region Assembly

        /// <summary>
        /// 获取指定类型的程序集限定名，但不包括版本、文化、公钥标记等信息（如 Librame.Utility.TypeUtil, Librame）。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <returns>返回字符串。</returns>
        public static string AssemblyQualifiedNameWithoutVcp<T>()
        {
            return typeof(T).AssemblyQualifiedNameWithoutVcp();
        }
        /// <summary>
        /// 获取指定类型的程序集限定名，但不包括版本、文化、公钥标记等信息（如 Librame.Utility.TypeUtil, Librame）。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <returns>返回字符串。</returns>
        public static string AssemblyQualifiedNameWithoutVcp(this Type type)
        {
            return string.Format("{0}, {1}", type.FullName, type.GetTypeInfo().Assembly.GetName().Name);
        }


        /// <summary>
        /// 获取指定类型所在的程序集。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <returns>返回程序集。</returns>
        public static Assembly GetAssembly<T>()
        {
            return typeof(T).GetAssembly();
        }
        /// <summary>
        /// 获取指定类型所在的程序集。
        /// </summary>
        /// <param name="type">指定的类型。</param>
        /// <returns>返回程序集。</returns>
        public static Assembly GetAssembly(this Type type)
        {
            return type.NotNull(nameof(type)).GetTypeInfo().Assembly;
        }

        /// <summary>
        /// 获取指定类型所在的程序集标识。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <returns>返回程序集标识。</returns>
        public static AssemblyName GetAssemblyName<T>()
        {
            return typeof(T).GetAssemblyName();
        }
        /// <summary>
        /// 获取指定类型所在的程序集标识。
        /// </summary>
        /// <param name="type">指定的类型。</param>
        /// <returns>返回程序集标识。</returns>
        public static AssemblyName GetAssemblyName(this Type type)
        {
            return new AssemblyName(type.NotNull(nameof(type)).GetTypeInfo().Assembly.FullName);
        }

        #endregion


        #region AsPairsString

        /// <summary>
        /// 将对象转换为键值对集合的字符串形式。
        /// </summary>
        /// <param name="obj">给定的对象。</param>
        /// <param name="searchMode">给定的搜索模式（可选；默认不进行成员名称筛选）。</param>
        /// <param name="searchNames">要排除的成员名称数组。</param>
        /// <returns>返回字符串。</returns>
        public static string AsPairsString(this object obj, SearchMode searchMode = SearchMode.Default,
            params string[] searchNames)
        {
            obj.NotNull(nameof(obj));

            var properties = obj.GetType().SearchProperties(searchMode, searchNames);

            // 如果有需要处理的属性信息集合
            if (properties.Length > 0)
            {
                var sb = new StringBuilder();

                var last = properties.Last();
                foreach (var p in properties)
                {
                    var o = p.GetValue(obj);
                    var value = string.Empty;

                    if (o != null)
                    {
                        var typeInfo = p.PropertyType.GetTypeInfo();

                        // 字符串不属于值类型
                        if (typeInfo.IsValueType || typeInfo.IsAnsiClass)
                            value = o.ToString();
                        else
                            value = o.AsPairsString(SearchMode.Default);
                    }

                    sb.AppendFormat("{0}{1}{2}", p.Name, StringUtility.EQUALITY, value);

                    if (p.Name != last.Name)
                        sb.Append(",");
                }

                return sb.ToString();
            }

            return string.Empty;
        }


        /// <summary>
        /// 将键值对集合的字符串还原为对象。
        /// </summary>
        /// <typeparam name="T">指定的对象类型。</typeparam>
        /// <param name="str">给定的字符串。</param>
        /// <param name="searchMode">给定的搜索模式（可选；默认不进行成员名称筛选）。</param>
        /// <param name="searchNames">要排除的成员名称数组。</param>
        /// <returns>返回对象。</returns>
        public static T FromPairsString<T>(this string str, SearchMode searchMode = SearchMode.Default,
            params string[] searchNames)
        {
            return (T)str.FromPairsString(typeof(T), searchMode, searchNames);
        }
        /// <summary>
        /// 将键值对集合的字符串还原为对象。
        /// </summary>
        /// <param name="str">给定的字符串。</param>
        /// <param name="type">给定的对象类型。</param>
        /// <param name="searchMode">给定的搜索模式（可选；默认不进行成员名称筛选）。</param>
        /// <param name="searchNames">要排除的成员名称数组。</param>
        /// <returns>返回对象。</returns>
        public static object FromPairsString(this string str, Type type, SearchMode searchMode = SearchMode.Default,
            params string[] searchNames)
        {
            str.NotEmpty(nameof(str));
            type.NotNull(nameof(type));

            var obj = Activator.CreateInstance(type);

            var properties = type.SearchProperties(searchMode, searchNames);

            // 如果有需要处理的属性信息集合
            if (properties.Length > 0)
            {
                var pairs = str.Split(',');
                foreach (var pair in pairs)
                {
                    if (string.IsNullOrEmpty(pair))
                        continue;

                    // 不使用 Split('=') 方法，会影响算法字符串的键值对
                    var kv = pair.SplitPair();
                    if (string.IsNullOrEmpty(kv.Key) || string.IsNullOrEmpty(kv.Value))
                        continue; // 不标准的键值对或空字符串值

                    // 检索属性
                    var pi = properties.First(p => p.Name == kv.Key);
                    if (pi == null)
                        continue;

                    // 还原值
                    var rawValue = kv.Value.AsOrDefault(pi.PropertyType);
                    if (rawValue != null)
                        pi.SetValue(obj, rawValue);
                }
            }

            return obj;
        }


        /// <summary>
        /// 搜索指定类型的属性集合。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <param name="searchMode">给定的搜索模式（可选；默认不进行成员名称筛选）。</param>
        /// <param name="searchNames">要排除的成员名称数组。</param>
        /// <returns>返回属性数组。</returns>
        public static PropertyInfo[] SearchProperties(this Type type, SearchMode searchMode = SearchMode.Default,
            params string[] searchNames)
        {
            var properties = type.GetProperties();

            // 如果需要成员名称筛选
            if (searchNames.Length > 0)
            {
                // 排除模式
                if (searchMode == SearchMode.Exclude)
                {
                    foreach (var key in searchNames)
                    {
                        // 越来越少
                        properties = properties.Where(f => f.Name != key).ToArray();
                    }
                }
                // 包含模式
                else if (searchMode == SearchMode.Include)
                {
                    var list = new List<PropertyInfo>();

                    foreach (var name in searchNames)
                    {
                        var p = properties.Where(f => f.Name == name);
                        list.AddRange(p);
                    }

                    // 移除可能存在的重复项
                    properties = list.Distinct().ToArray();
                }
                else
                {
                    // 默认不操作
                }
            }

            return properties;
        }

        #endregion


        #region CopyTo

        /// <summary>
        /// 将源类型实例复制到新实例。
        /// </summary>
        /// <param name="source">给定的源类型实例。</param>
        /// <returns>返回新实例。</returns>
        public static T CopyToCreate<T>(this T source)
            where T : class, new()
        {
            var target = Activator.CreateInstance<T>();
            CopyTo(source, target);

            return target;
        }
        /// <summary>
        /// 将源类型实例复制到目标类型实例。
        /// </summary>
        /// <param name="source">给定的源类型实例。</param>
        /// <param name="target">给定的目标类型实例。</param>
        public static void CopyTo<T>(this T source, T target)
            where T : class
        {
            source.NotNull(nameof(source));
            target.NotNull(nameof(target));

            SetProperties(typeof(T), source, target);
        }

        /// <summary>
        /// 将源对象复制到新对象。
        /// </summary>
        /// <param name="source">给定的源对象。</param>
        /// <returns>返回新对象。</returns>
        public static object CopyToCreate(this object source)
        {
            var target = Activator.CreateInstance(source?.GetType());
            CopyTo(source, target);

            return target;
        }
        /// <summary>
        /// 将源对象复制到目标对象。
        /// </summary>
        /// <param name="source">给定的源对象。</param>
        /// <param name="target">给定的目标对象。</param>
        public static void CopyTo(this object source, object target)
        {
            source = source.NotNull(nameof(source));
            target = target.NotNull(nameof(target));

            var sourceType = source.GetType();
            var targetType = target.GetType();
            if (sourceType != targetType)
            {
                throw new ArgumentException(string.Format("源类型 {0} 与目标类型 {1} 不一致",
                    sourceType.Name, targetType.Name));
            }

            SetProperties(sourceType, source, target);
        }

        private static void SetProperties(Type propertyType, object source, object target)
        {
            var properties = propertyType.GetProperties();
            if (properties.Length < 1)
                return;

            properties.Invoke(pi =>
            {
                var sourceValue = pi.GetValue(source);
                if (sourceValue != null)
                    pi.SetValue(target, sourceValue);
            });
        }

        #endregion


        #region EnumerableTypes

        /// <summary>
        /// 枚举指定程序集中派生于基础类型的所有类型集合。
        /// </summary>
        /// <typeparam name="TBase">指定的基础类型。</typeparam>
        /// <param name="assembly">给定的程序集（可选；默认查找基础类型所在的程序集）。</param>
        /// <param name="withInstantiable">仅支持可实例化类型（可选）。</param>
        /// <param name="withoutBaseType">排除基础类型自身（可选）。</param>
        /// <returns>返回类型数组。</returns>
        public static Type[] EnumerableTypesByAssignableFrom<TBase>(Assembly assembly = null,
            bool withInstantiable = true, bool withoutBaseType = true)
        {
            return typeof(TBase).EnumerableTypesByAssignableFrom(assembly, withInstantiable, withoutBaseType);
        }
        /// <summary>
        /// 枚举指定程序集中派生于基础类型的所有类型集合。
        /// </summary>
        /// <param name="baseType">给定的基础类型。</param>
        /// <param name="assembly">给定的程序集（可选；默认查找基础类型所在的程序集）。</param>
        /// <param name="withInstantiable">仅支持可实例化类型（可选）。</param>
        /// <param name="withoutBaseType">排除基础类型自身（可选）。</param>
        /// <returns>返回类型数组。</returns>
        public static Type[] EnumerableTypesByAssignableFrom(this Type baseType, Assembly assembly = null,
            bool withInstantiable = true, bool withoutBaseType = true)
        {
            var types = EnumerableTypes(baseType, t => baseType.IsAssignableFrom(t),
                assembly, withoutBaseType);

            // 仅包含可实例化类（排除抽象类）
            if (withInstantiable)
                types = types.Where(t => !t.GetTypeInfo().IsAbstract);

            return types.ToArray();
        }


        /// <summary>
        /// 枚举指定程序集中派生于基础类型的所有类型集合。
        /// </summary>
        /// <remarks>
        /// 此方法不能用于确定接口是否派生自另一个接口，或是否类实现的接口。仅支持确定类的继承关系。
        /// </remarks>
        /// <typeparam name="TBase">指定的基础类型。</typeparam>
        /// <param name="assembly">给定的程序集（可选；默认查找基础类型所在的程序集）。</param>
        /// <param name="withoutBaseType">排除基础类型自身（可选）。</param>
        /// <returns>返回类型数组。</returns>
        public static Type[] EnumerableTypesBySubclassOf<TBase>(Assembly assembly = null,
            bool withoutBaseType = true)
        {
            return typeof(TBase).EnumerableTypesBySubclassOf(assembly, withoutBaseType);
        }
        /// <summary>
        /// 枚举指定程序集中继承于基类型的所有类型集合。
        /// </summary>
        /// <remarks>
        /// 此方法不能用于确定接口是否派生自另一个接口，或是否类实现的接口。仅支持确定类的继承关系。
        /// </remarks>
        /// <param name="baseType">给定的基础类型。</param>
        /// <param name="assembly">给定的程序集（可选；默认查找基础类型所在的程序集）。</param>
        /// <param name="withoutBaseType">排除基础类型自身（可选）。</param>
        /// <returns>返回类型数组。</returns>
        public static Type[] EnumerableTypesBySubclassOf(this Type baseType, Assembly assembly = null,
            bool withoutBaseType = true)
        {
            var types = EnumerableTypes(baseType, t => t.GetTypeInfo().IsSubclassOf(baseType),
                assembly, withoutBaseType);

            return types.ToArray();
        }

        private static IEnumerable<Type> EnumerableTypes(Type baseType, Func<Type, bool> predicate,
            Assembly assembly = null, bool withoutBaseType = true)
        {
            baseType.NotNull(nameof(baseType));

            try
            {
                // 默认查找基础类型所在的程序集
                if (assembly == null)
                    assembly = baseType.GetTypeInfo().Assembly;

                // 筛选定义的公共类型
                var types = assembly.ExportedTypes.Where(predicate);

                // 移除自身基类
                if (withoutBaseType)
                    types = types.Where(t => t.FullName != baseType.FullName);

                return types;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region Instantiation

        /// <summary>
        /// 实例化类型，并初始化对象公共属性的默认值。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <returns>返回类型实例。</returns>
        public static T Instantiate<T>()
        {
            return (T)Initialize(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// 实例化类型，初始化对象公共属性的默认值。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <returns>返回对象。</returns>
        public static object Instantiate(this Type type)
        {
            return Initialize(Activator.CreateInstance(type));
        }

        /// <summary>
        /// 初始化对象公共属性的默认值。
        /// </summary>
        /// <param name="obj">给定的对象。</param>
        /// <returns>返回对象。</returns>
        public static object Initialize(this object obj)
        {
            var properties = obj.NotNull(nameof(obj)).GetType().GetProperties();
            if (properties == null || properties.Length < 1)
                return obj;

            foreach (var p in properties)
            {
                var value = GetDefaultValue(p);

                if (value == null)
                    value = p.PropertyType.AsDefault();

                p.SetValue(obj, value, null);
            }

            return obj;
        }

        /// <summary>
        /// 获取默认值属性特性。
        /// </summary>
        /// <param name="property">给定的属性信息。</param>
        /// <returns>返回默认值对象。</returns>
        private static object GetDefaultValue(PropertyInfo property)
        {
            var attrib = (DefaultValueAttribute)property.GetCustomAttribute(typeof(DefaultValueAttribute), false);

            return attrib?.Value;
        }


        /// <summary>
        /// 转换对象的默认值。
        /// </summary>
        /// <param name="type">给定的转换类型。</param>
        /// <returns>返回对象。</returns>
        public static object AsDefault(this Type type)
        {
            switch (type.FullName)
            {
                case "System.Boolean":
                    return false;

                case "System.Decimal":
                    return decimal.One;

                case "System.Double":
                    return double.NaN;

                case "System.DateTime":
                    return DateTime.Now;

                case "System.Guid":
                    return Guid.Empty;

                case "System.String":
                    return string.Empty;

                case "System.TimeSpan":
                    return TimeSpan.Zero;

                // Int
                case "System.Byte":
                    return byte.MinValue; // byte

                case "System.Int16":
                    return byte.MinValue; // short

                case "System.Int32":
                    return byte.MinValue; // int

                case "System.Int64":
                    return byte.MinValue; // long

                case "System.SByte":
                    return sbyte.MinValue; // sbyte

                case "System.UInt16":
                    return byte.MinValue; // ushort

                case "System.UInt32":
                    return byte.MinValue; // uint

                case "System.UInt64":
                    return byte.MinValue; // ulong

                default:
                    {
                        var typeInfo = type.GetTypeInfo();
                        
                        if (typeInfo.IsGenericType)
                        {
                            try
                            {
                                var gts = type.GenericTypeArguments;
                                // 链式转换
                                var parameters = gts.Select(t => AsDefault(t)).ToArray();

                                var ci = type.GetConstructor(type.GenericTypeArguments);
                                return ci.Invoke(parameters);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        if (typeInfo.IsClass && !typeInfo.IsAbstract)
                            return Activator.CreateInstance(type);

                        return null;
                    }
            }
        }

        #endregion

    }
}
