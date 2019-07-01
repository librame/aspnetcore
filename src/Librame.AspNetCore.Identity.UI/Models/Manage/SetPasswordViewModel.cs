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
    /// 设置密码视图模型。
    /// </summary>
    public class SetPasswordViewModel
    {
        /// <summary>
        /// 新密码。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(ResetPasswordViewModel.Password), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword), ResourceType = typeof(UserViewModelResource))]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认新密码。
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmNewPassword), ResourceType = typeof(UserViewModelResource))]
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(ResetPasswordViewModel.ConfirmPassword), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string ConfirmNewPassword { get; set; }
    }
}
