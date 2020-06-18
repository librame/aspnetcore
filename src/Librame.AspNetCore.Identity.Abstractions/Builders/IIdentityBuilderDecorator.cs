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
    using Extensions.Data.Builders;

    /// <summary>
    /// <see cref="IdentityBuilder"/> 装饰器接口。
    /// </summary>
    public interface IIdentityBuilderDecorator : IExtensionBuilder, IExtensionBuilderDecorator<IdentityBuilder>
    {
        /// <summary>
        /// 数据构建器。
        /// </summary>
        /// <value>返回 <see cref="IDataBuilder"/>。</value>
        IDataBuilder DataBuilder { get; }
    }
}
