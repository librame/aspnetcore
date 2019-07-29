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
using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;

    /// <summary>
    /// 内部 <see cref="IdentityBuilder"/> 封装器。
    /// </summary>
    internal class InternalIdentityBuilderWrapper : AbstractExtensionBuilderWrapper<IdentityBuilder>, IIdentityBuilderWrapper
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityBuilderWrapper"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawBuilder">给定的原始 <see cref="IdentityBuilder"/>。</param>
        public InternalIdentityBuilderWrapper(IExtensionBuilder builder, IdentityBuilder rawBuilder)
            : base(builder, rawBuilder)
        {
            Services.AddSingleton<IIdentityBuilderWrapper>(this);
        }

    }
}
