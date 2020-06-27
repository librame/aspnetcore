#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.IdentityModel.Tokens;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using AspNetCore.IdentityServer.Options;
    using Extensions.Data.Builders;

    /// <summary>
    /// 身份服务器构建器选项。
    /// </summary>
    public class IdentityServerBuilderOptions : AbstractDataBuilderOptions<IdentityServerStoreOptions, IdentityServerTableOptions>
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
        /// 用于签名令牌的签名证书。
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
