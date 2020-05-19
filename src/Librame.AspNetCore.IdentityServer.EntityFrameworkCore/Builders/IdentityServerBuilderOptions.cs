#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using AspNetCore.IdentityServer.Options;
    using Extensions.Data.Builders;
    using Extensions.Data.Stores;

    /// <summary>
    /// 身份服务器构建器选项。
    /// </summary>
    public class IdentityServerBuilderOptions : DataBuilderOptionsBase
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityServerBuilderOptions"/>。
        /// </summary>
        public IdentityServerBuilderOptions()
            : base(new DataTenant<Guid>())
        {
        }


        /// <summary>
        /// 帐户。
        /// </summary>
        public AccountOptions Accounts { get; set; }
            = new AccountOptions();

        /// <summary>
        /// 批准。
        /// </summary>
        public ConsentOptions Consents { get; set; }
            = new ConsentOptions();

        /// <summary>
        /// 授权。
        /// </summary>
        public AuthorizationOptions Authorizations { get; set; }
            = new AuthorizationOptions();
    }
}
