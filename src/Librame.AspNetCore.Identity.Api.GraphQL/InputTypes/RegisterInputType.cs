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
    /// 注册输入类型。
    /// </summary>
    public class RegisterInputType : ApiModelInputTypeBase<RegisterModel>
    {
        /// <summary>
        /// 构造一个 <see cref="RegisterInputType"/> 实例。
        /// </summary>
        public RegisterInputType()
            : base()
        {
            Field(f => f.Email);
            Field(f => f.Name);
            Field(f => f.Password);
            Field(f => f.UserId, true);
            Field(f => f.Token, true);
        }
    }
}
