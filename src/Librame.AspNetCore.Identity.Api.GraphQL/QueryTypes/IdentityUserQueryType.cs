#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using GraphQL.Types;

namespace Librame.AspNetCore.Identity.Api
{
    /// <summary>
    /// 身份用户查询类型。
    /// </summary>
    public class IdentityUserQueryType : ObjectGraphType<DefaultIdentityUser>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityUserQueryType"/> 实例。
        /// </summary>
        public IdentityUserQueryType()
        {
            Field(f => f.UserName, true);
            Field(f => f.NormalizedUserName, true);
        }
    }
}
