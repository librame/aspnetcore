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

    /// <summary>
    /// <see cref="IdentityBuilder"/> 装饰器接口。
    /// </summary>
    public interface IIdentityBuilderDecorator : IExtensionBuilderDecorator<IdentityBuilder>
    {
    }
}
