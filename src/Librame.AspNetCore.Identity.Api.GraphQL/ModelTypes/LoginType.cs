#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.Identity.Api.ModelTypes
{
    using AspNetCore.Api;
    using Models;

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
