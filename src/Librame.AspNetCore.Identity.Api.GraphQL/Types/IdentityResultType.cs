#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;
using Microsoft.AspNetCore.Identity;

namespace Librame.AspNetCore.Identity.Api.Types
{
    using AspNetCore.Api.Types;
    using AspNetCore.Identity.Api.Models;

    /// <summary>
    /// 身份错误类型。
    /// </summary>
    public class IdentityResultType : ApiTypeBase<IdentityResultModel>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityResultType"/>。
        /// </summary>
        public IdentityResultType()
            : base()
        {
            Field(f => f.Succeeded);
            Field<ListGraphType<IdentityErrorType>>(nameof(IdentityResult.Errors));
        }

    }
}
