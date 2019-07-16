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
    /// 内部存储构建器。
    /// </summary>
    internal class InternalUIBuilder : AbstractBuilder<UIBuilderOptions>, IUIBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalUIBuilder"/> 实例。
        /// </summary>
        /// <param name="applicationContextType">给定的应用程序上下文类型。</param>
        /// <param name="applicationPostConfigureOptionsType">给定的应用程序后置配置选项类型。</param>
        /// <param name="builder">给定的 <see cref="IBuilder"/>。</param>
        /// <param name="options">给定的 <see cref="UIBuilderOptions"/>。</param>
        public InternalUIBuilder(Type applicationContextType, Type applicationPostConfigureOptionsType,
            IBuilder builder, UIBuilderOptions options)
            : base(builder, options)
        {
            ApplicationContextType = applicationContextType;
            ApplicationPostConfigureOptionsType = applicationPostConfigureOptionsType;

            Services.AddSingleton<IUIBuilder>(this);
        }


        /// <summary>
        /// 应用程序上下文类型。
        /// </summary>
        public Type ApplicationContextType { get; private set; }

        /// <summary>
        /// 应用程序后置配置选项类型。
        /// </summary>
        public Type ApplicationPostConfigureOptionsType { get; private set; }


        /// <summary>
        /// 添加应用程序上下文类型。
        /// </summary>
        /// <typeparam name="TAppContext">指定的应用程序上下文类型。</typeparam>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public IUIBuilder AddApplicationContext<TAppContext>()
            where TAppContext : class, IApplicationContext
        {
            ApplicationContextType = typeof(TAppContext);
            Services.TryReplace<IApplicationContext, TAppContext>();

            return this;
        }

        /// <summary>
        /// 添加应用程序后置配置选项类型。
        /// </summary>
        /// <typeparam name="TAppPostConfigureOptions">指定的应用程序后置配置选项类型。</typeparam>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public IUIBuilder AddApplicationPostConfigureOptions<TAppPostConfigureOptions>()
            where TAppPostConfigureOptions : class, IApplicationPostConfigureOptions
        {
            ApplicationPostConfigureOptionsType = typeof(TAppPostConfigureOptions);
            Services.TryReplaceConfigureOptions<TAppPostConfigureOptions>();

            return this;
        }
    }
}
