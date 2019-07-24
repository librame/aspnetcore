#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api
{
    using Extensions.Core;

    /// <summary>
    /// 注册 API 模型资源。
    /// </summary>
    public class RegisterApiModelResource : IResource
    {
        /// <summary>
        /// 确认您的邮箱。
        /// </summary>
        public string ConfirmYourEmail { get; set; }
        /// <summary>
        /// 确认您的邮箱格式。
        /// </summary>
        public string ConfirmYourEmailFormat { get; set; }

        /// <summary>
        /// 确认您的手机。
        /// </summary>
        public string ConfirmYourPhone { get; set; }
        /// <summary>
        /// 确认您的手机格式。
        /// </summary>
        public string ConfirmYourPhoneFormat { get; set; }
    }
}
