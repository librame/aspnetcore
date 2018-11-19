#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Librame.Resources;

namespace Librame.AspNetCore.Identity.UI
{
    /// <summary>
    /// 登录视图资源。
    /// </summary>
    public class LoginViewResource : IResource
    {
        /// <summary>
        /// 标题。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述。
        /// </summary>
        public string Descr { get; set; }

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
    }
}
