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
    /// 验证手机号视图模型。
    /// </summary>
    public class VerifyPhoneNumberViewModel : AddPhoneNumberViewModel
    {
        /// <summary>
        /// 验证码。
        /// </summary>
        [Required]
        public string Code { get; set; }
    }
}
