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
    /// 增加手机号视图模型。
    /// </summary>
    public class AddPhoneNumberViewModel
    {
        /// <summary>
        /// 手机号。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Phone]
        [Display(Name = nameof(Phone), ResourceType = typeof(UserViewModelResource))]
        public string Phone { get; set; }
    }
}
