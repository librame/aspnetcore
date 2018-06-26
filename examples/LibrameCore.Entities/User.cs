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
using LibrameCore.Extensions.Authentication.Descriptors;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace LibrameCore.Entities
{
    /// <summary>
    /// 用户。
    /// </summary>
    [DisplayName("用户")]
    public class User : AbstractCIdDataDescriptor<int>, IUserDescriptor<int>
    {
        /// <summary>
        /// 名称。
        /// </summary>
        [DisplayName("名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        [DataType(DataType.Password)]
        [DisplayName("密码")]
        [StringLength(500)]
        public string Passwd { get; set; }

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
        [DataType(DataType.PhoneNumber)]
        [DisplayName("电话")]
        [Required]
        [StringLength(50)]
        public string Phone { get; set; }

        /// <summary>
        /// 标识。
        /// </summary>
        [DisplayName("标识")]
        [Required]
        [StringLength(50)]
        public string UniqueId { get; set; } = Guid.Empty.ToString();
    }
}
