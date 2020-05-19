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
        /// <param name="parentDependency">给定的父级 <see cref="IExtensionBuilderDependency"/>（可选）。</param>
        public IdentityBuilderDependency(IExtensionBuilderDependency parentDependency = null)
            : base(nameof(IdentityBuilderDependency), parentDependency)
        {
        }


        /// <summary>
        /// 身份选项依赖。
        /// </summary>
        public OptionsDependency<IdentityOptions> Identity { get; set; }
            = new OptionsDependency<IdentityOptions>();
    }
}
