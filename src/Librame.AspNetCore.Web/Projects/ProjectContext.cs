#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Librame.AspNetCore.Web.Projects
{
    using AspNetCore.Applications;
    using Builders;
    using Extensions;
    using Extensions.Core.Services;
    using Routings;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ProjectContext : IProjectContext
    {
        private (IProjectInfo Info, IProjectNavigation Navigation) _current;


        public ProjectContext(ServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));
            Navigations = serviceFactory.GetRequiredService<IEnumerable<IProjectNavigation>>();

            Infos = LoadInfos();
        }


        private Dictionary<string, IProjectInfo> LoadInfos()
        {
            var builderOptions = ServiceFactory.GetRequiredService<IOptions<WebBuilderOptions>>().Value;
            var infos = ApplicationHelper.GetApplicationInfos(builderOptions.SearchApplicationAssemblyPatterns,
                type => type.EnsureCreate<IProjectInfo>(ServiceFactory)); // 此创建方法要求类型可公共访问

            // Add default AreaInfo
            infos.Add(RouteDescriptor.DefaultRouteName, new DefaultProjectInfo(ServiceFactory));

            return infos;
        }


        public ServiceFactory ServiceFactory { get; }

        public IEnumerable<IProjectNavigation> Navigations { get; }

        public IReadOnlyDictionary<string, IProjectInfo> Infos { get; }

        public (IProjectInfo Info, IProjectNavigation Navigation) Current
        {
            get
            {
                if (_current.Info.IsNull() || _current.Navigation.IsNull())
                    return SetCurrent(null);

                return _current;
            }
        }


        public (IProjectInfo Info, IProjectNavigation Navigation) SetCurrent(string area)
        {
            area = FormatArea(area);
            if (area.IsNotNull() && _current.Navigation.IsNotNull()
                && !_current.Navigation.Area.Equals(area, StringComparison.OrdinalIgnoreCase))
            {
                _current = (FindInfo(area), FindNavigation(area));
            }

            return _current;
        }

        private IProjectInfo FindInfo(string name)
        {
            if (name.IsNotEmpty() && Infos.TryGetValue(name, out IProjectInfo info))
                return info;
            
            return Infos[RouteDescriptor.DefaultRouteName];
        }

        private IProjectNavigation FindNavigation(string area)
        {
            area = FormatArea(area);
            return Navigations.First(nav => nav.Area == area);
        }

        private static string FormatArea(string area)
        {
            if ((area.IsNotNull() && area.Length == 0)
                || RouteDescriptor.DefaultRouteName.Equals(area, StringComparison.OrdinalIgnoreCase))
            {
                return null; // 将 string.Empty/default 转换为 null
            }

            return area;
        }

    }
}
