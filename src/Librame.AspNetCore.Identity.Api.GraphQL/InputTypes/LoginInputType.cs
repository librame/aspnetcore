#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api
{
    using AspNetCore.Api;

    /// <summary>
    /// 登入输入类型。
    /// </summary>
    public class LoginInputType : ApiModelInputTypeBase<LoginModel>
    {
        /// <summary>
        /// 构造一个 <see cref="LoginInputType"/> 实例。
        /// </summary>
        public LoginInputType()
        {
            Field(f => f.Name);
            Field(f => f.Password);
            Field(f => f.RememberMe, true);
            Field(f => f.UserId, true);
            Field(f => f.Token, true);
        }
    }
}
