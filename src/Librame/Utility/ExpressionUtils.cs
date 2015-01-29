// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq.Expressions;

namespace Librame.Utility
{
    /// <summary>
    /// 表达式工具。
    /// </summary>
    /// <author>Librame Pang</author>
    public static class ExpressionUtils
    {
        /// <summary>
        /// 建立单个属性键的 Lambda 表达式（例：p => p.PropertyName）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TKey">指定的键类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <returns>返回 lambda 表达式。</returns>
        public static Expression<Func<T, TKey>> Build<T, TKey>(string propertyName)
        {
            // 建立变量
            var p = Expression.Parameter(typeof(T), "p");

            // 建立属性
            var property = Expression.Property(p, propertyName);

            // p => p.PropertyName
            return Expression.Lambda<Func<T, TKey>>(property, p);
        }

        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName > compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildGreaterThan<T>(string propertyName, object value, Type propertyType)
        {
            return Build<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.GreaterThan(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName >= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildGreaterThanOrEqual<T>(string propertyName, object value, Type propertyType)
        {
            return Build<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.GreaterThanOrEqual(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈 compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildLessThan<T>(string propertyName, object value, Type propertyType)
        {
            return Build<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.LessThan(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName 〈= compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildLessThanOrEqual<T>(string propertyName, object value, Type propertyType)
        {
            return Build<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.LessThanOrEqual(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName != compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildNotEqual<T>(string propertyName, object value, Type propertyType)
        {
            return Build<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.NotEqual(p, c));
        }
        /// <summary>
        /// 建立比较的单个属性值等于的 Lambda 表达式（例：p => p.PropertyName == compareValue）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定用于对比的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> BuildEqual<T>(string propertyName, object value, Type propertyType)
        {
            return Build<T, BinaryExpression>(propertyName, value, propertyType,
                (p, c) => Expression.Equal(p, c));
        }
        /// <summary>
        /// 建立使用单个属性值进行比较的 Lambda 表达式（例：p => p.PropertyName.CompareTo(value)）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TExpression">指定的表达式类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="compareTo">给定的对比方法。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> Build<T, TExpression>(string propertyName, object value, Type propertyType,
            Func<MemberExpression, ConstantExpression, TExpression> compareTo)
            where TExpression : Expression
        {
            // 建立变量
            var p = Expression.Parameter(typeof(T), "p");

            // 建立属性
            var property = Expression.Property(p, propertyName);
            var constant = Expression.Constant(value, propertyType);

            // 调用方法（如：Expression.Equal(property, constant);）
            var body = compareTo(property, constant);

            // p => p.PropertyName == value
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        /// <summary>
        /// 建立使用单个属性值进行比较的 Lambda 表达式（例：p => p.PropertyName.CallMethodName(value)）。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <param name="propertyName">给定的属性名。</param>
        /// <param name="value">给定的参考值。</param>
        /// <param name="propertyType">给定的属性类型。</param>
        /// <param name="callMethodName">给定要调用的方法名。</param>
        /// <returns>返回 Lambda 表达式。</returns>
        public static Expression<Func<T, bool>> Build<T>(string propertyName, object value, Type propertyType, string callMethodName)
        {
            var type = typeof(T);

            // 建立变量
            var p = Expression.Parameter(type, "p");
            // 建立属性
            var property = Expression.Property(p, propertyName);
            var constant = Expression.Constant(value, propertyType);
            // 得到属性信息
            var propertyInfo = type.GetProperty(propertyName);

            // 调用方法
            var body = Expression.Call(property,
                propertyInfo.PropertyType.GetMethod(callMethodName, new Type[] { propertyType }),
                constant);

            // p => p.PropertyName.CallMethodName(value)
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

    }
}