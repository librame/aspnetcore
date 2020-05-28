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
