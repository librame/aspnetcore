#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    /// <summary>
    /// 内部应用上下文。
    /// </summary>
    internal class InternalApplicationContext : IApplicationContext
    {
        private static readonly string UserInterfaceAssemblyNamePattern
            = $@"^{nameof(Librame)}.{nameof(AspNetCore)}.(\w).{nameof(UI)}$";

        private static readonly string ThemepackAssemblyNamePattern
            = $@"^{nameof(Librame)}.{nameof(AspNetCore)}.{nameof(UI)}.(\w)$";


        /// <summary>
        /// 构造一个 <see cref="InternalApplicationContext"/>。
        /// </summary>
        /// <param name="serviceProvider">给定的 <see cref="IServiceProvider"/>。</param>
        public InternalApplicationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider.NotNull(nameof(serviceProvider));
            Environment = ServiceProvider.GetRequiredService<IHostingEnvironment>();

            UserInterfaces = UserInterfaces.EnsureSingleton(() =>
            {
                var infos = new ConcurrentDictionary<string, IUserInterfaceInfo>();
                var interfaceType = typeof(IUserInterfaceInfo);

                var regex = new Regex(UserInterfaceAssemblyNamePattern);
                var assemblies = AssemblyHelper.CurrentDomainAssembliesWithoutSystem
                    .Where(assem => regex.IsMatch(assem.GetName().Name));

                assemblies.InvokeTypes(type =>
                {
                    var info = (IUserInterfaceInfo)type.EnsureCreate();

                    // Adds
                    info.AddLocalizers(Localizers, serviceProvider);
                    info.AddNavigations(Navigations, serviceProvider);

                    infos.AddOrUpdate(info.Name, info, (key, value) => info);
                },
                types => types.Where(type => interfaceType.IsAssignableFrom(type) && type.IsConcreteType()));

                return infos;
            });

            Themepacks = Themepacks.EnsureSingleton(() =>
            {
                var infos = new ConcurrentDictionary<string, IThemepackInfo>();
                var interfaceType = typeof(IThemepackInfo);

                var regex = new Regex(ThemepackAssemblyNamePattern);
                var assemblies = AssemblyHelper.CurrentDomainAssembliesWithoutSystem
                    .Where(assem => regex.IsMatch(assem.GetName().Name));

                assemblies.InvokeTypes(type =>
                {
                    var info = (IThemepackInfo)type.EnsureCreate();
                    infos.AddOrUpdate(info.Name, info, (key, value) => info);
                },
                types => types.Where(type => interfaceType.IsAssignableFrom(type) && type.IsConcreteType()));

                return infos;
            });
        }


        /// <summary>
        /// 服务提供程序。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IServiceProvider"/>。
        /// </value>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 主机环境。
        /// </summary>
        /// <value>
        /// 返回 <see cref="IHostingEnvironment"/>。
        /// </value>
        public IHostingEnvironment Environment { get; }

        /// <summary>
        /// 构建器用户界面集合。
        /// </summary>
        public ConcurrentDictionary<string, IUserInterfaceInfo> UserInterfaces { get; }

        /// <summary>
        /// 主题包集合。
        /// </summary>
        public ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }

        /// <summary>
        /// 本地化定位器集合。
        /// </summary>
        public ConcurrentDictionary<string, IStringLocalizer> Localizers
            => new ConcurrentDictionary<string, IStringLocalizer>();

        /// <summary>
        /// 导航集合。
        /// </summary>
        public ConcurrentDictionary<string, List<NavigationDescriptor>> Navigations
            => new ConcurrentDictionary<string, List<NavigationDescriptor>>();


        /// <summary>
        /// 获取指定路由的用户界面信息。
        /// </summary>
        /// <param name="routeDescriptor">给定的 <see cref="RouteDescriptor"/>。</param>
        /// <returns>返回 <see cref="IUserInterfaceInfo"/>。</returns>
        public IUserInterfaceInfo GetUserInterface(RouteDescriptor routeDescriptor)
        {
            if (routeDescriptor.IsNotNull() && routeDescriptor.Area.IsNotNullOrEmpty())
                return UserInterfaces[routeDescriptor.Area];

            return null;
        }

    }
}
