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
    using AspNetCore.Api.Types;

    /// <summary>
    /// 登入输入类型。
    /// </summary>
    public class LoginInputType : ApiInputTypeBase<LoginModel>
    {
        /// <summary>
        /// 构造一个 <see cref="LoginInputType"/>。
        /// </summary>
        public LoginInputType()
            : base()
        {
            Field(f => f.Username);
            Field(f => f.Password);
            Field(f => f.RememberMe, nullable: true);
        }

    }
}
