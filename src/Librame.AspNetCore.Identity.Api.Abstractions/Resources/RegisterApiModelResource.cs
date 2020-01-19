#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 注册 API 模型资源。
    /// </summary>
    public class RegisterApiModelResource : IResource
    {
        /// <summary>
        /// 确认您的电邮。
        /// </summary>
        public string ConfirmYourEmail { get; set; }

        /// <summary>
        /// 确认您的电邮格式。
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
