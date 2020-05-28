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
    /// 登入视图资源。
    /// </summary>
    public class LoginViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 忘记密码。
        /// </summary>
        public string ForgotPassword { get; set; }

        /// <summary>
        /// 注册用户。
        /// </summary>
        public string RegisterUser { get; set; }

        /// <summary>
        /// 外部登入描述。
        /// </summary>
        public string ExternalLoginDescr { get; set; }

        /// <summary>
        /// 外部登入信息。
        /// </summary>
        public string ExternalLoginInfo { get; set; }

        /// <summary>
        /// 外部登入标题。
        /// </summary>
        public string ExternalLoginTitle { get; set; }
    }
}
