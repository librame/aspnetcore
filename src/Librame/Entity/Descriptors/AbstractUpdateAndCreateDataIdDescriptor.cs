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

namespace Librame.Entity.Descriptors
{
    /// <summary>
    /// 抽象更新、创建、数据、主键描述符。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public abstract class AbstractUpdateAndCreateDataIdDescriptor<TId> : AbstractCreateDataIdDescriptor<TId>, IUpdateAndCreateIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 构建一个 <see cref="AbstractUpdateAndCreateDataIdDescriptor{TId}"/> 实例。
        /// </summary>
        public AbstractUpdateAndCreateDataIdDescriptor()
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
