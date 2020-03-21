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
    using AspNetCore.Web.Builders;
    using Extensions;
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ThemepackContext : IThemepackContext
    {
        private IThemepackInfo _currentInfo = null;


        public ThemepackContext(ServiceFactory serviceFactory, IWebBuilder builder)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));
            Builder = builder.NotNull(nameof(builder));
        }


        public ServiceFactory ServiceFactory { get; }

        public IWebBuilder Builder { get; }

        public IReadOnlyDictionary<string, IThemepackInfo> Infos
            => Builder.ThemepackInfos;


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


        public IThemepackInfo SetCurrentInfo(string name)
        {
            _currentInfo = FindInfo(name);
            return _currentInfo;
        }


        public IThemepackInfo FindInfo(string name)
        {
            // 项目信息键名支持 default
            if (name.IsNotEmpty() && Infos.TryGetValue(name, out IThemepackInfo info))
                return info;

            return Infos.Values.First();
        }

    }
}
