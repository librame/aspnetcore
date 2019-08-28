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

namespace Librame.AspNetCore.Identity
{
    static class ServiceIdentityBuilderWrapperExtensions
    {
        public static IIdentityBuilderWrapper AddServices(this IIdentityBuilderWrapper builderWrapper)
        {
            builderWrapper.Services.AddScoped<IIdentityIdentifierService, IdentityIdentifierService>();

            //builderWrapper.Services.TryReplace<IIdentifierService, IdentityIdentifierService>();
            //builderWrapper.Services.AddScoped(serviceProvider =>
            //{
            //    return (IIdentityIdentifierService)serviceProvider.GetRequiredService<IIdentifierService>();
            //});

            return builderWrapper;
        }

    }
}
