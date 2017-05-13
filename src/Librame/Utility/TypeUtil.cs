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
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Librame.Utility
{
    /// <summary>
    /// <see cref="Type"/> 实用工具。
    /// </summary>
    public static class TypeUtil
    {
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
        /// 构建键名。
        /// </summary>
        /// <typeparam name="T">指定的类型。</typeparam>
        /// <returns>返回字符串。</returns>
        public static string BuildKey<T>()
        {
            return typeof(T).BuildKey();
        }
        /// <summary>
        /// 构建键名。
        /// </summary>
        /// <param name="type">给定的类型。</param>
        /// <returns>返回字符串。</returns>
        public static string BuildKey(this Type type)
        {
            return type.NotNull(nameof(type)).AssemblyQualifiedNameWithoutVcp();
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


        /// <summary>
        /// 枚举指定程序集中派生于基础类型的所有类型集合。
        /// </summary>
        /// <typeparam name="TBase">指定的基础类型。</typeparam>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="withInstantiable">仅支持可实例化类型（可选）。</param>
        /// <param name="withoutBaseType">排除基础类型自身（可选）。</param>
        /// <returns>返回类型数组。</returns>
        public static Type[] EnumerableAssignableTypes<TBase>(Assembly assembly = null,
            bool withInstantiable = true, bool withoutBaseType = true)
        {
            return typeof(TBase).EnumerableAssignableTypes(assembly, withInstantiable, withoutBaseType);
        }

        /// <summary>
        /// 枚举指定程序集中派生于基础类型的所有类型集合。
        /// </summary>
        /// <param name="baseType">给定的基础类型。</param>
        /// <param name="assembly">给定的程序集（可选；默认查找基础类型所在的程序集）。</param>
        /// <param name="withInstantiable">仅支持可实例化类型（可选）。</param>
        /// <param name="withoutBaseType">排除基础类型自身（可选）。</param>
        /// <returns>返回类型数组。</returns>
        public static Type[] EnumerableAssignableTypes(this Type baseType, Assembly assembly = null,
            bool withInstantiable = true, bool withoutBaseType = true)
        {
            baseType.NotNull(nameof(baseType));

            try
            {
                // 默认查找基础类型所在的程序集
                if (assembly == null)
                    assembly = baseType.GetTypeInfo().Assembly;

                // 获取定义的公共类型
                var allTypes = assembly.GetExportedTypes();
                if (allTypes == null || allTypes.Length < 1)
                    return allTypes;

                // 加载所有派生类型集合
                var types = allTypes.Where(t => baseType.IsAssignableFrom(t));

                // 仅包含可实例化类（排除抽象类）
                if (withInstantiable)
                    types = types.Where(t => !t.GetTypeInfo().IsAbstract);

                // 移除自身基类
                if (withoutBaseType)
                    types = types.Where(t => t.FullName != baseType.FullName);

                return types.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


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
                    value = InitializePropertyValue(p.PropertyType);

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
        /// 初始化属性值。
        /// </summary>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回属性值对象。</returns>
        private static object InitializePropertyValue(Type propertyType)
        {
            switch (propertyType.FullName)
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
                    return byte.MinValue; // sbyte

                case "System.UInt16":
                    return byte.MinValue; // ushort

                case "System.UInt32":
                    return byte.MinValue; // uint

                case "System.UInt64":
                    return byte.MinValue; // ulong

                default:
                    {
                        var typeInfo = propertyType.GetTypeInfo();
                        
                        if (typeInfo.IsSubclassOf(typeof(Nullable<>)))
                        {
                            try
                            {
                                var gts = propertyType.GenericTypeArguments;
                                var parameters = gts.Select(t => InitializePropertyValue(t)).ToArray();

                                var ci = propertyType.GetConstructor(propertyType.GenericTypeArguments);
                                return ci.Invoke(parameters);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        if (typeInfo.IsClass && !typeInfo.IsAbstract)
                            return Activator.CreateInstance(propertyType);

                        return null;
                    }
            }
        }

        #endregion

    }
}
