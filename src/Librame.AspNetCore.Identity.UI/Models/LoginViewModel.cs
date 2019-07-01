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
    /// 登入视图模型。
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// 称呼。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(Name), ResourceType = typeof(UserViewModelResource))]
        public string Name { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        [StringLength(20, MinimumLength = 6, ErrorMessageResourceName = nameof(Password), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password), ResourceType = typeof(UserViewModelResource))]
        public string Password { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        [Display(Name = nameof(RememberMe), ResourceType = typeof(UserViewModelResource))]
        public bool RememberMe { get; set; }
    }
}
