// Copyright (c) Librame.NET All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Librame.Data.Models
{
    /// <summary>
    /// 带泛类型编号的抽象实体。
    /// </summary>
    /// <typeparam name="TId">指定的编号类型。</typeparam>
    /// <author>Librame Pang</author>
    public abstract class IdEntity<TId>
    {
        /// <summary>
        /// 编号。
        /// </summary>
        public TId Id { get; set; }
    }
}