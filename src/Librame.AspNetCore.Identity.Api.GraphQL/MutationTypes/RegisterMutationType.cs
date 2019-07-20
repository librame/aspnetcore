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
    /// 注册变化类型。
    /// </summary>
    public class RegisterMutationType : InputObjectGraphType<RegisterModel>
    {
        /// <summary>
        /// 构造一个 <see cref="RegisterMutationType"/> 实例。
        /// </summary>
        public RegisterMutationType()
        {
            Field(f => f.Email);
            Field(f => f.Name);
            Field(f => f.Password);
        }
    }
}
