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

namespace Librame.AspNetCore.Identity.Web.Models
{
    using Resources;

    /// <summary>
    /// 注册视图模型。
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// 电邮。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(Email), ResourceType = typeof(UserViewModelResource))]
        [EmailAddress(ErrorMessageResourceName = nameof(EmailAddressAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        public string Email { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(Password), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password), ResourceType = typeof(UserViewModelResource))]
        public string Password { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        [Compare(nameof(Password), ErrorMessageResourceName = nameof(ConfirmPassword), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(UserViewModelResource))]
        public string ConfirmPassword { get; set; }
    }
}
