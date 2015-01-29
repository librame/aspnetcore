// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Utility;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Librame.Data.Context
{
    /// <summary>
    /// 排序器表达式抽象基类。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class SorterExpressionBase
    {
        /// <summary>
        /// 构造一个排序器表达式抽象基类实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// sorter 为空。
        /// </exception>
        /// <param name="sorter">给定的排序器。</param>
        public SorterExpressionBase(SorterQueryBase sorter)
        {
            if (ReferenceEquals(sorter, null))
            {
                throw new ArgumentNullException("sorter");
            }

            Sorter = sorter;
        }

        /// <summary>
        /// 获取当前排序器。
        /// </summary>
        public SorterQueryBase Sorter { get; private set; }


        #region Abstract Expression

        protected abstract Expression<Func<T, sbyte>> SByteExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, byte>> ByteExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, char>> CharExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, short>> Int16Expression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, int>> Int32Expression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, long>> Int64Expression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, ushort>> UInt16Expression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, uint>> UInt32Expression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, ulong>> UInt64Expression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, float>> SingleExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, double>> DoubleExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, decimal>> DecimalExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, bool>> BooleanExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, DateTime>> DateTimeExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, Guid>> GuidExpression<T>(string propertyName) where T : class;
        protected abstract Expression<Func<T, string>> StringExpression<T>(string propertyName) where T : class;

        #endregion


        /// <summary>
        /// 建立排序规则。
        /// </summary>
        /// <typeparam name="T">给定的实体类型。</typeparam>
        /// <param name="info">给定的排序信息。</param>
        /// <returns>返回排序查询对象或空。</returns>
        protected virtual IOrderedQueryable<T> Build<T>(IQueryable<T> query, SortingInfo info) where T : class
        {
            if (!String.IsNullOrEmpty(info.Field))
            {
                var property = ReflectionUtils.GetProperty<T>(info.Field);
                if (!ReferenceEquals(property, null))
                {
                    switch (property.PropertyType.Name)
                    {
                        case "SByte":
                            return BuildOrderBy(query, SByteExpression<T>(property.Name), info.Direction);

                        case "Byte":
                            return BuildOrderBy(query, ByteExpression<T>(property.Name), info.Direction);

                        case "Char":
                            return BuildOrderBy(query, CharExpression<T>(property.Name), info.Direction);

                        case "Int16":
                            return BuildOrderBy(query, Int16Expression<T>(property.Name), info.Direction);

                        case "Int32":
                            return BuildOrderBy(query, Int32Expression<T>(property.Name), info.Direction);

                        case "Int64":
                            return BuildOrderBy(query, Int64Expression<T>(property.Name), info.Direction);

                        case "UInt16":
                            return BuildOrderBy(query, UInt16Expression<T>(property.Name), info.Direction);

                        case "UInt32":
                            return BuildOrderBy(query, UInt32Expression<T>(property.Name), info.Direction);

                        case "UInt64":
                            return BuildOrderBy(query, UInt64Expression<T>(property.Name), info.Direction);

                        case "Single":
                            return BuildOrderBy(query, SingleExpression<T>(property.Name), info.Direction);

                        case "Double":
                            return BuildOrderBy(query, DoubleExpression<T>(property.Name), info.Direction);

                        case "Decimal":
                            return BuildOrderBy(query, DecimalExpression<T>(property.Name), info.Direction);

                        case "Boolean":
                            return BuildOrderBy(query, BooleanExpression<T>(property.Name), info.Direction);

                        case "DateTime":
                            return BuildOrderBy(query, DateTimeExpression<T>(property.Name), info.Direction);

                        case "Guid":
                            return BuildOrderBy(query, GuidExpression<T>(property.Name), info.Direction);

                        case "String":
                            return BuildOrderBy(query, StringExpression<T>(property.Name), info.Direction);

                        default:
                            goto case "String";
                    }
                }
            }

            return null;
        }
        /// <summary>
        /// 建立排序规则。
        /// </summary>
        /// <typeparam name="T">指定的实体类型。</typeparam>
        /// <typeparam name="TKey">指定的键类型。</typeparam>
        /// <param name="query">给定的查询对象。</param>
        /// <param name="keySelector">给定的查询键选择器。</param>
        /// <param name="direction">给定的排序方向。</param>
        /// <returns>返回查询对象。</returns>
        protected virtual IOrderedQueryable<T> BuildOrderBy<T, TKey>(IQueryable<T> query, Expression<Func<T, TKey>> keySelector, SortDirection direction)
        {
            if (direction == SortDirection.Asc)
            {
                return query.OrderBy(keySelector);
            }
            else
            {
                return query.OrderByDescending(keySelector);
            }
        }


        /// <summary>
        /// 建立（当前排序器）的排序规则。
        /// </summary>
        /// <typeparam name="T">给定的实体类型。</typeparam>
        /// <param name="query">给定的查询对象。</param>
        /// <returns>返回查询对象或空。</returns>
        public virtual IOrderedQueryable<T> Build<T>(IQueryable<T> query) where T : class
        {
            if (!ReferenceEquals(Sorter.Infos, null))
            {
                IOrderedQueryable<T> orderedQuery = null;

                foreach (var info in Sorter.Infos)
                {
                    if (!ReferenceEquals(info, null))
                    {
                        if (ReferenceEquals(orderedQuery, null))
                        {
                            orderedQuery = Build(query, info);
                        }
                        else
                        {
                            orderedQuery = Build(orderedQuery, info);
                        }
                    }
                }

                return orderedQuery;
            }

            return null;
        }

    }
}