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
    /// 用户输入模型类型。
    /// </summary>
    public class UserInputModelType : InputModelTypeBase<UserModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UserInputModelType"/>。
        /// </summary>
        public UserInputModelType()
            : base()
        {
            Field(f => f.UserName);
            Field(f => f.Password);
            Field(f => f.EmailConfirmationUrl, nullable: true);
            Field(f => f.RememberMe, nullable: true);
        }

    }
}
