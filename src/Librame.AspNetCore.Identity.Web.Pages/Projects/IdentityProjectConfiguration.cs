#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
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

            conventions.AuthorizeAreaFolder(Area, Navigation.Manage.Route);
            conventions.AuthorizeAreaPage(Area, Navigation.Logout.Route);

            var filter = typeof(ExternalAuthenticationSchemesPageFilter<>)
                .MakeGenericType(Builder.UserType)
                .EnsureCreateObject(BuilderOptions);

            conventions.AddAreaFolderApplicationModelConvention(Area,
                Navigation.Manage.Route,
                model => model.Filters.Add((IFilterMetadata)filter));
        }

    }
}
