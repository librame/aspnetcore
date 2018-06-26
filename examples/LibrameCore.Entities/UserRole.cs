#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using LibrameCore.Extensions.Authentication.Descriptors;
using LibrameStandard.Extensions.Entity.Descriptors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrameCore.Entities
{
    /// <summary>
    /// 用户角色。
    /// </summary>
    [DisplayName("用户角色")]
    public class UserRole : AbstractCIdDataDescriptor<int>, IUserRoleDescriptor<int, int, int>
    {
        /// <summary>
        /// 角色。
        /// </summary>
        [DisplayName("角色")]
        [Required]
        public int RoleId { get; set; } = default;

        /// <summary>
        /// 用户。
        /// </summary>
        [DisplayName("用户")]
        [Required]
        public int UserId { get; set; } = default;
    }
}
