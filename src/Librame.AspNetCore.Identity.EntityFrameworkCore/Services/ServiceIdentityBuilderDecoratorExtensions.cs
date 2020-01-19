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

namespace Librame.AspNetCore.Identity.Builders
{
    using Services;

    internal static class ServiceIdentityBuilderDecoratorExtensions
    {
        public static IIdentityBuilderDecorator AddServices(this IIdentityBuilderDecorator decorator)
        {
            decorator.Services.TryAddScoped<IDefaultPasswordService, DefaultPasswordService>();

            return decorator;
        }

    }
}
