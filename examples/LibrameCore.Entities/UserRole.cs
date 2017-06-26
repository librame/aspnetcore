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
using System.ComponentModel.DataAnnotations;

namespace LibrameCore.Entities
{
    /// <summary>
    /// 用户角色。
    /// </summary>
    public class UserRole : AbstractCreateDataIdDescriptor<int>
    {
        /// <summary>
        /// 用户编号。
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// 角色编号。
        /// </summary>
        [Required]
        public int RoleId { get; set; }
    }
}
