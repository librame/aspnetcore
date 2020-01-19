#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Librame.AspNetCore.Web.Builders
{
    using Applications;

    internal static class ApplicationWebBuilderExtensions
    {
        internal static IWebBuilder AddApplications(this IWebBuilder builder)
        {
            builder.Services.TryAddSingleton<IApplicationContext, ApplicationContext>();
            builder.Services.TryAddSingleton<IApplicationPrincipal, ApplicationPrincipal>();

            return builder;
        }

    }
}
