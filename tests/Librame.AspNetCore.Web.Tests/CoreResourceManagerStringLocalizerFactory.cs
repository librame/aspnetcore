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
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Librame.AspNetCore.Web.Tests
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 核心资源管理器字符串定位器工厂。
    /// </summary>
    public class TestCoreResourceManagerStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IResourceNamesCache _resourceNamesCache = new ResourceNamesCache();
        private readonly ConcurrentDictionary<string, ResourceManagerStringLocalizer> _localizerCache =
            new ConcurrentDictionary<string, ResourceManagerStringLocalizer>();

        private readonly ILoggerFactory _loggerFactory;
        private readonly string _resourcesRelativePath;


        /// <summary>
        /// 构造一个 <see cref="TestCoreResourceManagerStringLocalizerFactory"/>。
        /// </summary>
        /// <param name="localizationOptions">给定的 <see cref="IOptions{LocalizationOptions}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", MessageId = "loggerFactory")]
        public TestCoreResourceManagerStringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions,
            ILoggerFactory loggerFactory)
        {
            localizationOptions.NotNull(nameof(localizationOptions));
            _loggerFactory = loggerFactory.NotNull(nameof(loggerFactory));

            _resourcesRelativePath = FormatLocationPath(localizationOptions.Value.ResourcesPath ?? string.Empty);
            Logger = loggerFactory.CreateLogger<TestCoreResourceManagerStringLocalizerFactory>();
        }


        /// <summary>
        /// 日志工厂。
        /// </summary>
        protected ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// 日志。
        /// </summary>
        protected ILogger Logger { get; }


        /// <summary>
        /// 创建字符串定位器。
        /// </summary>
        /// <param name="resourceSource">给定的资源类型。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/>。</returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            resourceSource.NotNull(nameof(resourceSource));

            var typeInfo = resourceSource.GetTypeInfo();
            var baseName = GetResourcePrefix(typeInfo, GetRootNamespace(typeInfo.Assembly), GetResourcePath(typeInfo.Assembly));

            return _localizerCache.GetOrAdd(baseName, _ => CreateResourceManagerStringLocalizer(typeInfo.Assembly, baseName));
        }

        /// <summary>
        /// 创建字符串定位器。
        /// </summary>
        /// <param name="baseName">给定的基础资源名称。</param>
        /// <param name="location">给定的程序集定位。</param>
        /// <returns>返回 <see cref="IStringLocalizer"/>。</returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            baseName.NotEmpty(nameof(baseName));
            location.NotEmpty(nameof(location));

            return _localizerCache.GetOrAdd($"B={baseName},L={location}", _ =>
            {
                var assembly = Assembly.LoadFile(location);
                baseName = GetResourcePrefix(baseName, location);

                return CreateResourceManagerStringLocalizer(assembly, baseName);
            });
        }


        /// <summary>
        /// 获取资源前缀。
        /// </summary>
        /// <param name="typeInfo">给定的 <see cref="TypeInfo"/>。</param>
        /// <param name="baseNamespace">给定的基础命名空间。</param>
        /// <param name="resourcesRelativePath">给定的资源相对路径。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        protected virtual string GetResourcePrefix(TypeInfo typeInfo, string baseNamespace, string resourcesRelativePath)
        {
            typeInfo.NotNull(nameof(typeInfo));
            baseNamespace.NotEmpty(nameof(baseNamespace));

            if (resourcesRelativePath.IsEmpty())
                return typeInfo.FullName;

            var prefix = $"{baseNamespace}.{resourcesRelativePath}";
            prefix += TrimPrefix(typeInfo.FullName, typeInfo.GetAssemblyDisplayName() + ".", baseNamespace + ".");
            Logger.LogTrace($"Get resource prefix '{prefix}'.");

            return prefix;
        }

        /// <summary>
        /// 获取资源前缀。
        /// </summary>
        /// <param name="baseResourceName">给定的基础资源名称。</param>
        /// <param name="baseNamespace">给定的基础命名空间。</param>
        /// <returns>返回字符串。</returns>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        protected virtual string GetResourcePrefix(string baseResourceName, string baseNamespace)
        {
            baseResourceName.NotEmpty(nameof(baseResourceName));
            baseNamespace.NotEmpty(nameof(baseNamespace));

            var assemblyName = new AssemblyName(baseNamespace);
            var assembly = Assembly.Load(assemblyName);
            var rootNamespace = GetRootNamespace(assembly);
            var resourceLocation = GetResourcePath(assembly);
            var locationPath = rootNamespace + "." + resourceLocation;

            baseResourceName = locationPath + TrimPrefix(baseResourceName, assembly.GetDisplayName() + ".", baseNamespace + ".");

            return baseResourceName;
        }

        ///// <summary>
        ///// 获取资源前缀。
        ///// </summary>
        ///// <param name="location">给定的程序集定位。</param>
        ///// <param name="baseName">给定的资源基础名称。</param>
        ///// <param name="resourceLocation">给定的资源定位。</param>
        ///// <returns>返回字符串。</returns>
        //[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        //protected virtual string GetResourcePrefix(string location, string baseName, string resourceLocation)
        //{
        //    // Re-root the base name if a resources path is set
        //    return location + "." + resourceLocation + TrimPrefix(baseName, string.Empty, location + ".");
        //}


        /// <summary>
        /// 创建资源管理器字符串定位器。
        /// </summary>
        /// <param name="assembly">给定的 <see cref="Assembly"/>。</param>
        /// <param name="baseName">给定的基础资源名。</param>
        /// <returns>返回 <see cref="ResourceManagerStringLocalizer"/>。</returns>
        protected virtual ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName)
        {
            var resourceManager = new TestCoreResourceManager(baseName, assembly, _loggerFactory.CreateLogger<TestCoreResourceManager>());

            return new ResourceManagerStringLocalizer(resourceManager, assembly, baseName, _resourceNamesCache,
                _loggerFactory.CreateLogger<ResourceManagerStringLocalizer>());
        }


        /// <summary>
        /// 获取根命名空间。
        /// </summary>
        /// <param name="assembly">给定的 <see cref="Assembly"/>。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetRootNamespace(Assembly assembly)
        {
            if (assembly.TryGetCustomAttribute(out RootNamespaceAttribute attribute))
                return attribute.RootNamespace;

            return assembly.GetDisplayName();
        }

        /// <summary>
        /// 获取资源路径。
        /// </summary>
        /// <param name="assembly">给定的 <see cref="Assembly"/>。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetResourcePath(Assembly assembly)
        {
            if (!assembly.TryGetCustomAttribute(out ResourceLocationAttribute attribute))
                return _resourcesRelativePath;

            // If we don't have an attribute assume all assemblies use the same resource location.
            return FormatLocationPath(attribute.ResourceLocation + ".");
        }


        private static string FormatLocationPath(string location)
        {
            if (location.IsNotEmpty())
            {
                if (location.CompatibleContains(ExtensionSettings.AltDirectorySeparator))
                    location = location.Replace(ExtensionSettings.AltDirectorySeparatorChar, '.');

                if (location.CompatibleContains(ExtensionSettings.DirectorySeparator))
                    location = location.Replace(ExtensionSettings.DirectorySeparatorChar, '.');
            }

            return location;
        }

        private static string TrimPrefix(string typeFullName, string assemblyNamePrefix, string baseNamespacePrefix)
        {
            // BUG: 原生没有考虑到程序集名称与程序集默认命名空间不一致的情况
            // typeFullName: Librame.Extensions.Core.HumanizationResource
            // assemblyNamePrefix: Librame.Extensions.Core.Abstractions
            if (typeFullName.StartsWith(assemblyNamePrefix, StringComparison.Ordinal))
                return typeFullName.Substring(assemblyNamePrefix.Length);

            // baseNamespacePrefix: Librame.Extensions.Core.
            // Fixed: [Librame.Extensions.Core.]HumanizationResource.resources
            if (typeFullName.StartsWith(baseNamespacePrefix, StringComparison.Ordinal))
                return typeFullName.Substring(baseNamespacePrefix.Length);

            return typeFullName;
        }

    }
}
