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
    /// <summary>
    /// 视图模型集合资源。
    /// </summary>
    public class ViewModelsResource : Resources.IResource
    {
        /// <summary>
        /// 邮箱。
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码。
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 新密码。
        /// </summary>
        public string NewPassword { get; set; }

        /// <summary>
        /// 确认新密码。
        /// </summary>
        public string ConfirmNewPassword { get; set; }

        /// <summary>
        /// 当前密码。
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// 电话号码。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 记住浏览器。
        /// </summary>
        public string RememberBrowser { get; set; }

        /// <summary>
        /// 记住我。
        /// </summary>
        public string RememberMe { get; set; }
    }
}
