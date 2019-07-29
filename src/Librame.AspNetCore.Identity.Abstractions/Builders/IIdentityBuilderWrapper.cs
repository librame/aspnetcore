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

namespace Librame.AspNetCore.Identity
{
    using Extensions.Core;

    /// <summary>
    /// <see cref="IdentityBuilder"/> 封装器接口。
    /// </summary>
    public interface IIdentityBuilderWrapper : IExtensionBuilderWrapper<IdentityBuilder>
    {
    }
}
