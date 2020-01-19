#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using Extensions.Data.Builders;

    /// <summary>
    /// 身份服务器构建器选项。
    /// </summary>
    public class IdentityServerBuilderOptions : DataBuilderOptionsBase
    {
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
