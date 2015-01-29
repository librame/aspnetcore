// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Data.Models
{
    /// <summary>
    /// 带数据、泛类型编号的抽象实体。
    /// </summary>
    /// <typeparam name="TId">指定的编号类型。</typeparam>
    /// <author>Librame Pang</author>
    public abstract class DataIdEntity<TId> : IdEntity<TId>
    {
        /// <summary>
        /// 数据范围。
        /// </summary>
        public DataScope DataScope { get; set; }
        /// <summary>
        /// 数据排序。
        /// </summary>
        public int DataSorting { get; set; }

        /// <summary>
        /// 构造一个带数据编号的抽象实体对象。
        /// </summary>
        public DataIdEntity()
        {
            DataScope = DataScope.Public;
            DataSorting = 1;
        }
    }
}