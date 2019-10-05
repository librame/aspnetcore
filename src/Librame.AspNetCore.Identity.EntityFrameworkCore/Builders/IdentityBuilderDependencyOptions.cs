#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;

    /// <summary>
    /// 身份构建器依赖选项。
    /// </summary>
    public class IdentityBuilderDependencyOptions : ExtensionBuilderDependencyOptions<IdentityBuilderDependencyOptions, IdentityBuilderOptions>
    {
        /// <summary>
        /// 身份选项配置器。
        /// </summary>
        public OptionsActionConfigurator<IdentityOptions> Identity { get; set; }
            = new OptionsActionConfigurator<IdentityOptions>(autoConfigureAction: false);
    }
}
