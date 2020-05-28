#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.ModelTypes
{
    using AspNetCore.Api;
    using AspNetCore.Identity.Api.Models;

    /// <summary>
    /// 登入类型。
    /// </summary>
    public class LoginType : ApiModelGraphTypeBase<LoginApiModel>
    {
        /// <summary>
        /// 构造一个 <see cref="LoginType"/> 实例。
        /// </summary>
        public LoginType()
            : base()
        {
            this.AddLoginApiModelFields();
        }

    }
}
