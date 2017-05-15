#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Librame.Entity.Descriptors
{
    /// <summary>
    /// 抽象主键描述符。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    [StructLayout(LayoutKind.Sequential)]
    public abstract class AbstractIdDescriptor<TId> : IIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 构建一个 <see cref="AbstractIdDescriptor{TId}"/> 实例。
        /// </summary>
        public AbstractIdDescriptor()
        {
            Id = default(TId);
        }


        /// <summary>
        /// 主键。
        /// </summary>
        [DisplayName("编号")]
        public virtual TId Id { get; set; }
    }
}
