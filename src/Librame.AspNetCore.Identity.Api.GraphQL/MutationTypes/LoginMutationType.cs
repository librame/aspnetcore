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
    /// 登入变化类型。
    /// </summary>
    public class LoginMutationType : InputObjectGraphType<LoginModel>
    {
        /// <summary>
        /// 构造一个 <see cref="LoginMutationType"/> 实例。
        /// </summary>
        public LoginMutationType()
        {
            Field(f => f.Name);
            Field(f => f.Password);
            Field(f => f.RememberMe);
        }
    }
}
