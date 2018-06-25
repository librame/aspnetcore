#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameStandard.Entity.Descriptors;

namespace LibrameCore.Extensions.Authentication.Descriptors
{
    /// <summary>
    /// 角色描述符。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    public class RoleDescriptor<TId> : AbstractIdDescriptor<TId>, IRoleDescriptor<TId>
    {
        /// <summary>
        /// 默认名称。
        /// </summary>
        internal const string DEFAULT_NAME = "Administrator";


        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; } = DEFAULT_NAME;
    }
}
