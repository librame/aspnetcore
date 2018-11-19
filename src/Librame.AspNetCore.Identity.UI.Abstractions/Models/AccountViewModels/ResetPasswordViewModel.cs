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

namespace Librame.AspNetCore.Identity.UI.Models.AccountViewModels
{
    /// <summary>
    /// 重置密码视图模型。
    /// </summary>
    public class ResetPasswordViewModel : AbstractEmailViewModel
    {
        /// <summary>
        /// 密码。
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(Password), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(ViewModelsResource))]
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ConfirmPassword), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 代码。
        /// </summary>
        public string Code { get; set; }
    }
}
