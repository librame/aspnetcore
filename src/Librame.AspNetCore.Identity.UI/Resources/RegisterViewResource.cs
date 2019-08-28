#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    /// <summary>
    /// 注册视图资源。
    /// </summary>
    public class RegisterViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 登入。
        /// </summary>
        public string Login { get; set; }

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
