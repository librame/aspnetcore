#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;

namespace Librame.AspNetCore.Identity.Api.Types
{
    using AspNetCore.Api.Types;

    /// <summary>
    /// 登入结果类型。
    /// </summary>
    public class SignInResultType : ApiTypeBase<SignInResult>
    {
        /// <summary>
        /// 构造一个 <see cref="SignInResultType"/>。
        /// </summary>
        public SignInResultType()
        {
            Field(f => f.Succeeded);
            Field(f => f.IsLockedOut);
            Field(f => f.IsNotAllowed);
            Field(f => f.RequiresTwoFactor);
        }

    }
}
