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
    /// 用户角色描述符。
    /// </summary>
    /// <typeparam name="TId">指定的主键类型。</typeparam>
    /// <typeparam name="TRoleId">指定的角色编号类型。</typeparam>
    /// <typeparam name="TUserId">指定的用户编号类型。</typeparam>
    public class UserRoleDescriptor<TId, TUserId, TRoleId> : AbstractIdDescriptor<TId>, IUserRoleDescriptor<TId, TUserId, TRoleId>
    {
        /// <summary>
        /// 角色编号。
        /// </summary>
        public TRoleId RoleId { get; set; } = default;

        /// <summary>
        /// 用户编号。
        /// </summary>
        public TUserId UserId { get; set; } = default;
    }
}
