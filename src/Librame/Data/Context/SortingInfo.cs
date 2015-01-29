// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Data.Context
{
    /// <summary>
    /// 排序信息。
    /// </summary>
    /// <author>Librame Pang</author>
    public class SortingInfo
    {
        /// <summary>
        /// 获取或设置字段名。
        /// </summary>
        public string Field { get; set; }
        /// <summary>
        /// 获取或设置方向。
        /// </summary>
        public SortDirection Direction { get; set; }


        /// <summary>
        /// 解析排序方向。
        /// </summary>
        /// <param name="theOperator">给定的排序方向字符串。</param>
        /// <returns>返回排序方向。</returns>
        public static SortDirection ParseDirection(string direction)
        {
            switch (direction.ToLower())
            {
                case "asc":
                    return SortDirection.Asc;

                case "desc":
                    return SortDirection.Desc;

                default:
                    goto case "asc";
            }
        }

    }
}