// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Utility;
using System;
using System.Linq.Expressions;

namespace Librame.Data.Context.Kendo
{
    /// <summary>
    /// 过滤器表达式。
    /// </summary>
    /// <author>Librame Pang</author>
    public class FilterExpression : FilterExpressionBase
    {
        /// <summary>
        /// 构造一个过滤器表达式实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// filter 为空。
        /// </exception>
        /// <param name="filter">给定的过滤器。</param>
        public FilterExpression(FilterQueryBase filter)
            : base(filter)
        {
        }


        #region Expression

        protected override Expression<Func<T, bool>> StartsWithExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.Build<T>(propertyName, value, propertyType, "StartsWith");
        }

        protected override Expression<Func<T, bool>> EndsWithExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.Build<T>(propertyName, value, propertyType, "EndsWith");
        }

        protected override Expression<Func<T, bool>> ContainsExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.Build<T>(propertyName, value, propertyType, "Contains");
        }

        protected override Expression<Func<T, bool>> NotContainsExpression<T>(string propertyName, object value, Type propertyType)
        {
            // 暂不支持
            return null;
        }

        protected override Expression<Func<T, bool>> EqualsExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.BuildEqual<T>(propertyName, value, propertyType);
        }

        protected override Expression<Func<T, bool>> NotEqualsExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.BuildNotEqual<T>(propertyName, value, propertyType);
        }

        protected override Expression<Func<T, bool>> GreaterThanExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.BuildGreaterThan<T>(propertyName, value, propertyType);
        }

        protected override Expression<Func<T, bool>> GreaterThanOrEqualExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.BuildGreaterThanOrEqual<T>(propertyName, value, propertyType);
        }

        protected override Expression<Func<T, bool>> LessThanExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.BuildLessThan<T>(propertyName, value, propertyType);
        }

        protected override Expression<Func<T, bool>> LessThanOrEqualExpression<T>(string propertyName, object value, Type propertyType)
        {
            return ExpressionUtils.BuildLessThanOrEqual<T>(propertyName, value, propertyType);
        }

        #endregion

    }
}