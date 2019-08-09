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

namespace Librame.AspNetCore.UI
{
    using Extensions.Core;

    /// <summary>
    /// 内部用户界面构建器。
    /// </summary>
    internal class InternalUserInterfaceBuilder : AbstractExtensionBuilder, IUserInterfaceBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalUserInterfaceBuilder"/> 实例。
        /// </summary>
        /// <param name="applicationPostConfigureOptionsType">给定的应用后置配置选项类型。</param>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        public InternalUserInterfaceBuilder(Type applicationPostConfigureOptionsType,
            IExtensionBuilder builder)
            : base(builder)
        {
            ApplicationPostConfigureOptionsType = applicationPostConfigureOptionsType;

            Services.AddSingleton<IUserInterfaceBuilder>(this);
        }


        /// <summary>
        /// 应用后置配置选项类型。
        /// </summary>
        public Type ApplicationPostConfigureOptionsType { get; private set; }


        /// <summary>
        /// 添加应用后置配置选项类型。
        /// </summary>
        /// <typeparam name="TAppPostConfigureOptions">指定的应用后置配置选项类型。</typeparam>
        /// <returns>返回 <see cref="IUserInterfaceBuilder"/>。</returns>
        public IUserInterfaceBuilder AddApplicationPostConfigureOptions<TAppPostConfigureOptions>()
            where TAppPostConfigureOptions : class, IApplicationPostConfigureOptions
        {
            ApplicationPostConfigureOptionsType = typeof(TAppPostConfigureOptions);
            Services.TryReplaceConfigureOptions<TAppPostConfigureOptions>();
            return this;
        }

    }
}
