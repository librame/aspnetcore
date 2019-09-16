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
using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.Identity.UI
{
    using AspNetCore.UI;

    class IdentityInterfaceConfigurationWithPages : InterfaceConfigurationWithPages
    {
        public IdentityInterfaceConfigurationWithPages(IApplicationContext context)
            : base(context, nameof(Identity))
        {
        }


        protected override void ConfigurePageConventions(PageConventionCollection conventions)
        {
            base.ConfigurePageConventions(conventions);

            conventions.AuthorizeAreaFolder(Area, Info.Sitemap.Manage.RelativePath);
            conventions.AuthorizeAreaPage(Area, Info.Sitemap.Logout.RelativePath);

            var filter = new ExternalAuthenticationSchemesPageFilter(Builder, BuilderOptions);
            conventions.AddAreaFolderApplicationModelConvention(Area,
                Info.Sitemap.Manage.RelativePath,
                model => model.Filters.Add(filter));
        }
    }
}
