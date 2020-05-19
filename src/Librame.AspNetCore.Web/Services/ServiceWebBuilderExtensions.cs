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
    using Services;

    internal static class ServiceWebBuilderExtensions
    {
        internal static IWebBuilder AddServices(this IWebBuilder builder)
        {
            builder.Services.TryAddSingleton<ICopyrightService, CopyrightService>();
            builder.Services.TryAddSingleton<IUserPortraitService, UserPortraitService>();

            return builder;
        }

    }
}
