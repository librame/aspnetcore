#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Resources
{
    using AspNetCore.Web.Resources;

    /// <summary>
    /// 发送验证码视图资源。
    /// </summary>
    public class SendCodeViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 选择双因子验证提供程序。
        /// </summary>
        public string SelectedProvider { get; set; }

        /// <summary>
        /// 您的安全码是。
        /// </summary>
        public string YourSecurityCodeIs { get; set; }

        /// <summary>
        /// 安全码。
        /// </summary>
        public string SecurityCode { get; set; }
    }
}
