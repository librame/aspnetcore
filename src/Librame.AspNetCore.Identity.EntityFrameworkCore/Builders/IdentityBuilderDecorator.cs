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
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Librame.AspNetCore.Identity.Builders
{
    using Extensions.Core.Builders;
    using Extensions.Core.Services;
    using Extensions.Data.Builders;

    /// <summary>
    /// <see cref="IdentityBuilder"/> 装饰器。
    /// </summary>
    public class IdentityBuilderDecorator : AbstractExtensionBuilderDecorator<IdentityBuilder>, IIdentityBuilderDecorator
    {
        /// <summary>
        /// 构造一个 <see cref="IdentityBuilderDecorator"/>。
        /// </summary>
        /// <param name="sourceBuilder">给定的 <see cref="IdentityBuilder"/>。</param>
        /// <param name="parentBuilder">给定的 <see cref="IExtensionBuilder"/>。</param>
        /// <param name="dependency">给定的 <see cref="IdentityBuilderDependency"/>。</param>
        public IdentityBuilderDecorator(IdentityBuilder sourceBuilder, IExtensionBuilder parentBuilder,
            IdentityBuilderDependency dependency)
            : base(sourceBuilder, parentBuilder, dependency)
        {
            Services.AddSingleton<IIdentityBuilderDecorator>(this);
        }


        /// <summary>
        /// 获取指定服务类型的特征。
        /// </summary>
        /// <param name="serviceType">给定的服务类型。</param>
        /// <returns>返回 <see cref="ServiceCharacteristics"/>。</returns>
        public override ServiceCharacteristics GetServiceCharacteristics(Type serviceType)
            => ServiceCharacteristics.Singleton();


        /// <summary>
        /// 数据构建器。
        /// </summary>
        /// <value>返回 <see cref="IDataBuilder"/>。</value>
        public IDataBuilder DataBuilder { get; private set; }

    }
}
