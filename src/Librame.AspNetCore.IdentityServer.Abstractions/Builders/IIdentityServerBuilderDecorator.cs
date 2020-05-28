#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.IdentityServer.Builders
{
    using Extensions.Core.Builders;

    /// <summary>
    /// <see cref="IIdentityServerBuilder"/> 装饰器接口。
    /// </summary>
    public interface IIdentityServerBuilderDecorator : IExtensionBuilderDecorator<IIdentityServerBuilder>
    {
        /// <summary>
        /// 用户类型。
        /// </summary>
        Type UserType { get; }
    }
}
