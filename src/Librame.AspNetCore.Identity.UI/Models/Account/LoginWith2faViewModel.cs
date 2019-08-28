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
    /// 双因子验证登入视图模型。
    /// </summary>
    public class LoginWith2faViewModel
    {
        /// <summary>
        /// 双因子验证码。
        /// </summary>
        [StringLength(7, MinimumLength = 6, ErrorMessageResourceName = nameof(TwoFactorCode), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(TwoFactorCode), ResourceType = typeof(UserViewModelResource))]
        public string TwoFactorCode { get; set; }

        /// <summary>
        /// 记住本机。
        /// </summary>
        [Display(Name = nameof(RememberMachine), ResourceType = typeof(UserViewModelResource))]
        public bool RememberMachine { get; set; }
    }
}
