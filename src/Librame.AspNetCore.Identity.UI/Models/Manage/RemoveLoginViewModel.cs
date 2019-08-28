#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.ComponentModel.DataAnnotations;

namespace Librame.AspNetCore.Identity.UI
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
