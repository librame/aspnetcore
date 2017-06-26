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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibrameCore.Entities
{
    /// <summary>
    /// 角色。
    /// </summary>
    [Description("角色")]
    public class Role : AbstractCreateDataIdDescriptor<int>, IRoleModel
    {
        /// <summary>
        /// 名称。
        /// </summary>
        [DisplayName("名称")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 说明。
        /// </summary>
        [DisplayName("说明")]
        [DataType(DataType.MultilineText)]
        [StringLength(200)]
        public string Descr { get; set; }
    }
}
