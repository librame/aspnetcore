#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using AspNetCore.Web.Resources;

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


        /// <summary>
        /// 密码规则提示。
        /// </summary>
        public string PasswordRulePrompts { get; set; }

        /// <summary>
        /// 密码的最小长度。
        /// </summary>
        public string PasswordRequiredLength { get; set; }

        /// <summary>
        /// 密码的最小唯一字符数默认为 1。
        /// </summary>
        public string PasswordRequiredUniqueChars { get; set; }

        /// <summary>
        /// 密码包含特殊字符。
        /// </summary>
        public string PasswordRequireNonAlphanumeric { get; set; }

        /// <summary>
        /// 密码包含小写英文字母。
        /// </summary>
        public string PasswordRequireLowercase { get; set; }

        /// <summary>
        /// 密码包含大写英文字母。
        /// </summary>
        public string PasswordRequireUppercase { get; set; }

        /// <summary>
        /// 密码包含数字。
        /// </summary>
        public string PasswordRequireDigit { get; set; }
    }
}
