#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Web.Models
{
    /// <summary>
    /// 移出登入视图模型。
    /// </summary>
    public class RemoveLoginViewModel
    {
        /// <summary>
        /// 登入提供程序。
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// 提供程序密钥。
        /// </summary>
        public string ProviderKey { get; set; }
    }
}
