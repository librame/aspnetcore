#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;

namespace Librame.AspNetCore.Identity.Builders
{
    using AspNetCore.Identity.Options;
    using Extensions.Core.Builders;
    using Extensions.Data.Options;

    /// <summary>
    /// 身份构建器选项。
    /// </summary>
    public class IdentityBuilderOptions : AbstractDataOptions<IdentityStoreOptions, IdentityTableOptions>, IExtensionBuilderOptions
    {
        /// <summary>
        /// 启用密码规则提示（默认启用）。
        /// </summary>
        public bool PasswordRulePromptEnabled { get; set; }
            = true;

        /// <summary>
        /// 认证器 URI 工厂方法。
        /// </summary>
        public Func<IdentityAuthenticatorDescriptor, string> AuthenticatorUriFactory { get; set; }
            = descr => descr.BuildOTPAuthUriString();
    }
}
