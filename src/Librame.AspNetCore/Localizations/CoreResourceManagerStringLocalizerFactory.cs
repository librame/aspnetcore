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
using System.IO;
using System.Reflection;

namespace Librame.AspNetCore
{
    using Extensions;

    /// <summary>
    /// ASP.NET Core 资源管理器字符串定位器工厂。
    /// </summary>
    public class CoreResourceManagerStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourcesRelativePath;


        /// <summary>
        /// 构造一个 <see cref="CoreResourceManagerStringLocalizerFactory"/> 实例。
        /// </summary>
        /// <param name="localizationOptions">给定的 <see cref="IOptions{LocalizationOptions}"/>。</param>
        /// <param name="loggerFactory">给定的 <see cref="ILoggerFactory"/>。</param>
        public CoreResourceManagerStringLocalizerFactory(
            IOptions<LocalizationOptions> localizationOptions,
            ILoggerFactory loggerFactory)
        {
            localizationOptions.NotNull(nameof(localizationOptions));

            LoggerFactory = loggerFactory.NotNull(nameof(loggerFactory));

            _resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? string.Empty;
            
            if (_resourcesRelativePath.IsNotNullOrEmpty())
            {
                _resourcesRelativePath = _resourcesRelativePath.Replace(Path.AltDirectorySeparatorChar, '.')
                    .Replace(Path.DirectorySeparatorChar, '.') + ".";
            }
        }


        /// <summary>
        /// 记录器工厂。
        /// </summary>
        protected ILoggerFactory LoggerFactory { get; }

        /// <summary>
        /// 记录器。
        /// </summary>
        protected ILogger Logger => LoggerFactory.CreateLogger<CoreResourceManagerStringLocalizerFactory>();

        /// <summary>
        /// 资源名称集合缓存。
        /// </summary>
        protected IResourceNamesCache ResourceNamesCache { get; private set; }
            = new ResourceNamesCache();

        /// <summary>
        /// 定位器缓存。
        /// </summary>
        protected ConcurrentDictionary<string, ResourceManagerStringLocalizer> LocalizerCache { get; private set; }
            = new ConcurrentDictionary<string, ResourceManagerStringLocalizer>();


        /// <summary>
        /// 获取资源前缀。
        /// </summary>
        /// <param name="typeInfo">给定的 <see cref="TypeInfo"/>。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetResourcePrefix(TypeInfo typeInfo)
        {
            typeInfo.NotNull(nameof(typeInfo));

            return GetResourcePrefix(typeInfo, GetRootNamespace(typeInfo.Assembly), GetResourcePath(typeInfo.Assembly));
        }

        /// <summary>
        /// 获取资源前缀。
        /// </summary>
        /// <param name="typeInfo">给定的 <see cref="TypeInfo"/>。</param>
        /// <param name="baseNamespace">给定的基础命名空间。</param>
        /// <param name="resourcesRelativePath">给定包含资源集合文件夹的相对路径。</param>
        /// <returns>返回字符串。</returns>
        /// <remarks>
        /// For the type "Sample.Controllers.Home" if there's a resourceRelativePath return
        /// "Sample.Resourcepath.Controllers.Home" if there isn't one then it would return "Sample.Controllers.Home".
        /// </remarks>
        protected virtual string GetResourcePrefix(TypeInfo typeInfo, string baseNamespace, string resourcesRelativePath)
        {
            typeInfo.NotNull(nameof(typeInfo));
            baseNamespace.NotNullOrEmpty(nameof(baseNamespace));

            var prefix = string.Empty;

            var reusableAttribute = GetResourceMappingAttribute(typeInfo);
            if (reusableAttribute.IsNotNull() && reusableAttribute.Enabled)
            {
                if (reusableAttribute.PrefixFactory.IsNull())
                {
                    reusableAttribute.PrefixFactory = (_baseNamespace, _resourcesRelativePath, _typeInfo) =>
                    {
                        if (resourcesRelativePath.IsNullOrEmpty())
                            return $"{_baseNamespace}.{_typeInfo.Name}";
                        else
                            return $"{_baseNamespace}.{_resourcesRelativePath}{_typeInfo.Name}"; // 已格式化为点分隔符（如：Resources.）
                    };
                }

                prefix = reusableAttribute.PrefixFactory.Invoke(baseNamespace, resourcesRelativePath, typeInfo);
            }
            else
            {
                if (resourcesRelativePath.IsNullOrEmpty())
                {
                    prefix = typeInfo.FullName;
                }
                else
                {
                    // This expectation is defined by dotnet's automatic resource storage.
                    // We have to conform to "{RootNamespace}.{ResourceLocation}.{FullTypeName - AssemblyName}".
                    var assemblyName = new AssemblyName(typeInfo.Assembly.FullName).Name;
                    prefix = baseNamespace + "." + resourcesRelativePath + TrimPrefix(typeInfo.FullName, assemblyName + ".");
                }
            }

            Logger.LogInformation($"{typeInfo.FullName} resource prefix: {prefix}");

            return prefix;
        }

