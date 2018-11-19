#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Librame.AspNetCore.Identity.UI.Models.ManageViewModels
{
    /// <summary>
    /// 索引视图模型。
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// 包含密码。
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// 登录集合列表。
        /// </summary>
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// 电话号码。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 启用双因子。
        /// </summary>
        public bool TwoFactor { get; set; }

        /// <summary>
        /// 记住浏览器。
        /// </summary>
        public bool BrowserRemembered { get; set; }

        /// <summary>
        /// 认证器密钥。
        /// </summary>
        public string AuthenticatorKey { get; set; }
    }
}
