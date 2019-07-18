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
    internal class InternalUIBuilder : AbstractExtensionBuilder, IUIBuilder
    {
        /// <summary>
        /// 构造一个 <see cref="InternalUIBuilder"/> 实例。
        /// </summary>
        /// <param name="applicationContextType">给定的应用程序上下文类型。</param>
        /// <param name="applicationPostConfigureOptionsType">给定的应用程序后置配置选项类型。</param>
        /// <param name="themepack">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <param name="builder">给定的 <see cref="IExtensionBuilder"/>。</param>
        public InternalUIBuilder(Type applicationContextType, Type applicationPostConfigureOptionsType,
            IThemepackInfo themepack, IExtensionBuilder builder)
            : base(builder)
        {
            ApplicationContextType = applicationContextType;
            ApplicationPostConfigureOptionsType = applicationPostConfigureOptionsType;
            Themepack = themepack;

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
        /// 主题包。
        /// </summary>
        public IThemepackInfo Themepack { get; private set; }


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


        /// <summary>
        /// 添加主题包。
        /// </summary>
        /// <param name="themepackInfo">给定的 <see cref="IThemepackInfo"/>。</param>
        /// <returns>返回 <see cref="IUIBuilder"/>。</returns>
        public IUIBuilder AddThemepack(IThemepackInfo themepackInfo)
        {
            Themepack = themepackInfo;
            Services.TryReplace(themepackInfo);
            return this;
        }

    }
}
