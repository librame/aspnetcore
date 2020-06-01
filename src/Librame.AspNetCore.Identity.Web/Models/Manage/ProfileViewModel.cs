#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Models
{
    /// <summary>
    /// 资料视图模型。
    /// </summary>
    public class ProfileViewModel
    {
        /// <summary>
        /// 有密码。
        /// </summary>
        public bool HasPassword { get; set; }

        /// <summary>
        /// 用户登入信息列表。
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public IList<UserLoginInfo> Logins { get; set; }

        /// <summary>
        /// 手机号。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 双因子。
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
