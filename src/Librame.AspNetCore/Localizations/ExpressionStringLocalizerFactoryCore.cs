#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Librame.AspNetCore
{
    using Extensions.Core;

    /// <summary>
    /// 表达式字符串定位器工厂核心。
    /// </summary>
    public class ExpressionStringLocalizerFactoryCore : ExpressionStringLocalizerFactory
    {
        /// <summary>
        /// 构造一个 <see cref="ExpressionStringLocalizerFactoryCore"/>。
        /// </summary>
        /// <param name="localizationOptions">给定的 <see cref="IOptions{LocalizationOptions}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public ExpressionStringLocalizerFactoryCore(IOptions<LocalizationOptions> localizationOptions,
            ILoggerFactory loggerFactory)
            : base(localizationOptions, loggerFactory)
        {
        }


        /// <summary>
        /// 资源名称集合缓存。
        /// </summary>
        protected IResourceNamesCache ResourceNamesCache { get; private set; }
            = new ResourceNamesCache();


        /// <summary>
        /// 创建资源管理器字符串定位器。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="baseName">给定的基础名称。</param>
        /// <returns>返回 <see cref="ResourceManagerStringLocalizer"/>。</returns>
        protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName)
        {
            var resourceManager = new ResourceManagerCore(baseName, assembly, LoggerFactory.CreateLogger<ResourceManagerCore>());
            
            return new ResourceManagerStringLocalizer(resourceManager, assembly, baseName, ResourceNamesCache, Logger);
        }

    }
}