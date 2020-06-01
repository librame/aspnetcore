#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Identity.Web.Models
{
    using Resources;

    /// <summary>
    /// 用户视图模型。
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// 称呼。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(Name), ResourceType = typeof(UserViewModelResource))]
        public string Name { get; set; }

        /// <summary>
        /// 电邮。
        /// </summary>
        [EmailAddress(ErrorMessageResourceName = nameof(EmailAddressAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(Email), ResourceType = typeof(UserViewModelResource))]
        public string Email { get; set; }
    }
}
