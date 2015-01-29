// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Utility;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Librame.Data.Context
{
    /// <summary>
    /// 过滤器表达式抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class FilterExpressionBase
    {
        /// <summary>
        /// 构造一个过滤器表达式抽象基类实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// filter 为空。
        /// </exception>
        /// <param name="filter">给定的过滤器。</param>
        public FilterExpressionBase(FilterQueryBase filter)
        {
            if (ReferenceEquals(filter, null))
            {
                throw new ArgumentNullException("filter");
            }

            Filter = filter;
        }

        /// <summary>
        /// 获取当前过滤器。
        /// </summary>
        public FilterQueryBase Filter { get; private set; }


        #region Abstract Expression

        protected abstract Expression<Func<T, bool>> StartsWithExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> EndsWithExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> ContainsExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> NotContainsExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> EqualsExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> NotEqualsExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> GreaterThanExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> GreaterThanOrEqualExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> LessThanExpression<T>(string propertyName, object value, Type propertyType) where T : class;
        protected abstract Expression<Func<T, bool>> LessThanOrEqualExpression<T>(string propertyName, object value, Type propertyType) where T : class;

        #endregion


        /// <summary>
        /// 建立用于筛选的查询表达式。
        /// </summary>
        /// <typeparam name="T">给定的实体类型。</typeparam>
        /// <param name="info">给定的过滤信息。</param>
        /// <returns>返回查询表达式。</returns>
        protected virtual Expression<Func<T, bool>> BuildExpression<T>(FiltrationInfo info) where T : class
        {
            if (!String.IsNullOrEmpty(info.Field))
            {
                var property = ReflectionUtils.GetProperty<T>(info.Field);
                if (!ReferenceEquals(property, null))
                {
                    object value = info.GetRealValue(property.PropertyType);
                    if (!ReferenceEquals(value, null))
                    {
                        switch (info.Operator)
                        {
                            case FilterOperation.StartsWith:
                                return StartsWithExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.EndsWith:
                                return EndsWithExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.Contains:
                                return ContainsExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.NotContains:
                                return NotContainsExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.Equals:
                                return EqualsExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.NotEquals:
                                return NotEqualsExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.GreaterThanOrEquals:
                                return GreaterThanOrEqualExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.GreaterThan:
                                return GreaterThanExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.LessThanOrEquals:
                                return LessThanOrEqualExpression<T>(property.Name, value, property.PropertyType);

                            case FilterOperation.LessThan:
                                return LessThanExpression<T>(property.Name, value, property.PropertyType);

                            default:
                                return null;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 建立（当前过滤器）用于筛选的查询表达式。
        /// </summary>
        /// <typeparam name="T">给定的实体类型。</typeparam>
        /// <param name="query">给定的查询对象。</param>
        /// <returns>返回查询对象。</returns>
        public virtual IQueryable<T> BuildExpression<T>(IQueryable<T> query) where T : class
        {
            if (!ReferenceEquals(Filter.Infos, null))
            {
                foreach (var info in Filter.Infos)
                {
                    if (!ReferenceEquals(info, null))
                    {
                        var predicate = BuildExpression<T>(info);

                        if (!ReferenceEquals(predicate, null))
                        {
                            query = query.Where(predicate);
                        }
                    }
                }
            }

            return query;
        }

    }
}