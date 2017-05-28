#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace LibrameCore.Entity.Descriptors
{
    /// <summary>
    /// 父主键描述符接口。
    /// </summary>
    public interface IParentIdDescriptor<TId> : IIdDescriptor<TId>
        where TId : struct
    {
        /// <summary>
        /// 父主键。
        /// </summary>
        TId ParentId { get; }
    }
}
