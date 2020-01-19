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
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Librame.AspNetCore.Web.Tests
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 核心资源管理器。
    /// </summary>
    public class TestCoreResourceManager : ResourceManager
    {
        private const string InternalResourceBaseName = "InternalResource";

        private readonly ConcurrentDictionary<string, ResourceSet> _resourceSets
            = new ConcurrentDictionary<string, ResourceSet>();


        /// <summary>
        /// 构造一个 <see cref="TestCoreResourceManager"/>。
        /// </summary>
        /// <param name="resourceSource">给定的资源来源类型。</param>
        /// <param name="logger">给定的 <see cref="ILogger{AspNetResourceManager}"/>。</param>
        public TestCoreResourceManager(Type resourceSource, ILogger<TestCoreResourceManager> logger)
            : base(resourceSource)
        {
            Logger = logger.NotNull(nameof(logger));
        }

        /// <summary>
        /// 构造一个 <see cref="CoreResourceManager"/>。
        /// </summary>
        /// <param name="baseName">给定的基础名称。</param>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="logger">给定的 <see cref="ILogger{TestCoreResourceManager}"/>。</param>
        public TestCoreResourceManager(string baseName, Assembly assembly, ILogger<TestCoreResourceManager> logger)
            : base(baseName, assembly)
        {
            Logger = logger.NotNull(nameof(logger));
        }

        /// <summary>
        /// 构造一个 <see cref="CoreResourceManager"/>。
        /// </summary>
        /// <param name="baseName">给定的基础名称。</param>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="usingResourceSet">给定的使用资源集类型。</param>
        /// <param name="logger">给定的 <see cref="ILogger{AspNetResourceManager}"/>。</param>
        public TestCoreResourceManager(string baseName, Assembly assembly, Type usingResourceSet, ILogger<TestCoreResourceManager> logger)
            : base(baseName, assembly, usingResourceSet)
        {
            Logger = logger.NotNull(nameof(logger));
        }


        /// <summary>
        /// 日志。
        /// </summary>
        /// <value>
        /// 返回 <see cref="ILogger{TestCoreResourceManager}"/>。
        /// </value>
        protected ILogger<TestCoreResourceManager> Logger { get; }


        /// <summary>
        /// 内置获取资源集。
        /// </summary>
        /// <param name="culture">给定的 <see cref="CultureInfo"/>。</param>
        /// <param name="createIfNotExists">如果不存在则新建资源文件（未实现）。</param>
        /// <param name="tryParents">尝试查找父级资源（未实现）。</param>
        /// <returns>返回 <see cref="ResourceSet"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "culture")]
        protected override ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
        {
            // CultureInfo.InvariantCulture.Name is empty
            if (culture.Name.IsEmpty() && MainAssembly.TryGetCustomAttribute(out NeutralResourcesLanguageAttribute attribute))
                culture = new CultureInfo(attribute.CultureName);
            else
                attribute = new NeutralResourcesLanguageAttribute(string.Empty);

            var resourceSetKey = $"{nameof(BaseName)}={BaseName},{nameof(CultureInfo)}={culture.Name}";

            if (!_resourceSets.TryGetValue(resourceSetKey, out ResourceSet resourceSet))
            {
                try
                {
                    // 提供对内置资源的支持
                    if (IsInternalResourceBaseName() && culture.Name.Equals(attribute.CultureName, StringComparison.OrdinalIgnoreCase))
                        resourceSet = GetAssemblyResourceSet(MainAssembly);
                    else
                        resourceSet = GetAssemblyResourceSet(MainAssembly.GetSatelliteAssembly(culture));
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.AsInnerMessage());
                }

                if (resourceSet.IsNotNull())
                    _resourceSets.GetOrAdd(resourceSetKey, resourceSet);
            }

            return resourceSet;
        }

        /// <summary>
        /// 获取程序集资源集。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <returns>返回 <see cref="ResourceSet"/>。</returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "assembly")]
        protected virtual ResourceSet GetAssemblyResourceSet(Assembly assembly)
        {
            var resourceNames = assembly.GetManifestResourceNames();

            var resourceName = resourceNames.SingleOrDefault(name => name.StartsWith(BaseName, StringComparison.OrdinalIgnoreCase));
            if (resourceName.IsEmpty())
                return null;

            var resourceStream = assembly.GetManifestResourceStream(resourceName);
            return new ResourceSet(resourceStream);
        }

        /// <summary>
        /// 是内置资源基础名称。
        /// </summary>
        /// <returns>返回布尔值。</returns>
        protected virtual bool IsInternalResourceBaseName()
            => InternalResourceBaseName.Equals(BaseName.Split('.').Last(), StringComparison.OrdinalIgnoreCase);
    }
}
