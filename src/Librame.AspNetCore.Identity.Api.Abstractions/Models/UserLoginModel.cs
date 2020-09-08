#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Models
{
    using AspNetCore.Api.Models;

    /// <summary>
    /// 用户登入模型。
    /// </summary>
    public class UserLoginModel : AbstractCreationIdentifierModel
    {
        /// <summary>
        /// 登入提供程序。
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// 提供程序显示名称。
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// 提供程序键。
        /// </summary>
        public string ProviderKey { get; set; }


        /// <summary>
        /// 用户模型。
        /// </summary>
        public UserModel User { get; set; }
    }
}
