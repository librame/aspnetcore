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

namespace Librame.AspNetCore.Identity.Builders
{
    using Extensions.Core.Builders;
    using Extensions.Core.Dependencies;

    /// <summary>
    /// 身份构建器依赖选项。
    /// </summary>
    public class IdentityBuilderDependency : AbstractExtensionBuilderDependency<IdentityBuilderOptions>
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityBuilderDependency"/>。
        /// </summary>
        public IdentityBuilderDependency()
            : base(nameof(IdentityBuilderDependency))
        {
        }


        /// <summary>
        /// 身份选项依赖。
        /// </summary>
        public OptionsDependency<IdentityOptions> Identity { get; set; }
            = new OptionsDependency<IdentityOptions>(autoConfigureOptions: false);
    }
}
