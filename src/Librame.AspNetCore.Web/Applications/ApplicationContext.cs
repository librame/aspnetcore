#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
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


        public ProjectDescriptor CurrentProject
            => Project.Current;

        public IThemepackInfo CurrentThemepackInfo
        {
            get => Themepack.CurrentInfo;
        }


        public ProjectDescriptor SetCurrentProject(string area)
            => Project.SetCurrent(area);

        public IThemepackInfo SetCurrentThemepackInfo(string name)
            => Themepack.SetCurrentInfo(name);
    }
}
