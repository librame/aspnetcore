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
    /// 注册输入类型。
    /// </summary>
    public class RegisterInputType : ApiInputTypeBase<RegisterModel>
    {
        /// <summary>
        /// 构造一个 <see cref="RegisterInputType"/>。
        /// </summary>
        public RegisterInputType()
            : base()
        {
            Field(f => f.Username);
            Field(f => f.Password);
            Field(f => f.ConfirmEmailUrl, nullable: true);
        }

    }
}
