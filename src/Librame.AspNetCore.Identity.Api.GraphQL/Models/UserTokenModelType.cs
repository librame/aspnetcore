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
    /// 用户令牌模型类型。
    /// </summary>
    public class UserTokenModelType : CreationIdentifierModelTypeBase<UserTokenModel>
    {
        /// <summary>
        /// 构造一个 <see cref="UserTokenModelType"/>。
        /// </summary>
        public UserTokenModelType()
        {
            Field(f => f.LoginProvider);
            Field(f => f.Name);
            Field(f => f.Value);
        }

    }
}
