#region License

/* **************************************************************************************
 * Copyright (c) Librame Pang All rights reserved.
 * 
 * http://librame.net
 * 
 * You must not remove this notice, or any other, from this software.
 * **************************************************************************************/

#endregion

using Microsoft.Extensions.DependencyInjection;

namespace Librame.AspNetCore.UI
{
    static class ApplicationUiBuilderExtensions
    {
        public static IUiBuilder AddApplications(this IUiBuilder builder)
        {
            builder.Services.AddSingleton<IApplicationContext, ApplicationContext>();
            builder.Services.AddSingleton<IApplicationPrincipal, ApplicationPrincipal>();

            builder.Services.ConfigureOptions(builder.ApplicationPostConfigureOptionsType);

            return builder;
        }

    }
}
