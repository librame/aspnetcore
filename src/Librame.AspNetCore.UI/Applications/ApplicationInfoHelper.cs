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
        private static readonly string _uiAssemblyNamePattern
            = $@"^{nameof(Librame)}.{nameof(AspNetCore)}.(\w+).{nameof(UI)}$";

        private static readonly string _themepackAssemblyNamePattern
            = $@"^{nameof(Librame)}.{nameof(AspNetCore)}.{nameof(UI)}.(Themepack).(\w+)$";


        static ApplicationInfoHelper()
        {
            Uis = Uis.EnsureSingleton(() =>
            {
                var infos = new ConcurrentDictionary<string, IUiInfo>();
                var interfaceType = typeof(IUiInfo);

                var regex = new Regex(_uiAssemblyNamePattern);
                var assemblies = AssemblyHelper.CurrentDomainAssembliesWithoutSystem
                    .Where(assem => regex.IsMatch(assem.GetName().Name));

                assemblies.InvokeTypes(type =>
                {
                    var info = type.EnsureCreate<IUiInfo>();
                    infos.AddOrUpdate(info.Name, info, (key, value) => info);
                },
                types => types.Where(type => interfaceType.IsAssignableFrom(type) && type.IsConcreteType()));

                return infos;
            });

            Themepacks = Themepacks.EnsureSingleton(() =>
            {
                var infos = new ConcurrentDictionary<string, IThemepackInfo>();
                var interfaceType = typeof(IThemepackInfo);

                var regex = new Regex(_themepackAssemblyNamePattern);
                var assemblies = AssemblyHelper.CurrentDomainAssembliesWithoutSystem
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
        /// 构建器用户界面集合。
        /// </summary>
        public static ConcurrentDictionary<string, IUiInfo> Uis { get; }

        /// <summary>
        /// 主题包集合。
        /// </summary>
        public static ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }
    }
}
