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

namespace Librame.AspNetCore.Applications
{
    using Extensions.Core.Decorators;

    /// <summary>
    /// <see cref="IApplicationBuilder"/> 装饰器接口。
    /// </summary>
    public interface IApplicationBuilderDecorator : IDecorator<IApplicationBuilder>
    {
    }
}
