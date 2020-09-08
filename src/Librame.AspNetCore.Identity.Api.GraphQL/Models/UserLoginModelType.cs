#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.Types
{
    using AspNetCore.Api.Models;
    using AspNetCore.Identity.Api.Models;

    /// <summary>
    /// 用户登入模型类型。
    /// </summary>
    public class UserLoginModelType : CreationIdentifierModelTypeBase<UserLoginModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UserLoginModelType"/>。
        /// </summary>
        public UserLoginModelType()
        {
            Field(f => f.LoginProvider);
            Field(f => f.ProviderDisplayName);
            Field(f => f.ProviderKey);
        }

    }
}
