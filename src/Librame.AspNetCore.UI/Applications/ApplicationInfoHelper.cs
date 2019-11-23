#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 应用信息助手。
    /// </summary>
    public static class ApplicationInfoHelper
    {
        private static readonly string _interfaceAssemblyNamePattern
            = $@"^{nameof(Librame)}.{nameof(AspNetCore)}.(\w+).{nameof(UI)}$";

        private static readonly string _themepackAssemblyNamePattern
            = $@"^{nameof(Librame)}.{nameof(AspNetCore)}.{nameof(UI)}.(Themepack).(\w+)$";


        static ApplicationInfoHelper()
        {
            Themepacks = Themepacks.EnsureSingleton(() =>
            {
                return GetApplicationInfos(_themepackAssemblyNamePattern,
                    type => type.EnsureCreate<IThemepackInfo>());
            });
        }


        /// <summary>
        /// 主题包信息集合。
        /// </summary>
        public static ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }


        /// <summary>
        /// 获取界面信息集合。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactory"/>。</param>
        /// <param name="pattern">给定验证界面程序集名的正则表达式（可选）。</param>
        /// <returns>返回 <see cref="ConcurrentDictionary{TKey, TValue}"/>。</returns>
        public static ConcurrentDictionary<string, IInterfaceInfo> GetInterfaceInfos(ServiceFactory serviceFactory,
            string pattern = null)
        {
            return GetApplicationInfos(pattern.NotEmptyOrDefault(_interfaceAssemblyNamePattern),
                type => type.EnsureCreate<IInterfaceInfo>(serviceFactory));
        }


        /// <summary>
        /// 从程序集集合中获取指定的应用信息字典集合。
        /// </summary>
        /// <typeparam name="TAppInfo">指定的应用信息类型。</typeparam>
        /// <param name="pattern">给定匹配程序集名称的正则表达式模式。</param>
        /// <param name="createInfoFactory">给定创建信息实例的工厂方法。</param>
        /// <param name="assemblies">给定的程序集集合（可选；默认使用 <see cref="AssemblyUtility.CurrentDomainAssembliesWithoutSystem"/>）。</param>
        /// <returns>返回加载的字典集合。</returns>
        public static ConcurrentDictionary<string, TAppInfo> GetApplicationInfos<TAppInfo>(string pattern,
            Func<Type, TAppInfo> createInfoFactory, IEnumerable<Assembly> assemblies = null)
            where TAppInfo : IApplicationInfo
        {
            pattern.NotEmpty(nameof(pattern));
            createInfoFactory.NotNull(nameof(createInfoFactory));

            var regex = new Regex(pattern);
            var filterAssemblies = (assemblies ?? AssemblyUtility.CurrentDomainAssembliesWithoutSystem)
                .Where(assembly => regex.IsMatch(assembly.GetSimpleName()))
                .ToList();

            var infoType = typeof(TAppInfo);
            if (filterAssemblies.IsEmpty())
                throw new Exception($"Available application information '{infoType.FullName}' that matches the assembly name pattern '{pattern}' could not be found.");

            var infos = new ConcurrentDictionary<string, TAppInfo>();
            filterAssemblies.InvokeTypes(type =>
            {
                var info = createInfoFactory.Invoke(type);
                infos.AddOrUpdate(info.Name, info, (key, value) => info);
            },
            types => types.Where(type => infoType.IsAssignableFrom(type) && type.IsConcreteType()));

            return infos;
        }

    }
}
