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
        private ConcurrentDictionary<string, IStringLocalizer> _localizers = null;
        private ConcurrentDictionary<string, List<NavigationDescriptor>> _navigations = null;


        public ApplicationContext(ServiceFactoryDelegate serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));

            Themepacks = ApplicationInfoUtility.Themepacks;
            Uis = ApplicationInfoUtility.Uis;

            if (_localizers.IsNull() || _navigations.IsNull())
            {
                _localizers = new ConcurrentDictionary<string, IStringLocalizer>();
                _navigations = new ConcurrentDictionary<string, List<NavigationDescriptor>>();

                Uis.ForEach(pair =>
                {
                    pair.Value.AddLocalizers(ref _localizers, ServiceFactory);
                    pair.Value.AddNavigations(ref _navigations, ServiceFactory);
                });
            }
        }


        public ServiceFactoryDelegate ServiceFactory { get; }

        public IApplicationPrincipal Principal
            => ServiceFactory.GetService<IApplicationPrincipal>();

        public IHostingEnvironment Environment
            => ServiceFactory.GetService<IHostingEnvironment>();


        public ConcurrentDictionary<string, IThemepackInfo> Themepacks { get; }

        public ConcurrentDictionary<string, IUiInfo> Uis { get; }


        public ConcurrentDictionary<string, IStringLocalizer> Localizers
            => _localizers;

        public ConcurrentDictionary<string, List<NavigationDescriptor>> Navigations
            => _navigations;
    }
}
