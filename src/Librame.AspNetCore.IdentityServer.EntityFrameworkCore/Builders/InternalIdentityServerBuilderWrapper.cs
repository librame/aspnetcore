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
using System;

namespace Librame.AspNetCore.IdentityServer
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 内部 <see cref="IIdentityServerBuilder"/> 封装器。
    /// </summary>
    internal class InternalIdentityServerBuilderWrapper : AbstractExtensionBuilderWrapper<IIdentityServerBuilder>, IIdentityServerBuilderWrapper
    {
        /// <summary>
        /// 构造一个 <see cref="InternalIdentityServerBuilderWrapper"/> 实例。
        /// </summary>
        /// <param name="userType">给定的用户类型。</param>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="rawBuilder">给定的原始 <see cref="IIdentityServerBuilder"/>。</param>
        public InternalIdentityServerBuilderWrapper(Type userType, IExtensionBuilder builder, IIdentityServerBuilder rawBuilder)
            : base(builder, rawBuilder)
        {
            UserType = userType.NotNull(nameof(userType));

            Services.AddSingleton<IIdentityServerBuilderWrapper>(this);
        }


        /// <summary>
        /// 用户类型。
        /// </summary>
        public Type UserType { get; }
    }
}
