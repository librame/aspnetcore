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
using LibrameStandard.Authentication.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrameCore.Entities
{
    /// <summary>
    /// 用户。
    /// </summary>
    [Description("角色")]
    public class User : AbstractCreateDataIdDescriptor<int>, IUserModel
    {
        /// <summary>
        /// 唯一标识。
        /// </summary>
        [Required]
        [StringLength(50)]
        public string UniqueId { get; set; } = Guid.Empty.ToString();

        /// <summary>
        /// 邮箱。
        /// </summary>
        [DataType(DataType.EmailAddress)]
        [DisplayName("邮箱")]
        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        /// <summary>
        /// 电话。
        /// </summary>
        [DisplayName("手机")]
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        [StringLength(500)]
        public string Passwd { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        [DisplayName("名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
