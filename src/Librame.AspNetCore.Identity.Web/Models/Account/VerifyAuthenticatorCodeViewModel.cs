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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Models
{
    using Resources;

    /// <summary>
    /// 验证认证码视图模型。
    /// </summary>
    public class VerifyAuthenticatorCodeViewModel
    {
        /// <summary>
        /// 认证码。
        /// </summary>
        [Required(ErrorMessageResourceName = nameof(RequiredAttribute), ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = nameof(Code), ResourceType = typeof(VerifyAuthenticatorCodeViewResource))]
        public string Code { get; set; }

        /// <summary>
        /// 返回 URL。
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 记住浏览器。
        /// </summary>
        [Display(Name = nameof(RememberBrowser), ResourceType = typeof(VerifyAuthenticatorCodeViewResource))]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        [Display(Name = nameof(RememberMe), ResourceType = typeof(UserViewModelResource))]
        public bool RememberMe { get; set; }
    }
}
