#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.UI
{
    using AspNetCore.UI;

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
        /// 取消按钮。
        /// </summary>
        public string CancelButton { get; set; }

        /// <summary>
        /// 忘记密码。
        /// </summary>
        public string ForgotPassword { get; set; }

        /// <summary>
        /// 注册用户。
        /// </summary>
        public string RegisterUser { get; set; }

        /// <summary>
        /// 本地登录。
        /// </summary>
        public string LocalLogin { get; set; }

        /// <summary>
        /// 外部登入。
        /// </summary>
        public string ExternalLogin { get; set; }

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

        /// <summary>
        /// 未配置登录方案。
        /// </summary>
        public string NoLoginSchemesConfigured { get; set; }
    }
}
