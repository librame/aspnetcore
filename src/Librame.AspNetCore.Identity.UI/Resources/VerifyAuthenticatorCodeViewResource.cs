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
    /// 验证认证码视图资源。
    /// </summary>
    public class VerifyAuthenticatorCodeViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 认证码。
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 记住浏览器。
        /// </summary>
        public string RememberBrowser { get; set; }

        /// <summary>
        /// 丢失认证器。
        /// </summary>
        public string LostAuthenticator { get; set; }
    }
}
