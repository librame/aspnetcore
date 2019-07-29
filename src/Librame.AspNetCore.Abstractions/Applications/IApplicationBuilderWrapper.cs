#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Builder;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    /// <summary>
    /// <see cref="IApplicationBuilder"/> 封装器接口。
    /// </summary>
    public interface IApplicationBuilderWrapper : IBuilderWrapper<IApplicationBuilder>
    {
    }
}
