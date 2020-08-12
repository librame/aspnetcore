#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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
    using AspNetCore.Web.Builders;
    using Extensions;
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ProjectContext : IProjectContext
    {
        private readonly WebBuilderOptions _options;

        private ProjectDescriptor _current;


        public ProjectContext(ServiceFactory serviceFactory,
            IEnumerable<IProjectNavigation> navigations,
            IOptions<WebBuilderOptions> options)
        {
            ServiceFactory = serviceFactory.NotNull(nameof(serviceFactory));
            Navigations = navigations.NotNull(nameof(navigations));
            _options = options.NotNull(nameof(options)).Value;

            Initialize();
        }


        private void Initialize()
        {
            var identityNavigation = Navigations.FirstOrDefault(_options.PredicateIdentityNavigation);
            if (identityNavigation.IsNotNull())
            {
                Navigations.ForEach(nav => nav.IdentityNavigation = identityNavigation);
            }
        }


        private Dictionary<string, IProjectInfo> LoadInfos()
        {
            var infos = ApplicationHelper.GetApplicationInfos(_options.SearchApplicationAssemblyPatterns,
                type => type.EnsureCreate<IProjectInfo>(ServiceFactory)); // 此创建方法要求项目信息实现类型可公共构造

            // Add default AreaInfo
            var defaultInfo = new DefaultProjectInfo(ServiceFactory);
            infos.Add(defaultInfo.Name, defaultInfo);

            return infos;
        }


        public ServiceFactory ServiceFactory { get; }

        public IEnumerable<IProjectNavigation> Navigations { get; }

        public IReadOnlyDictionary<string, IProjectInfo> Infos
            => LoadInfos();


        public ProjectDescriptor Current
        {
            get
            {
                if (_current.IsNull())
                    return SetCurrent(null);

                return _current;
            }
        }


        public ProjectDescriptor SetCurrent(string area)
        {
            ExtensionSettings.Preference.RunLocker(() =>
            {
                _current = new ProjectDescriptor(FindInfo(area), FindNavigation(area));
            });
            
            return _current;
        }


        public IProjectInfo FindInfo(string name)
        {
            // 项目信息键名支持 default
            if (name.IsNotEmpty() && Infos.TryGetValue(name, out IProjectInfo info))
                return info;
            
            return Infos[DefaultProjectInfo.DefaultProjectName];
        }

        public IProjectNavigation FindNavigation(string area)
        {
            // 项目导航键名不支持 default
            if (area == DefaultProjectInfo.DefaultProjectName)
                area = null; // 默认项目导航的区域必须为空

            return Navigations.First(nav => nav.Area == area);
        }

    }
}
