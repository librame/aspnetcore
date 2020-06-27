#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Options
{
    /// <summary>
    /// 身份存储初始化选项。
    /// </summary>
    public class IdentityStoreInitializationOptions
    {
        /// <summary>
        /// 默认用户电邮列表集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<string> DefaultUserEmails { get; set; }
            = new List<string>
            {
                "librame@librame.net",
                "libramecore@librame.net"
            };

        /// <summary>
        /// 默认角色名称列表集合。
        /// </summary>
        [SuppressMessage("Usage", "CA2227:集合属性应为只读")]
        public List<string> DefaultRoleNames { get; set; }
            = new List<string>
            {
                "SuperAdministrator",
                "Administrator"
            };

        /// <summary>
        /// 默认密码。
        /// </summary>
        public string DefaultPassword { get; set; }
            = "Password!123456";
    }
}
