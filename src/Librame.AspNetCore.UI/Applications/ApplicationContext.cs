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
using System.Linq;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(ServiceFactoryDelegate serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));

            InterfaceInfos = ApplicationInfoHelper.GetInterfaces(serviceFactory);
            ThemepackInfos = ApplicationInfoHelper.Themepacks;
            ThemepackInfos.Values.ForEach(info => info.ApplyServiceFactory(serviceFactory));

            CurrentThemepackInfo = GetThemepackInfo(name: null);
        }


        public ServiceFactoryDelegate ServiceFactory { get; }


        public IApplicationPrincipal Principal
            => ServiceFactory.GetService<IApplicationPrincipal>();

        public IHostingEnvironment Environment
            => ServiceFactory.GetService<IHostingEnvironment>();


        public ConcurrentDictionary<string, IInterfaceInfo> InterfaceInfos { get; }

        public ConcurrentDictionary<string, IThemepackInfo> ThemepackInfos { get; }


        public IInterfaceInfo CurrentInterfaceInfo { get; private set; }

        public IThemepackInfo CurrentThemepackInfo { get; private set; }


        public IInterfaceInfo GetInterfaceInfo(string name, bool orDefault = true)
        {
            if (name.IsNotNullOrEmpty() && InterfaceInfos.ContainsKey(name))
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
            if (name.IsNotNullOrEmpty() && ThemepackInfos.ContainsKey(name))
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
