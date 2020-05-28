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
    /// 登入输入类型。
    /// </summary>
    public class LoginInputType : ApiModelInputGraphTypeBase<LoginApiModel>
    {
        /// <summary>
        /// 构造一个 <see cref="LoginInputType"/> 实例。
        /// </summary>
        public LoginInputType()
            : base()
        {
            Name = GetInputTypeName<LoginInputType>();

            this.AddLoginApiModelFields();
        }

    }
}
