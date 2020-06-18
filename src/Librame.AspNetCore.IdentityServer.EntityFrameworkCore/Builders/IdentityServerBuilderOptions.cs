#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using AspNetCore.IdentityServer.Options;
    using Extensions.Core.Builders;

    /// <summary>
    /// 身份服务器构建器选项。
    /// </summary>
    public class IdentityServerBuilderOptions : IExtensionBuilderOptions
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
