#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Concurrent;
using System.Linq;
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
                var infos = new ConcurrentDictionary<string, IThemepackInfo>();
                var interfaceType = typeof(IThemepackInfo);

                var regex = new Regex(_themepackAssemblyNamePattern);
                var assemblies = AssemblyUtility.CurrentDomainAssembliesWithoutSystem
                    .Where(assem => regex.IsMatch(assem.GetName().Name));

                assemblies.InvokeTypes(type =>
                {
                    var info = type.EnsureCreate<IThemepackInfo>();
                    infos.AddOrUpdate(info.Name, info, (key, value) => info);
                },
                types => types.Where(type => interfaceType.IsAssignableFrom(type) && type.IsConcreteType()));

                return infos;
            });
        }


        /// <summary>
        /// 主题包信息集合。
        /// </summary>
        public static ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }


        /// <summary>
        /// 获取界面信息集合。
        /// </summary>
        /// <param name="serviceFactory">给定的 <see cref="ServiceFactoryDelegate"/>。</param>
        /// <param name="pattern">给定验证界面程序集名的正则表达式（可选）。</param>
        /// <returns>返回 <see cref="ConcurrentDictionary{TKey, TValue}"/>。</returns>
        public static ConcurrentDictionary<string, IInterfaceInfo> GetInterfaces(ServiceFactoryDelegate serviceFactory,
            string pattern = null)
        {
            var infos = new ConcurrentDictionary<string, IInterfaceInfo>();

            var regex = new Regex(pattern.NotEmptyOrDefault(_interfaceAssemblyNamePattern));
            var assemblies = AssemblyUtility.CurrentDomainAssembliesWithoutSystem
                .Where(assem => regex.IsMatch(assem.GetName().Name));

            var interfaceType = typeof(IInterfaceInfo);
            assemblies.InvokeTypes(type =>
            {
                var info = type.EnsureCreate<IInterfaceInfo>(serviceFactory);
                infos.AddOrUpdate(info.Name, info, (key, value) => info);
            },
            types => types.Where(type => interfaceType.IsAssignableFrom(type) && type.IsConcreteType()));

            return infos;
        }

    }
}
