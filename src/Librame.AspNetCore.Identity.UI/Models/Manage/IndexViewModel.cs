#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 首页视图模型。
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// 有密码。
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// 用户登入信息列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// 手机号。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 双因子。
        /// </summary>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// 记住浏览器。
        /// </summary>
        public bool BrowserRemembered { get; set; }

        /// <summary>
        /// 认证器密钥。
        /// </summary>
        public string AuthenticatorKey { get; set; }
    }

    ///// <summary>
    ///// 索引视图模型。
    ///// </summary>
    //public class IndexViewModel
    //{
    //    /// <summary>
    //    /// 称呼。
    //    /// </summary>
    //    [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
    //    [Display(Name = nameof(Name), ResourceType = typeof(UserViewModelResource))]
    //    public string Name { get; set; }

    //    /// <summary>
    //    /// 电邮。
    //    /// </summary>
    //    [EmailAddress(ErrorMessageResourceName = nameof(EmailAddressAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
    //    [Display(Name = nameof(Email), ResourceType = typeof(UserViewModelResource))]
    //    public string Email { get; set; }

    //    /// <summary>
    //    /// 电话。
    //    /// </summary>
    //    //[Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
    //    [Display(Name = nameof(Phone), ResourceType = typeof(UserViewModelResource))]
    //    public string Phone { get; set; }
    //}
}
