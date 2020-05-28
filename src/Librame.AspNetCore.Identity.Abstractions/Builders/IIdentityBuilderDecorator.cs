#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Identity;

namespace Librame.AspNetCore.Identity.Builders
{
    using Extensions.Core.Builders;

    /// <summary>
    /// <see cref="IdentityBuilder"/> 装饰器接口。
    /// </summary>
    public interface IIdentityBuilderDecorator : IExtensionBuilderDecorator<IdentityBuilder>
    {
    }
}
