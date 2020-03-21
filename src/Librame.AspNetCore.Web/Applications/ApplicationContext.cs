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
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Web.Applications
{
    using AspNetCore.Web.Projects;
    using AspNetCore.Web.Services;
    using AspNetCore.Web.Themepacks;
    using Extensions;
    using Extensions.Core.Services;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class ApplicationContext : IApplicationContext
    {
        public ApplicationContext(IApplicationPrincipal principal,
            IProjectContext project, IThemepackContext themepack,
            IWebHostEnvironment environment, ICopyrightService copyright)
        {
            Principal = principal.NotNull(nameof(principal));
            Project = project.NotNull(nameof(project));
            Themepack = themepack.NotNull(nameof(themepack));
            Environment = environment.NotNull(nameof(environment));
            Copyright = copyright.NotNull(nameof(copyright));
        }


        public IApplicationPrincipal Principal { get; }

        public IProjectContext Project { get; }

        public IThemepackContext Themepack { get; }

        public IWebHostEnvironment Environment { get; }

        public ICopyrightService Copyright { get; }

        public ServiceFactory ServiceFactory
            => Project.ServiceFactory;


        public IThemepackInfo CurrentThemepackInfo
        {
            get => Themepack.CurrentInfo;
            set => Themepack.CurrentInfo = value;
        }

        public (IProjectInfo Info, IProjectNavigation Navigation) LoginbarProject
            => Project.Loginbar;

        public (IProjectInfo Info, IProjectNavigation Navigation) CurrentProject
            => Project.Current;


        public (IProjectInfo Info, IProjectNavigation Navigation) SetCurrentProject(string area)
            => Project.SetCurrent(area);
    }
}
