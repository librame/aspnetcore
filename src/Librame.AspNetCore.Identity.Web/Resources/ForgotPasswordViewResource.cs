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
    /// 忘记密码视图资源。
    /// </summary>
    public class ForgotPasswordViewResource : AbstractViewResource
    {
        /// <summary>
        /// 按钮文本。
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// 重置密码。
        /// </summary>
        public string ResetPassword { get; set; }

        /// <summary>
        /// 重置密码格式化。
        /// </summary>
        public string ResetPasswordFormat { get; set; }
    }
}
