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

namespace Librame.AspNetCore.Identity.UI.Models.ManageViewModels
{
    /// <summary>
    /// 更改密码视图模型。
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// 当前密码。
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(OldPassword), ResourceType = typeof(ViewModelsResource))]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码。
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(AccountViewModels.ResetPasswordViewModel.Password), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword), ResourceType = typeof(ViewModelsResource))]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认新密码。
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmNewPassword), ResourceType = typeof(ViewModelsResource))]
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(AccountViewModels.ResetPasswordViewModel.ConfirmPassword), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string ConfirmNewPassword { get; set; }
    }
}
