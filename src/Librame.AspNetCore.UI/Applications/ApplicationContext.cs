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
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(ServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));

            InterfaceInfos = ApplicationInfoHelper.GetInterfaceInfos(serviceFactory);
            ThemepackInfos = ApplicationInfoHelper.Themepacks;
            ThemepackInfos.Values.ForEach(info => info.ApplyServiceFactory(serviceFactory));

            CurrentThemepackInfo = GetThemepackInfo(name: null);
        }


        public ServiceFactory ServiceFactory { get; }


        public IApplicationPrincipal Principal
            => ServiceFactory.GetService<IApplicationPrincipal>();

        public IWebHostEnvironment Environment
            => ServiceFactory.GetService<IWebHostEnvironment>();


        public ConcurrentDictionary<string, IInterfaceInfo> InterfaceInfos { get; }

        public ConcurrentDictionary<string, IThemepackInfo> ThemepackInfos { get; }


        public IInterfaceInfo CurrentInterfaceInfo { get; private set; }

        public IThemepackInfo CurrentThemepackInfo { get; private set; }


        public IInterfaceInfo GetInterfaceInfo(string name, bool orDefault = true)
        {
            if (name.IsNotEmpty() && InterfaceInfos.ContainsKey(name))
                return InterfaceInfos[name];

            if (!orDefault)
                return null;

            return InterfaceInfos.Values.First()
                ?? throw new ArgumentException($"No available {nameof(IInterfaceInfo)} is loaded.");
        }

        public IInterfaceInfo SetCurrentInterfaceInfo(string name, bool orDefault = true)
        {
            CurrentInterfaceInfo = GetInterfaceInfo(name, orDefault);
            return CurrentInterfaceInfo;
        }


        public IThemepackInfo GetThemepackInfo(string name, bool orDefault = true)
        {
            if (name.IsNotEmpty() && ThemepackInfos.ContainsKey(name))
                return ThemepackInfos[name];

            if (!orDefault)
                return null;

            return ThemepackInfos.Values.First()
                ?? throw new ArgumentException($"No available {nameof(IThemepackInfo)} is loaded.");
        }

        public IThemepackInfo SetCurrentThemepackInfo(string name, bool orDefault = true)
        {
            CurrentThemepackInfo = GetThemepackInfo(name, orDefault);
            return CurrentThemepackInfo;
        }

    }
}
