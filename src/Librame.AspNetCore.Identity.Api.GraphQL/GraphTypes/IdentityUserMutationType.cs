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
    /// 身份用户变化类型。
    /// </summary>
    public class IdentityUserMutationType : ObjectGraphType<DefaultIdentityUser>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityUserMutationType"/> 实例。
        /// </summary>
        public IdentityUserMutationType()
        {
            Field(f => f.UserName);
            Field(f => f.NormalizedUserName, true);
            Field(f => f.Email, true);
            Field(f => f.NormalizedEmail, true);
            Field(f => f.PhoneNumber, true);
            Field(f => f.PhoneNumberConfirmed);
            Field(f => f.LockoutEnabled);
        }
    }
}
