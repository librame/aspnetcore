#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Web.Themepacks
{
    using Builders;
    using Extensions;
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ThemepackContext : IThemepackContext
    {
        private IThemepackInfo _currentInfo;


        public ThemepackContext(ServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));

            Builder = serviceFactory.GetRequiredService<IWebBuilder>();

            Infos = Builder.ThemepackInfos;
        }


        public ServiceFactory ServiceFactory { get; }

        public IWebBuilder Builder { get; }

        public IReadOnlyDictionary<string, IThemepackInfo> Infos { get; }


        public IThemepackInfo CurrentInfo
        {
            get
            {
                if (_currentInfo.IsNull())
                    _currentInfo = Infos.Values.First();

                return _currentInfo;
            }
            set
            {
                _currentInfo = value.NotNull(nameof(value));
            }
        }

    }
}
