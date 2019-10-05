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

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;
    using Extensions;

    class IdentityInterfaceConfigurationWithPages : InterfaceConfigurationWithPages
    {
        public IdentityInterfaceConfigurationWithPages(IApplicationContext context)
            : base(context, nameof(Identity))
        {
        }


        protected override void ConfigurePageConventions(PageConventionCollection conventions)
        {
            base.ConfigurePageConventions(conventions);

            conventions.AuthorizeAreaFolder(Area, Info.Sitemap.Manage.Route);
            conventions.AuthorizeAreaPage(Area, Info.Sitemap.Logout.Route);

            var filter = typeof(ExternalAuthenticationSchemesPageFilter<>)
                .MakeGenericType(Builder.UserType)
                .EnsureCreateObject(BuilderOptions);

            conventions.AddAreaFolderApplicationModelConvention(Area,
                Info.Sitemap.Manage.Route,
                model => model.Filters.Add((IFilterMetadata)filter));
        }
    }
}
