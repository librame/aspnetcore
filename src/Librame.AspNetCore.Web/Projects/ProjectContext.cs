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
                type => type.EnsureCreate<IProjectInfo>(ServiceFactory)); // 此创建方法要求项目信息实现类型可公共构造

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
            set
            {
                value.Info.NotNull(nameof(value.Info));
                value.Navigation.NotNull(nameof(value.Navigation));

                _current = value;
            }
        }


        public (IProjectInfo Info, IProjectNavigation Navigation) SetCurrent(string area)
        {
            _current = (FindInfo(area), FindNavigation(area));
            return _current;
        }


        public IProjectInfo FindInfo(string name)
        {
            // 项目信息键名支持 default
            if (name.IsNotEmpty() && Infos.TryGetValue(name, out IProjectInfo info))
                return info;
            
            return Infos[RouteDescriptor.DefaultRouteName];
        }

        public IProjectNavigation FindNavigation(string area)
        {
            // 项目导航键名不支持 default
            if (area == RouteDescriptor.DefaultRouteName)
                area = null; // 默认项目导航的区域必须为空

            return Navigations.First(nav => nav.Area == area);
        }

    }
}
