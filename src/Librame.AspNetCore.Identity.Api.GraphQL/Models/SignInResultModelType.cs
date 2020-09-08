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
    /// 登入结果模型类型。
    /// </summary>
    public class SignInResultModelType : ModelTypeBase<SignInResultModel>
    {
        /// <summary>
        /// 构造一个 <see cref="SignInResultModelType"/>。
        /// </summary>
        public SignInResultModelType()
        {
            Field(f => f.Succeeded);
            Field(f => f.IsLockedOut);
            Field(f => f.IsNotAllowed);
            Field(f => f.RequiresTwoFactor);

            Field(f => f.User, type: typeof(UserModelType), nullable: true);
        }

    }
}
