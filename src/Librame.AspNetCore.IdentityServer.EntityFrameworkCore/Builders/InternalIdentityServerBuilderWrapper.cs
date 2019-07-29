#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions.Core;

    /// <summary>
    /// 内部 <see cref="IIdentityServerBuilder"/> 封装器。
    /// </summary>
    internal class InternalIdentityServerBuilderWrapper : AbstractExtensionBuilderWrapper<IIdentityServerBuilder>, IIdentityServerBuilderWrapper
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityServerBuilderWrapper"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawBuilder">给定的原始 <see cref="IIdentityServerBuilder"/>。</param>
        public InternalIdentityServerBuilderWrapper(IExtensionBuilder builder, IIdentityServerBuilder rawBuilder)
            : base(builder, rawBuilder)
        {
            Services.AddSingleton<IIdentityServerBuilderWrapper>(this);
        }

    }
}
