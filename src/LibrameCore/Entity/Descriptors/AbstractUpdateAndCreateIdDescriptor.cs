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
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace LibrameCore.Entity.Descriptors
{
    /// <summary>
    /// 抽象更新、创建、主键描述符接口。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    [StructLayout(LayoutKind.Sequential)]
    public abstract class AbstractUpdateAndCreateIdDescriptor<TId> : AbstractCreateIdDescriptor<TId>, IUpdateAndCreateIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 构建一个 <see cref="AbstractUpdateAndCreateIdDescriptor{TId}"/> 实例。
        /// </summary>
        public AbstractUpdateAndCreateIdDescriptor()
            : base()
        {
            UpdateTime = CreateTime;
            UpdatorId = CreatorId;
        }


        /// <summary>
        /// 更新时间。
        /// </summary>
        [DisplayName("更新时间")]
        public virtual DateTime UpdateTime { get; set; }

        /// <summary>
        /// 更新者主键。
        /// </summary>
        [DisplayName("更新者")]
        public virtual TId UpdatorId { get; set; }
    }
}
