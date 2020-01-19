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
    /// 启用验证器视图模型。
    /// </summary>
    public class EnableAuthenticatorViewModel
    {
        /// <summary>
        /// 双因子验证码。
        /// </summary>
        [StringLength(7, MinimumLength = 6, ErrorMessageResourceName = nameof(TwoFactorCode), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(TwoFactorCode), ResourceType = typeof(UserViewModelResource))]
        public string TwoFactorCode { get; set; }
    }
}
