// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Librame.Data.Context
{
    /// <summary>
    /// 过滤信息。
    /// </summary>
    /// <author>Librame Pang</author>
    public class FiltrationInfo
    {
        /// <summary>
        /// 获取或设置字段名。
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 获取或设置运算值。
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 获取或设置运算符。
        /// </summary>
        public FilterOperation Operator { get; set; }
        /// <summary>
        /// 获取或设置忽略大小写。
        /// </summary>
        public bool IgnoreCase { get; set; }

        /// <summary>
        /// 获取真实的运算值。
        /// </summary>
        /// <param name="valueType">给定的值类型。</param>
        /// <returns>返回运算值。</returns>
        public object GetRealValue(Type valueType)
        {
            return Value.Parse(valueType);
        }


        /// <summary>
        /// 解析运算符。
        /// </summary>
        /// <param name="theOperator">给定的运算符字符串。</param>
        /// <returns>返回过滤运算方式。</returns>
        public static FilterOperation ParseOperator(string theOperator)
        {
            switch (theOperator.ToLower())
            {
                //equal ==
                case "eq":
                case "==":
                case "isequalto":
                case "equals":
                case "equalto":
                case "equal":
                    return FilterOperation.Equals;

                //not equal !=
                case "neq":
                case "!=":
                case "isnotequalto":
                case "notequals":
                case "notequalto":
                case "notequal":
                case "ne":
                    return FilterOperation.NotEquals;

                // Greater
                case "gt":
                case ">":
                case "isgreaterthan":
                case "greaterthan":
                case "greater":
                    return FilterOperation.GreaterThan;

                // Greater or equal
                case "gte":
                case ">=":
                case "isgreaterthanorequalto":
                case "greaterthanequal":
                case "ge":
                    return FilterOperation.GreaterThanOrEquals;

                // Less
                case "lt":
                case "<":
                case "islessthan":
                case "lessthan":
                case "less":
                    return FilterOperation.LessThan;

                // Less or equal
                case "lte":
                case "<=":
                case "islessthanorequalto":
                case "lessthanequal":
                case "le":
                    return FilterOperation.LessThanOrEquals;

                case "startswith":
                    return FilterOperation.StartsWith;

                case "endswith":
                    return FilterOperation.EndsWith;

                //string.Contains()
                case "contains":
                    return FilterOperation.Contains;

                case "doesnotcontain":
                case "notcontains":
                    return FilterOperation.NotContains;

                default:
                    goto case "contains";
            }
        }
    }
}