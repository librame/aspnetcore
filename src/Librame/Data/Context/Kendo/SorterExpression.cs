// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Librame.Utility;
using System;
using System.Linq.Expressions;

namespace Librame.Data.Context.Kendo
{
    /// <summary>
    /// 排序器表达式。
    /// </summary>
    /// <author>Librame Pang</author>
    public class SorterExpression : SorterExpressionBase
    {
        /// <summary>
        /// 构造一个排序器表达式实例。
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// sorter 为空。
        /// </exception>
        /// <param name="sorter">给定的排序器。</param>
        public SorterExpression(SorterQueryBase sorter)
            : base(sorter)
        {
        }


        #region Expression

        protected override Expression<Func<T, sbyte>> SByteExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, sbyte>(propertyName);
        }

        protected override Expression<Func<T, byte>> ByteExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, byte>(propertyName);
        }

        protected override Expression<Func<T, char>> CharExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, char>(propertyName);
        }

        protected override Expression<Func<T, short>> Int16Expression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, short>(propertyName);
        }

        protected override Expression<Func<T, int>> Int32Expression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, int>(propertyName);
        }

        protected override Expression<Func<T, long>> Int64Expression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, long>(propertyName);
        }

        protected override Expression<Func<T, ushort>> UInt16Expression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, ushort>(propertyName);
        }

        protected override Expression<Func<T, uint>> UInt32Expression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, uint>(propertyName);
        }

        protected override Expression<Func<T, ulong>> UInt64Expression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, ulong>(propertyName);
        }

        protected override Expression<Func<T, float>> SingleExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, float>(propertyName);
        }

        protected override Expression<Func<T, double>> DoubleExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, double>(propertyName);
        }

        protected override Expression<Func<T, decimal>> DecimalExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, decimal>(propertyName);
        }

        protected override Expression<Func<T, bool>> BooleanExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, bool>(propertyName);
        }

        protected override Expression<Func<T, DateTime>> DateTimeExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, DateTime>(propertyName);
        }

        protected override Expression<Func<T, Guid>> GuidExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, Guid>(propertyName);
        }

        protected override Expression<Func<T, string>> StringExpression<T>(string propertyName)
        {
            return ExpressionUtils.Build<T, string>(propertyName);
        }

        #endregion

    }
}