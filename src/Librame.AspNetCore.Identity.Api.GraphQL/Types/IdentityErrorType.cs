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
    /// 身份错误类型。
    /// </summary>
    public class IdentityErrorType : ApiTypeBase<IdentityError>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityErrorType"/>。
        /// </summary>
        public IdentityErrorType()
            : base()
        {
            Field(f => f.Code);
            Field(f => f.Description);
        }

    }
}
