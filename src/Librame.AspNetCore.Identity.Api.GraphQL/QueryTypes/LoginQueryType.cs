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
    /// 登入查询类型。
    /// </summary>
    public class LoginQueryType : ApiModelQueryGraphTypeBase<LoginApiModel>
    {
        /// <summary>
        /// 构造一个 <see cref="LoginQueryType"/> 实例。
        /// </summary>
        public LoginQueryType()
            : base()
        {
            Name = GetQueryTypeName<LoginQueryType>();

            Field(f => f.Name);
            Field(f => f.Password);
            Field(f => f.RememberMe, nullable: true);
            Field(f => f.UserId, nullable: true);
            Field(f => f.Token, nullable: true);
        }
    }
}
