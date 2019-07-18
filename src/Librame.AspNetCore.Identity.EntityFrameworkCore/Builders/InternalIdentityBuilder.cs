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
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 内部身份构建器。
    /// </summary>
    internal class InternalIdentityBuilder : AbstractExtensionBuilder, IIdentityBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityBuilder"/> 实例。
        /// </summary>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        public InternalIdentityBuilder(IExtensionBuilder builder)
            : base(builder)
        {
            Services.AddSingleton<IIdentityBuilder>(this);
        }


        /// <summary>
        /// 身份构建器核心。
        /// </summary>
        /// <value>返回 <see cref="IdentityBuilder"/>。</value>
        public IdentityBuilder IdentityCore { get; private set; }


        /// <summary>
        /// 添加身份核心。
        /// </summary>
        /// <param name="identityCore">给定的 <see cref="IdentityBuilder"/>。</param>
        /// <returns>返回 <see cref="IIdentityBuilder"/>。</returns>
        public IIdentityBuilder AddIdentityCore(IdentityBuilder identityCore)
        {
            IdentityCore = identityCore.NotNull(nameof(identityCore));
            return this;
        }

    }
}
