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
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Librame.AspNetCore.UI
{
    using Extensions;
    using Extensions.Core;

    class ApplicationContext : IApplicationContext
    {
        private readonly ConcurrentDictionary<string, IUiInfo> _uis = null;
        private bool _uisInitialized = false;


        public ApplicationContext(ServiceFactoryDelegate serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));

            Themepacks = ApplicationInfoHelper.Themepacks;
            _uis = ApplicationInfoHelper.Uis;
        }


        public ServiceFactoryDelegate ServiceFactory { get; }

        public IApplicationPrincipal Principal
            => ServiceFactory.GetService<IApplicationPrincipal>();

        public IHostingEnvironment Environment
            => ServiceFactory.GetService<IHostingEnvironment>();


        public ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }


        public ConcurrentDictionary<string, IStringLocalizer> Localizers
            => new ConcurrentDictionary<string, IStringLocalizer>();

        public ConcurrentDictionary<string, List<NavigationDescriptor>> Navigations
            => new ConcurrentDictionary<string, List<NavigationDescriptor>>();


        public IUiInfo GetUi(RouteDescriptor route)
        {
            return GetUi(route?.Area);
        }

        public IUiInfo GetUi(string name)
        {
            if (!_uisInitialized)
            {
                _uis.ForEach(pair =>
                {
                    pair.Value.AddLocalizers(Localizers, ServiceFactory);
                    pair.Value.AddNavigations(Navigations, ServiceFactory);
                });

                _uisInitialized = true;
            }

            return _uis[name];
        }

    }
}
