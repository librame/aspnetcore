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
    /// 验证码视图模型。
    /// </summary>
    public class VerifyCodeViewModel : VerifyAuthenticatorCodeViewModel
    {
        /// <summary>
        /// 提供程序。
        /// </summary>
        [Required]
        public string Provider { get; set; }
    }
}
