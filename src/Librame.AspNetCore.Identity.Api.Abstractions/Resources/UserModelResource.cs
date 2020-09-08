#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Resources
{
    using Extensions.Core.Resources;

    /// <summary>
    /// 用户模型资源。
    /// </summary>
    public class UserModelResource : IResource
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
