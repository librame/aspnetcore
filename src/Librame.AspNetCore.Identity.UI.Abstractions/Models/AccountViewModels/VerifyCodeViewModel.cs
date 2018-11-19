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
    /// 验证代码视图模型。
    /// </summary>
    public class VerifyCodeViewModel
    {
        /// <summary>
        /// 提供程序。
        /// </summary>
        [Required]
        public string Provider { get; set; }

        /// <summary>
        /// 代码。
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 回调 URL。
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 记住浏览器。
        /// </summary>
        [Display(Name = nameof(RememberBrowser), ResourceType = typeof(ViewModelsResource))]
        public bool RememberBrowser { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        [Display(Name = nameof(RememberMe), ResourceType = typeof(ViewModelsResource))]
        public bool RememberMe { get; set; }
    }
}
