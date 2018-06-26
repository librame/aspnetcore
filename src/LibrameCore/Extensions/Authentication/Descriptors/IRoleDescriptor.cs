#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Extensions.Entity.Descriptors;

namespace LibrameCore.Extensions.Authentication.Descriptors
{
    /// <summary>
    /// 角色描述符接口。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public interface IRoleDescriptor<TId> : IIdDescriptor<TId>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        string Name { get; }
    }
}