        /// <summary>
        /// 获取资源前缀。
        /// </summary>
        /// <param name="baseResourceName">给定的基础资源名称。</param>
        /// <param name="baseNamespace">给定的基础命名空间。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetResourcePrefix(string baseResourceName, string baseNamespace)
        {
            baseResourceName.NotNullOrEmpty(nameof(baseResourceName));
            baseNamespace.NotNullOrEmpty(nameof(baseNamespace));

            var assemblyName = new AssemblyName(baseNamespace);
            var assembly = Assembly.Load(assemblyName);

            var rootNamespace = GetRootNamespace(assembly);
            var resourceLocation = GetResourcePath(assembly);
            var locationPath = rootNamespace + "." + resourceLocation;

            baseResourceName = locationPath + TrimPrefix(baseResourceName, baseNamespace + ".");

            return baseResourceName;
        }

        /// <summary>
        /// 获取资源前缀。
        /// </summary>
        /// <param name="location">给定的资源目录定位。</param>
        /// <param name="baseName">给定的基础名称。</param>
        /// <param name="resourceLocation">资源在 <paramref name="location"/> 中的位置。</param>
        /// <returns>返回字符串。</returns>
        protected virtual string GetResourcePrefix(string location, string baseName, string resourceLocation)
        {
            // Re-root the base name if a resources path is set
            return location + "." + resourceLocation + TrimPrefix(baseName, location + ".");
        }


        /// <summary>
        /// 获取资源映射特性。
        /// </summary>
        /// <param name="typeInfo">给定的 <see cref="TypeInfo"/>。</param>
        /// <returns>返回 <see cref="ResourceMappingAttribute"/>。</returns>
        protected virtual ResourceMappingAttribute GetResourceMappingAttribute(TypeInfo typeInfo)
        {
            if (typeInfo.TryGetCustomAttribute(out ResourceMappingAttribute typeAttribute))
                return typeAttribute;

            if (typeInfo.Assembly.TryGetCustomAttribute(out ResourceMappingAttribute assemblyAttribute))
                return assemblyAttribute;

            return null;
        }


        /// <summary>
        /// 创建字符串定位器。
        /// </summary>
        /// <param name="resourceSource">给定的资源来源类型。</param>
        /// <returns>返回 <see cref="ResourceManagerStringLocalizer"/>。</returns>
        public virtual IStringLocalizer Create(Type resourceSource)
        {
            var typeInfo = resourceSource.NotNull(nameof(resourceSource)).GetTypeInfo();

            var baseName = GetResourcePrefix(typeInfo);

            var assembly = typeInfo.Assembly;

            return LocalizerCache.GetOrAdd(baseName, _ =>
            {
                return CreateResourceManagerStringLocalizer(assembly, baseName);
            });
        }

        /// <summary>
        /// 创建字符串定位器。
        /// </summary>
        /// <param name="baseName">给定的基础名称。</param>
        /// <param name="location">给定的资源目录定位。</param>
        /// <returns>返回 <see cref="ResourceManagerStringLocalizer"/>。</returns>
        public virtual IStringLocalizer Create(string baseName, string location)
        {
            baseName.NotNullOrEmpty(nameof(baseName));
            location.NotNullOrEmpty(nameof(location));

            return LocalizerCache.GetOrAdd($"B={baseName},L={location}", _ =>
            {
                var assemblyName = new AssemblyName(location);
                var assembly = Assembly.Load(assemblyName);
                baseName = GetResourcePrefix(baseName, location);

                return CreateResourceManagerStringLocalizer(assembly, baseName);
            });
        }

        /// <summary>
        /// 创建资源管理器字符串定位器。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <param name="baseName">给定的基础名称。</param>
        /// <returns>返回 <see cref="ResourceManagerStringLocalizer"/>。</returns>
        protected virtual ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName)
        {
            // 使用核心资源管理器
            var resourceManager = new CoreResourceManager(baseName, assembly, LoggerFactory.CreateLogger<CoreResourceManager>());
            
            return new ResourceManagerStringLocalizer(
                resourceManager,
                assembly,
                baseName,
                ResourceNamesCache,
                Logger);
        }


        /// <summary>
        /// 获取根命名空间特性。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <returns>返回 <see cref="ResourceLocationAttribute"/>。</returns>
        protected virtual ResourceLocationAttribute GetResourceLocationAttribute(Assembly assembly)
        {
            return assembly.GetCustomAttribute<ResourceLocationAttribute>();
        }

        /// <summary>
        /// 获取根命名空间特性。
        /// </summary>
        /// <param name="assembly">给定的程序集。</param>
        /// <returns>返回 <see cref="RootNamespaceAttribute"/>。</returns>
        protected virtual RootNamespaceAttribute GetRootNamespaceAttribute(Assembly assembly)
        {
            return assembly.GetCustomAttribute<RootNamespaceAttribute>();
        }


        private string GetRootNamespace(Assembly assembly)
        {
            var rootNamespaceAttribute = GetRootNamespaceAttribute(assembly);

            return rootNamespaceAttribute?.RootNamespace ??
                new AssemblyName(assembly.FullName).Name;
        }

        private string GetResourcePath(Assembly assembly)
        {
            var resourceLocationAttribute = GetResourceLocationAttribute(assembly);

            // If we don't have an attribute assume all assemblies use the same resource location.
            var resourceLocation = resourceLocationAttribute == null
                ? _resourcesRelativePath
                : resourceLocationAttribute.ResourceLocation + ".";

            resourceLocation = resourceLocation
                .Replace(Path.DirectorySeparatorChar, '.')
                .Replace(Path.AltDirectorySeparatorChar, '.');

            return resourceLocation;
        }

        private static string TrimPrefix(string name, string prefix)
        {
            if (name.StartsWith(prefix, StringComparison.Ordinal))
                return name.Substring(prefix.Length);

            return name;
        }

    }
}