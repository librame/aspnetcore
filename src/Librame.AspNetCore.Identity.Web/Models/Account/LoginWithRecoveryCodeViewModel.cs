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
    /// 恢复码登入视图模型。
    /// </summary>
    public class LoginWithRecoveryCodeViewModel
    {
        /// <summary>
        /// 恢复码。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(RecoveryCode), ResourceType = typeof(UserViewModelResource))]
        public string RecoveryCode { get; set; }
    }
}
