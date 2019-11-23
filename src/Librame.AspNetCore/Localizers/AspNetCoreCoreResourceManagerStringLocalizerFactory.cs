//#region License

/////* **************************************************************************************
// * Copyright (c) Librame Pang All rights reserved.
// * 
// * http://librame.net
// * 
// * You must not remove this notice, or any other, from this software.
// * **************************************************************************************/

//#endregion

//using Microsoft.Extensions.Localization;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using System.Reflection;

//namespace Librame.AspNetCore
//{
//    using Extensions.Core;

//    /// <summary>
//    /// <see cref="CoreResourceManagerStringLocalizerFactory"/> for ASP.NET Core。
//    /// </summary>
//    public class AspNetCoreCoreResourceManagerStringLocalizerFactory : CoreResourceManagerStringLocalizerFactory
//    {
//        /// <summary>
//        /// 构造一个 <see cref="AspNetCoreCoreResourceManagerStringLocalizerFactory"/>。
//        /// </summary>
//        /// <param name="localizationOptions">给定的 <see cref="IOptions{LocalizationOptions}"/>。</param>
//        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
//        /// <param name="builderOptions">给定的 <see cref="IOptions{CoreBuilderOptions}"/>。</param>
//        public AspNetCoreCoreResourceManagerStringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions,
//            ILoggerFactory loggerFactory, IOptions<CoreBuilderOptions> builderOptions)
//            : base(localizationOptions, loggerFactory, builderOptions)
//        {
//        }


//        /// <summary>
//        /// 资源名称集合缓存。
//        /// </summary>
//        protected IResourceNamesCache ResourceNamesCache { get; private set; }
//            = new ResourceNamesCache();


//        /// <summary>
//        /// 创建资源管理器字符串定位器。
//        /// </summary>
//        /// <param name="assembly">给定的程序集。</param>
//        /// <param name="baseName">给定的基础名称。</param>
//        /// <returns>返回 <see cref="ResourceManagerStringLocalizer"/>。</returns>
//        protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName)
//        {
//            var resourceManager = new AspNetCoreResourceManager(baseName, assembly, LoggerFactory.CreateLogger<AspNetCoreResourceManager>());
//            return new ResourceManagerStringLocalizer(resourceManager, assembly, baseName, ResourceNamesCache, Logger);
//        }

//    }
//}