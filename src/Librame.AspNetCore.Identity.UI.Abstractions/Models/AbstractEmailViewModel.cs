#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Identity.UI.Models
{
    /// <summary>
    /// 抽象邮件视图模型。
    /// </summary>
    public abstract class AbstractEmailViewModel
    {
        /// <summary>
        /// 邮件。
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = nameof(Email), ResourceType = typeof(ViewModelsResource))]
        public string Email { get; set; }
    }
}
