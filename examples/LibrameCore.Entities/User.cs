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
using LibrameCore.Authentication.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrameCore.Entities
{
    /// <summary>
    /// 用户。
    /// </summary>
    [Description("用户")]
    public class User : AbstractCreateDataIdDescriptor<int>, IUserModel
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
    }
}
