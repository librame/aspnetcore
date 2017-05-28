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
    /// 更新、创建、主键描述符接口。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public interface IUpdateAndCreateIdDescriptor<TId> : ICreateIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 更新时间。
        /// </summary>
        DateTime UpdateTime { get; }

        /// <summary>
        /// 更新者主键。
        /// </summary>
        TId UpdatorId { get; }
    }
}
