#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace LibrameCore.Entity.Descriptors
{
    /// <summary>
    /// 创建、主键描述符接口。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public interface ICreateIdDescriptor<TId> : IIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 创建时间。
        /// </summary>
        DateTime CreateTime { get; }

        /// <summary>
        /// 创建者主键。
        /// </summary>
        TId CreatorId { get; }
    }
}
