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

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 更改密码视图模型。
    /// </summary>
    public class ChangePasswordViewModel
    {
        /// <summary>
        /// 当前密码。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(OldPassword), ResourceType = typeof(UserViewModelResource))]
        public string OldPassword { get; set; }

        /// <summary>
        /// 新密码。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(RegisterViewModel.Password), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(NewPassword), ResourceType = typeof(UserViewModelResource))]
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(RegisterViewModel.ConfirmPassword), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(UserViewModelResource))]
        public string ConfirmPassword { get; set; }
    }
}
