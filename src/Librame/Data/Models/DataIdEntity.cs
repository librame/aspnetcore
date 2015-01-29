// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Data.Models
{
    /// <summary>
    /// 带数据、整数型编号的抽象实体。
    /// </summary>
    /// <author>Librame Pang</author>
    public abstract class DataIdEntity : DataIdEntity<int>
    {
        /// <summary>
        /// 构造一个带数据编号的抽象实体对象。
        /// </summary>
        public DataIdEntity()
            : base()
        {
        }
    }
}