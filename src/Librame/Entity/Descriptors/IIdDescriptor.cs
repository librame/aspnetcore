#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.Entity.Descriptors
{
    /// <summary>
    /// 主键描述符接口。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public interface IIdDescriptor<TId> : IAutomapping
        where TId : struct
    {
        /// <summary>
        /// 主键。
        /// </summary>
        TId Id { get; }
    }
}
