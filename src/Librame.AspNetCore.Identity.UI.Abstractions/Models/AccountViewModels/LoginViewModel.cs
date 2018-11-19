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
    /// 登录视图模型。
    /// </summary>
    public class LoginViewModel : AbstractEmailViewModel
    {
        /// <summary>
        /// 密码。
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = nameof(Password), ResourceType = typeof(ViewModelsResource))]
        public string Password { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        [Display(Name = nameof(RememberMe), ResourceType = typeof(ViewModelsResource))]
        public bool RememberMe { get; set; }
    }
}
