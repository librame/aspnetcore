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
    /// 用户令牌模型。
    /// </summary>
    public class UserTokenModel : AbstractCreationIdentifierModel
    {
        /// <summary>
        /// 登入提供程序。
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// 名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 值。
        /// </summary>
        public string Value { get; set; }


        /// <summary>
        /// 用户模型。
        /// </summary>
        public UserModel User { get; set; }
    }
}
