#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// ASP.NET Core 资源管理器。
    /// </summary>
    public class CoreResourceManager : ResourceManager
    {
        /// <summary>
        /// 构造一个 <see cref="CoreResourceManager"/> 实例。
        /// </summary>
        /// <param name="resourceSource">给定的资源来源类型。</param>
        /// <param name="logger">给定的 <see cref="ILogger{AspNetResourceManager}"/>。</param>
        public CoreResourceManager(Type resourceSource, ILogger<CoreResourceManager> logger)
            : base(resourceSource)
        {
            Logger = logger.NotNull(nameof(logger));
        }

        /// <summary>
        /// 构造一个 <see cref="CoreResourceManager"/> 实例。
        /// </summary>
        /// <param name="baseName">给定的基础名称。</param>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="logger">给定的 <see cref="ILogger{AspNetResourceManager}"/>。</param>
        public CoreResourceManager(string baseName, Assembly assembly, ILogger<CoreResourceManager> logger)
            : base(baseName, assembly)
        {
            Logger = logger.NotNull(nameof(logger));
        }

        /// <summary>
        /// 构造一个 <see cref="CoreResourceManager"/> 实例。
        /// </summary>
        /// <param name="baseName">给定的基础名称。</param>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="usingResourceSet">给定的使用资源集类型。</param>
        /// <param name="logger">给定的 <see cref="ILogger{AspNetResourceManager}"/>。</param>
        public CoreResourceManager(string baseName, Assembly assembly, Type usingResourceSet, ILogger<CoreResourceManager> logger)
            : base(baseName, assembly, usingResourceSet)
        {
            Logger = logger.NotNull(nameof(logger));
        }


        /// <summary>
        /// 记录器。
        /// </summary>
        /// <value>
        /// 返回 <see cref="ILogger{CoreResourceManager}"/>。
        /// </value>
        protected ILogger<CoreResourceManager> Logger { get; }


        /// <summary>
        /// 内置获取资源集。
        /// </summary>
        /// <param name="culture">给定的 <see cref="CultureInfo"/>。</param>
        /// <param name="createIfNotExists">如果不存在则新建资源文件（未实现）。</param>
        /// <param name="tryParents">尝试查找父级资源（未实现）。</param>
        /// <returns>返回 <see cref="ResourceSet"/>。</returns>
        protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            if (culture.IsNull() || culture.Name.IsNullOrEmpty())
            {
                if (CultureInfo.CurrentCulture.IsNotNull())
                    culture = CultureInfo.CurrentCulture;
                else if (CultureInfo.CurrentUICulture.IsNotNull())
                    culture = CultureInfo.CurrentUICulture;
                else
                    culture.NotNull(nameof(culture));
            }

            // 优先支持视图资源映射模式
            var viewResourceType = MainAssembly.GetType(BaseName);
            if (viewResourceType.IsNotNull() && viewResourceType.TryGetCustomAttribute(out ViewResourceMappingAttribute attribute))
            {
                var viewBaseName = attribute.GetResourceBaseName(viewResourceType);
                var resourceManager = new CoreResourceManager(viewBaseName, MainAssembly, Logger);
                return resourceManager.GetResourceSet(culture, createIfNotExists, tryParents);
            }
            
            Assembly cultureAssembly;

            try
            {
                cultureAssembly = MainAssembly.GetSatelliteAssembly(culture);
            }
            catch (FileNotFoundException ex)
            {
                Logger.LogError(ex.AsInnerMessage());
                cultureAssembly = MainAssembly;
            }

            var resourceNames = cultureAssembly.GetManifestResourceNames();
            if (resourceNames.IsNullOrEmpty()) return null;
            
            var resourceName = resourceNames.SingleOrDefault(name => name.StartsWith(BaseName));
            if (resourceName.IsNullOrEmpty())
                return base.InternalGetResourceSet(culture, createIfNotExists, tryParents);

            var resourceStream = cultureAssembly.GetManifestResourceStream(resourceName);
            return new ResourceSet(resourceStream);
        }

    }
}
