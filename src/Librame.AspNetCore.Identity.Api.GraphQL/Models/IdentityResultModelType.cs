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

namespace Librame.AspNetCore.Identity.Api.Types
{
    using AspNetCore.Api.Models;
    using AspNetCore.Identity.Api.Models;

    /// <summary>
    /// 身份结果模型类型。
    /// </summary>
    public class IdentityResultModelType : ModelTypeBase<IdentityResultModel>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityResultModelType"/>。
        /// </summary>
        public IdentityResultModelType()
            : base()
        {
            Field(f => f.Succeeded);
            Field(f => f.Errors, type: typeof(ListGraphType<IdentityErrorType>), nullable: true);
        }

    }
}
