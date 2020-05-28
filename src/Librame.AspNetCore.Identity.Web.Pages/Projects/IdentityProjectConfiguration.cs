#region License

/* **************************************************************************************
 * Copyright (c) Librame Pong All rights reserved.
 * 
 * https://github.com/librame
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Librame.AspNetCore.Identity.Web.Projects
{
    using AspNetCore.Web.Applications;
    using AspNetCore.Web.Projects;
    using Extensions;

    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses")]
    internal class IdentityProjectConfiguration : ProjectConfigurationBaseWithRazorPages
    {
        public IdentityProjectConfiguration(IApplicationContext context)
            : base(context, nameof(Identity))
        {
        }


        protected override void ConfigurePageConventions(PageConventionCollection conventions)
        {
            base.ConfigurePageConventions(conventions);

            var managePath = Navigation.Manage.GenerateSimulativeLink();

            conventions.AuthorizeAreaFolder(Area, managePath);
            conventions.AuthorizeAreaPage(Area, Navigation.Logout.GenerateSimulativeLink());

            var filter = typeof(ExternalAuthenticationSchemesPageFilter<>)
                .MakeGenericType(Builder.UserType)
                .EnsureCreateObject(BuilderOptions);

            conventions.AddAreaFolderApplicationModelConvention(Area,
                managePath,
                model => model.Filters.Add((IFilterMetadata)filter));
        }

    }
}
