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
    /// <see cref="IIdentityServerBuilder"/> 封装器接口。
    /// </summary>
    public interface IIdentityServerBuilderWrapper : IExtensionBuilderWrapper<IIdentityServerBuilder>
    {
    }
}
